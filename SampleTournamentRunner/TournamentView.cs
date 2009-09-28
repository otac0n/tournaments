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
        private Bitmap rendered;

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
        }

        public void Run()
        {
            Application.Run(this);
        }

        private void AddTeam_Click(object sender, EventArgs e)
        {
            var item = new ListViewItem(new string[] { "New Team", 100.ToString() });
            this.TeamsList.Items.Add(item);
            item.BeginEdit();
        }

        private void TeamsList_DoubleClick(object sender, EventArgs e)
        {
            if (this.TeamsList.SelectedIndices.Count != 0)
            {
                var item = this.TeamsList.SelectedItems[0];
                string name = item.SubItems[0].Text;
                int rating = int.Parse(item.SubItems[1].Text);

                using (EditTeamView dlg = new EditTeamView(name, rating))
                {
                    dlg.ShowDialog();
                    item.SubItems[0].Text = dlg.TeamName;
                    item.SubItems[1].Text = dlg.Rating.ToString();
                }
            }
        }
    }
}
