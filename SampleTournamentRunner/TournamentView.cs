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

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (this.rendered != null)
            {
                e.Graphics.DrawImage(this.rendered, new Point(0, 0));
            }
        }
    }
}
