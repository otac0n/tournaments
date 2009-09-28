using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tournaments.Sample
{
    public partial class EditTeamView : Form
    {
        public string TeamName { get; private set; }

        public int Rating { get; private set; }

        public EditTeamView(string teamName, int rating)
        {
            InitializeComponent();

            this.TeamName = teamName;
            this.Rating = rating;

            this.TeamText.Text = this.TeamName;
            this.RatingText.Text = this.Rating.ToString();
            this.UpdateOk();
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            this.TeamName = this.TeamText.Text;
            this.Rating = int.Parse(this.RatingText.Text);
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TeamText_TextChanged(object sender, EventArgs e)
        {
            this.UpdateOk();
        }

        private void RatingText_TextChanged(object sender, EventArgs e)
        {
            this.UpdateOk();
        }

        private void UpdateOk()
        {
            int temp = 0;
            Ok.Enabled = !string.IsNullOrEmpty(this.TeamText.Text) && int.TryParse(this.RatingText.Text, out temp);
        }
    }
}
