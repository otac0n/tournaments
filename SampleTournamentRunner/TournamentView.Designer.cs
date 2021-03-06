﻿namespace Tournaments.Sample
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
            this.RoundScoreColumn = new System.Windows.Forms.ColumnHeader();
            this.RoundTeamColumn = new System.Windows.Forms.ColumnHeader();
            this.label2 = new System.Windows.Forms.Label();
            this.StartNext = new System.Windows.Forms.Button();
            this.RollBack = new System.Windows.Forms.Button();
            this.VisualizerScrollPanel = new System.Windows.Forms.Panel();
            this.Visualization = new System.Windows.Forms.PictureBox();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.TournamentStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.StandingsList = new System.Windows.Forms.ListView();
            this.StandingRankHeader = new System.Windows.Forms.ColumnHeader();
            this.StandingTeamHeader = new System.Windows.Forms.ColumnHeader();
            this.StandingReasonHeader = new System.Windows.Forms.ColumnHeader();
            this.label3 = new System.Windows.Forms.Label();
            this.VisualizerScrollPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Visualization)).BeginInit();
            this.StatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // TeamsList
            // 
            this.TeamsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TeamColumn,
            this.RatingColumn});
            this.TeamsList.FullRowSelect = true;
            this.TeamsList.LabelEdit = true;
            this.TeamsList.Location = new System.Drawing.Point(12, 37);
            this.TeamsList.Name = "TeamsList";
            this.TeamsList.Size = new System.Drawing.Size(205, 171);
            this.TeamsList.TabIndex = 1;
            this.TeamsList.UseCompatibleStateImageBehavior = false;
            this.TeamsList.View = System.Windows.Forms.View.Details;
            this.TeamsList.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.TeamsList_AfterLabelEdit);
            this.TeamsList.DoubleClick += new System.EventHandler(this.TeamsList_DoubleClick);
            this.TeamsList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TeamsList_KeyDown);
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
            this.label1.Location = new System.Drawing.Point(9, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Teams:";
            // 
            // AddTeam
            // 
            this.AddTeam.Location = new System.Drawing.Point(142, 9);
            this.AddTeam.Name = "AddTeam";
            this.AddTeam.Size = new System.Drawing.Size(75, 22);
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
            this.RoundScoreColumn,
            this.RoundTeamColumn});
            this.RoundsList.FullRowSelect = true;
            this.RoundsList.LabelEdit = true;
            this.RoundsList.Location = new System.Drawing.Point(12, 242);
            this.RoundsList.Name = "RoundsList";
            this.RoundsList.Size = new System.Drawing.Size(205, 251);
            this.RoundsList.TabIndex = 4;
            this.RoundsList.UseCompatibleStateImageBehavior = false;
            this.RoundsList.View = System.Windows.Forms.View.Details;
            this.RoundsList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RoundsList_MouseClick);
            this.RoundsList.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.RoundsList_AfterLabelEdit);
            // 
            // RoundScoreColumn
            // 
            this.RoundScoreColumn.Text = "Score";
            this.RoundScoreColumn.Width = 66;
            // 
            // RoundTeamColumn
            // 
            this.RoundTeamColumn.Text = "Team";
            this.RoundTeamColumn.Width = 130;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 226);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "&Rounds:";
            // 
            // StartNext
            // 
            this.StartNext.Location = new System.Drawing.Point(61, 214);
            this.StartNext.Name = "StartNext";
            this.StartNext.Size = new System.Drawing.Size(75, 22);
            this.StartNext.TabIndex = 5;
            this.StartNext.Text = "Start &Next";
            this.StartNext.UseVisualStyleBackColor = true;
            this.StartNext.Click += new System.EventHandler(this.StartNext_Click);
            // 
            // RollBack
            // 
            this.RollBack.Location = new System.Drawing.Point(142, 214);
            this.RollBack.Name = "RollBack";
            this.RollBack.Size = new System.Drawing.Size(75, 22);
            this.RollBack.TabIndex = 6;
            this.RollBack.Text = "Roll &Back";
            this.RollBack.UseVisualStyleBackColor = true;
            this.RollBack.Click += new System.EventHandler(this.RollBack_Click);
            // 
            // VisualizerScrollPanel
            // 
            this.VisualizerScrollPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.VisualizerScrollPanel.AutoScroll = true;
            this.VisualizerScrollPanel.BackColor = System.Drawing.SystemColors.Window;
            this.VisualizerScrollPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VisualizerScrollPanel.Controls.Add(this.Visualization);
            this.VisualizerScrollPanel.Location = new System.Drawing.Point(223, 214);
            this.VisualizerScrollPanel.Name = "VisualizerScrollPanel";
            this.VisualizerScrollPanel.Size = new System.Drawing.Size(488, 279);
            this.VisualizerScrollPanel.TabIndex = 9;
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
            // StatusBar
            // 
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TournamentStatus});
            this.StatusBar.Location = new System.Drawing.Point(0, 496);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(723, 22);
            this.StatusBar.TabIndex = 10;
            // 
            // TournamentStatus
            // 
            this.TournamentStatus.Name = "TournamentStatus";
            this.TournamentStatus.Size = new System.Drawing.Size(42, 17);
            this.TournamentStatus.Text = "Ready.";
            // 
            // StandingsList
            // 
            this.StandingsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.StandingsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.StandingRankHeader,
            this.StandingTeamHeader,
            this.StandingReasonHeader});
            this.StandingsList.FullRowSelect = true;
            this.StandingsList.GridLines = true;
            this.StandingsList.Location = new System.Drawing.Point(223, 37);
            this.StandingsList.Name = "StandingsList";
            this.StandingsList.Size = new System.Drawing.Size(487, 171);
            this.StandingsList.TabIndex = 8;
            this.StandingsList.UseCompatibleStateImageBehavior = false;
            this.StandingsList.View = System.Windows.Forms.View.Details;
            // 
            // StandingRankHeader
            // 
            this.StandingRankHeader.Text = "Rank";
            // 
            // StandingTeamHeader
            // 
            this.StandingTeamHeader.Text = "Team";
            // 
            // StandingReasonHeader
            // 
            this.StandingReasonHeader.Text = "Reason";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(220, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Standings:";
            // 
            // TournamentView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 518);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.StandingsList);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.VisualizerScrollPanel);
            this.Controls.Add(this.RollBack);
            this.Controls.Add(this.StartNext);
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
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView TeamsList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddTeam;
        private System.Windows.Forms.ListView RoundsList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button StartNext;
        private System.Windows.Forms.Button RollBack;
        private System.Windows.Forms.Panel VisualizerScrollPanel;
        private System.Windows.Forms.PictureBox Visualization;
        private System.Windows.Forms.ColumnHeader TeamColumn;
        private System.Windows.Forms.ColumnHeader RatingColumn;
        private System.Windows.Forms.ColumnHeader RoundTeamColumn;
        private System.Windows.Forms.ColumnHeader RoundScoreColumn;
        private System.Windows.Forms.StatusStrip StatusBar;
        private System.Windows.Forms.ToolStripStatusLabel TournamentStatus;
        private System.Windows.Forms.ListView StandingsList;
        private System.Windows.Forms.ColumnHeader StandingRankHeader;
        private System.Windows.Forms.ColumnHeader StandingTeamHeader;
        private System.Windows.Forms.ColumnHeader StandingReasonHeader;
        private System.Windows.Forms.Label label3;
    }
}

