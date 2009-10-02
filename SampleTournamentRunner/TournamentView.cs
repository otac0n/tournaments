using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tournaments.Standard;
using System.Diagnostics;

namespace Tournaments.Sample
{
    public partial class TournamentView : Form
    {
        private IPairingsGenerator generator;
        private ITournamentVisualizer visualizer;
        private SystemGraphics measureGraphics = new SystemGraphics();
        private List<TournamentTeam> teams = new List<TournamentTeam>();
        private List<TournamentRound> rounds = new List<TournamentRound>();
        private Dictionary<long, string> teamNames = new Dictionary<long, string>();
        int teamIdentity = 0;

        private Dictionary<TournamentRound, List<ListViewGroup>> roundGroupsHelper = new Dictionary<TournamentRound, List<ListViewGroup>>();
        private Dictionary<TournamentRound, List<ListViewItem>> roundItemsHelper = new Dictionary<TournamentRound, List<ListViewItem>>();
        private Dictionary<long, List<ListViewItem>> teamItemsHelper = new Dictionary<long, List<ListViewItem>>();

        public TournamentView(IPairingsGenerator generator)
        {
            if (generator == null)
            {
                throw new ArgumentNullException("generator");
            }

            InitializeComponent();

            this.generator = generator;
            this.generator.Reset();
            this.visualizer = this.generator as ITournamentVisualizer;

            this.UpdateState();
        }

        private void UpdateState()
        {
            bool roundsPlayed = this.rounds.Count > 0;
            bool nextRoundAvailable = false;
            bool validState = false;

            try
            {
                this.generator.Reset();
                this.generator.LoadState(this.teams, this.rounds);
                validState = true;

                this.TournamentStatus.Text = "Ready.";
                this.TournamentStatus.BackColor = Color.Transparent;
            }
            catch (InvalidTournamentStateException ex)
            {
                this.Visualization.Image = null;
                this.TournamentStatus.Text = "Error: " + ex.Message;
                this.TournamentStatus.BackColor = Color.Red;
            }

            if (validState)
            {
                try
                {
                    this.StandingsList.Items.Clear();
                    var standings = this.generator.GenerateRankings();
                    foreach (var standing in standings)
                    {
                        var item = new ListViewItem(new string[] { standing.Rank.ToString(), this.teamNames[standing.Team.TeamId], standing.ScoreDescription });
                        this.StandingsList.Items.Add(item);
                    }
                }
                catch (InvalidTournamentStateException)
                {
                }

                try
                {
                    if (this.visualizer != null)
                    {
                        var names = new TournamentNameTable(this.teamNames);
                        var size = this.visualizer.Measure(this.measureGraphics, names);
                        if (size.Height > 0 && size.Width > 0)
                        {
                            var rendered = new Bitmap((int)Math.Ceiling(size.Width), (int)Math.Ceiling(size.Height));
                            this.visualizer.Render(new SystemGraphics(rendered), names);
                            this.Visualization.Image = rendered;
                        }
                        else
                        {
                            this.Visualization.Image = null;
                        }
                    }
                }
                catch (InvalidTournamentStateException)
                {
                }

                try
                {
                    var round = this.generator.CreateNextRound(null);
                    if (round != null)
                    {
                        nextRoundAvailable = true;
                    }
                }
                catch (InvalidTournamentStateException)
                {
                }
            }

            this.AddTeam.Enabled = !roundsPlayed;
            this.RollBack.Enabled = roundsPlayed;
            this.StartNext.Enabled = nextRoundAvailable;

            foreach(ColumnHeader col in this.TeamsList.Columns)
            {
                col.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            }

            foreach (ColumnHeader col in this.StandingsList.Columns)
            {
                col.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            }

            foreach (ColumnHeader col in this.RoundsList.Columns)
            {
                col.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        public void Run()
        {
            Application.Run(this);
        }

        private void AddTeam_Click(object sender, EventArgs e)
        {
            if (this.rounds.Count == 0)
            {
                var id = ++this.teamIdentity;

                var team = new TournamentTeam(id, 100);
                this.teams.Add(team);
                this.teamNames.Add(id, "New Team");

                var item = new ListViewItem(new string[] { this.teamNames[team.TeamId], team.Rating.ToString() });
                item.Tag = team;
                this.TeamsList.Items.Add(item);

                this.UpdateState();

                this.TeamsList.SelectedIndices.Clear();
                item.BeginEdit();
            }
        }

        private void TeamsList_DoubleClick(object sender, EventArgs e)
        {
            if (this.TeamsList.SelectedIndices.Count != 0)
            {
                var item = this.TeamsList.SelectedItems[0];
                var team = (TournamentTeam)item.Tag;
                string name = teamNames[team.TeamId];
                int rating = team.Rating.Value;

                using (EditTeamView dlg = new EditTeamView(name, rating))
                {
                    dlg.ShowDialog();
                    teamNames[team.TeamId] = dlg.TeamName;
                    team.Rating = dlg.Rating;
                    item.SubItems[0].Text = teamNames[team.TeamId];
                    item.SubItems[1].Text = team.Rating.ToString();
                }

                this.UpdateState();
            }
        }

        private void TeamsList_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if(object.ReferenceEquals(e.Label, null))
            {
                return;
            }
            if (string.IsNullOrEmpty(e.Label))
            {
                e.CancelEdit = true;
                return;
            }

            var item = this.TeamsList.Items[e.Item];
            var team = (TournamentTeam)item.Tag;
            teamNames[team.TeamId] = e.Label;

            if(this.teamItemsHelper.ContainsKey(team.TeamId))
            {
                var staleItems = this.teamItemsHelper[team.TeamId];
                foreach (var stale in staleItems)
                {
                    stale.SubItems[1].Text = teamNames[team.TeamId];
                }
            }

            this.UpdateState();
        }

        private void TeamsList_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.rounds.Count == 0 && !e.Alt && !e.Control && e.KeyCode == Keys.Delete)
            {
                var items = (from ListViewItem t in this.TeamsList.SelectedItems
                            select t).ToList();

                foreach (var item in items)
                {
                    var team = (TournamentTeam)item.Tag;
                    this.teams.Remove(team);
                    this.teamNames.Remove(team.TeamId);
                    this.TeamsList.Items.Remove(item);
                }

                this.UpdateState();
            }
        }

