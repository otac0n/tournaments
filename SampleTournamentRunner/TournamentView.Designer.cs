namespace Tournaments.Sample
{
    partial class TournamentView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TeamsList = new System.Windows.Forms.ListView();
            this.TeamColumn = new System.Windows.Forms.ColumnHeader();
            this.RatingColumn = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.AddTeam = new System.Windows.Forms.Button();
            this.RoundsList = new System.Windows.Forms.ListView();
            this.RoundTeamColumn = new System.Windows.Forms.ColumnHeader();
            this.RoundScoreColumn = new System.Windows.Forms.ColumnHeader();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.VisualizerScrollPanel = new System.Windows.Forms.Panel();
            this.Visualization = new System.Windows.Forms.PictureBox();
            this.VisualizerScrollPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Visualization)).BeginInit();
            this.SuspendLayout();
            // 
            // TeamsList
            // 
            this.TeamsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.TeamsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TeamColumn,
            this.RatingColumn});
            this.TeamsList.LabelEdit = true;
            this.TeamsList.Location = new System.Drawing.Point(12, 25);
            this.TeamsList.Name = "TeamsList";
            this.TeamsList.Size = new System.Drawing.Size(178, 249);
            this.TeamsList.TabIndex = 1;
            this.TeamsList.UseCompatibleStateImageBehavior = false;
            this.TeamsList.View = System.Windows.Forms.View.Details;
            this.TeamsList.DoubleClick += new System.EventHandler(this.TeamsList_DoubleClick);
            // 
            // TeamColumn
            // 
            this.TeamColumn.Text = "Team";
            this.TeamColumn.Width = 98;
            // 
            // RatingColumn
            // 
            this.RatingColumn.Text = "Rating/Seed";
            this.RatingColumn.Width = 76;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Teams:";
            // 
            // AddTeam
            // 
            this.AddTeam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddTeam.Location = new System.Drawing.Point(12, 280);
            this.AddTeam.Name = "AddTeam";
            this.AddTeam.Size = new System.Drawing.Size(75, 23);
            this.AddTeam.TabIndex = 2;
            this.AddTeam.Text = "&Add";
            this.AddTeam.UseVisualStyleBackColor = true;
            this.AddTeam.Click += new System.EventHandler(this.AddTeam_Click);
            // 
            // RoundsList
            // 
            this.RoundsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.RoundsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.RoundTeamColumn,
            this.RoundScoreColumn});
            this.RoundsList.Location = new System.Drawing.Point(196, 25);
            this.RoundsList.Name = "RoundsList";
            this.RoundsList.Size = new System.Drawing.Size(219, 249);
            this.RoundsList.TabIndex = 4;
            this.RoundsList.UseCompatibleStateImageBehavior = false;
            this.RoundsList.View = System.Windows.Forms.View.Details;
            // 
            // RoundTeamColumn
            // 
            this.RoundTeamColumn.Text = "Team";
            // 
            // RoundScoreColumn
            // 
            this.RoundScoreColumn.Text = "Score";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(196, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "&Rounds:";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(196, 280);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Start &Next";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.Location = new System.Drawing.Point(277, 280);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Roll &Back";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // VisualizerScrollPanel
            // 
            this.VisualizerScrollPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.VisualizerScrollPanel.AutoScroll = true;
            this.VisualizerScrollPanel.Controls.Add(this.Visualization);
            this.VisualizerScrollPanel.Location = new System.Drawing.Point(421, 25);
            this.VisualizerScrollPanel.Name = "VisualizerScrollPanel";
            this.VisualizerScrollPanel.Size = new System.Drawing.Size(281, 249);
            this.VisualizerScrollPanel.TabIndex = 7;
            // 
            // Visualization
            // 
            this.Visualization.Location = new System.Drawing.Point(0, 0);
            this.Visualization.Name = "Visualization";
            this.Visualization.Size = new System.Drawing.Size(10, 10);
            this.Visualization.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.Visualization.TabIndex = 0;
            this.Visualization.TabStop = false;
            // 
            // TournamentView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 315);
            this.Controls.Add(this.VisualizerScrollPanel);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RoundsList);
            this.Controls.Add(this.AddTeam);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TeamsList);
            this.Name = "TournamentView";
            this.Text = "Run Tournament";
            this.VisualizerScrollPanel.ResumeLayout(false);
            this.VisualizerScrollPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Visualization)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView TeamsList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddTeam;
        private System.Windows.Forms.ListView RoundsList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel VisualizerScrollPanel;
        private System.Windows.Forms.PictureBox Visualization;
        private System.Windows.Forms.ColumnHeader TeamColumn;
        private System.Windows.Forms.ColumnHeader RatingColumn;
        private System.Windows.Forms.ColumnHeader RoundTeamColumn;
        private System.Windows.Forms.ColumnHeader RoundScoreColumn;
    }
}

