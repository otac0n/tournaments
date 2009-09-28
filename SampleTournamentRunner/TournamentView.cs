using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tournaments.Standard;

namespace Tournaments.Sample
{
    public partial class TournamentView : Form
    {
        private IPairingsGenerator generator;
        private ITournamentVisualizer visualizer;
        private SystemGraphics measureGraphics = new SystemGraphics(System.Drawing.Graphics.FromImage(new Bitmap(1, 1)));
        private List<TournamentTeam> teams = new List<TournamentTeam>();
        private List<TournamentRound> rounds = new List<TournamentRound>();
        private Dictionary<long, string> teamNames = new Dictionary<long, string>();
        int teamIdentity = 0;

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

            try
            {
                this.generator.Reset();
                this.generator.LoadState(this.teams, this.rounds);

                var round = this.generator.CreateNextRound(null);
                if (round != null)
                {
                    nextRoundAvailable = true;
                }

                if (this.visualizer != null)
                {
                    var names = new TournamentNameTable(this.teamNames);
                    var size = this.visualizer.Measure(this.measureGraphics, names);
                    if (size.Height > 0 && size.Width > 0)
                    {
                        var rendered = new Bitmap((int)Math.Ceiling(size.Width), (int)Math.Ceiling(size.Height));
                        this.visualizer.Render(new SystemGraphics(System.Drawing.Graphics.FromImage(rendered)), names);
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

            this.AddTeam.Enabled = !roundsPlayed;
            this.RollBack.Enabled = roundsPlayed;
            this.StartNext.Enabled = nextRoundAvailable;
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
            if (string.IsNullOrEmpty(e.Label))
            {
                e.CancelEdit = true;
                return;
            }

            var item = this.TeamsList.Items[e.Item];
            var team = (TournamentTeam)item.Tag;
            teamNames[team.TeamId] = e.Label;

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
    }
}