        private void StartNext_Click(object sender, EventArgs e)
        {
            TournamentRound round = null;
            try
            {
                this.generator.Reset();
                this.generator.LoadState(this.teams, this.rounds);
                round = this.generator.CreateNextRound(null);
            }
            catch (InvalidTournamentStateException ex)
            {
                MessageBox.Show(ex.Message, "Error Creating Next Round.");
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }
                return;
            }

            if (round != null)
            {
                this.rounds.Add(round);
                var roundNumber = this.rounds.Count;

                this.roundGroupsHelper.Add(round, new List<ListViewGroup>());
                this.roundItemsHelper.Add(round, new List<ListViewItem>());

                var i = 1;
                foreach (var pairing in round.Pairings)
                {
                    var group = new ListViewGroup("Round " + roundNumber + ", Pairing " + i);
                    this.RoundsList.Groups.Add(group);
                    this.roundGroupsHelper[round].Add(group);

                    foreach(var teamScore in pairing.TeamScores)
                    {
                        var item = new ListViewItem(new string[] { teamScore.Score == null ? "" : teamScore.Score.ToString(), this.teamNames[teamScore.Team.TeamId] });
                        item.Tag = teamScore;
                        item.Group = group;
                        this.RoundsList.Items.Add(item);
                        this.roundItemsHelper[round].Add(item);

                        if (!this.teamItemsHelper.ContainsKey(teamScore.Team.TeamId))
                        {
                            this.teamItemsHelper.Add(teamScore.Team.TeamId, new List<ListViewItem>());
                        }

                        this.teamItemsHelper[teamScore.Team.TeamId].Add(item);
                    }

                    i++;
                }

                this.UpdateState();
            }
        }

        private void RollBack_Click(object sender, EventArgs e)
        {
            var round = this.rounds.Last();

            this.rounds.Remove(round);

            foreach (var roundItem in this.roundItemsHelper[round])
            {
                this.RoundsList.Items.Remove(roundItem);
                var teamItems = from th in this.teamItemsHelper.Keys
                                where this.teamItemsHelper[th].Contains(roundItem)
                                select this.teamItemsHelper[th];
                foreach (var item in teamItems)
                {
                    item.Remove(roundItem);
                }
            }

            foreach (var roundGroup in this.roundGroupsHelper[round])
            {
                this.RoundsList.Groups.Remove(roundGroup);
            }
            this.UpdateState();
        }

        private void RoundsList_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            var item = RoundsList.Items[e.Item];
            var teamScore = (TournamentTeamScore)item.Tag;

            if (object.ReferenceEquals(e.Label, null))
            {
                return;
            }
            if (string.IsNullOrEmpty(e.Label))
            {
                teamScore.Score = null;
            }
            else
            {
                double score = 0.0;
                if (!double.TryParse(e.Label, out score))
                {
                    e.CancelEdit = true;
                    return;
                }

                teamScore.Score = new HighestPointsScore(score);
            }

            this.UpdateState();
        }

        private void RoundsList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.RoundsList.SelectedItems.Count > 0)
                {
                    this.RoundsList.SelectedItems[0].BeginEdit();
                }
            }
        }
    }
}
