//-----------------------------------------------------------------------
// <copyright file="SingleEliminationTournament.cs" company="(none)">
//  Copyright (c) 2009 John Gietzen
//
//  Permission is hereby granted, free of charge, to any person obtaining
//  a copy of this software and associated documentation files (the
//  "Software"), to deal in the Software without restriction, including
//  without limitation the rights to use, copy, modify, merge, publish,
//  distribute, sublicense, and/or sell copies of the Software, and to
//  permit persons to whom the Software is furnished to do so, subject to
//  the following conditions:
//
//  The above copyright notice and this permission notice shall be
//  included in all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS
//  BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN
//  ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
//  CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//  SOFTWARE
// </copyright>
// <author>John Gietzen</author>
//-----------------------------------------------------------------------

namespace Tournaments.Standard
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Xml;
    using System.Globalization;
    using Tournaments.Graphics;

    /// <summary>
    /// Implements a Singe Elmination Tournament
    /// </summary>
    public class SingleEliminationTournament : IPairingsGenerator, ITournamentVisualizer
    {
        private List<TournamentTeam> loadedTeams;
        private List<SingleEliminationNode> loadedNodes;
        private PairingsGeneratorState state = PairingsGeneratorState.NotInitialized;

        public string Name
        {
            get
            {
                return "Single-elimination";
            }
        }

        public PairingsGeneratorState State
        {
            get
            {
                return this.state;
            }
        }

        public bool SupportsLateEntry
        {
            get
            {
                return false;
            }
        }

        public void Reset()
        {
            this.loadedTeams = null;
            this.loadedNodes = null;
            this.state = PairingsGeneratorState.NotInitialized;
        }

        public void LoadState(IEnumerable<TournamentTeam> teams, IList<TournamentRound> rounds)
        {
            if (teams == null)
            {
                throw new ArgumentNullException("teams");
            }

            if (rounds == null)
            {
                throw new ArgumentNullException("rounds");
            }

            List<SingleEliminationNode> nodes = new List<SingleEliminationNode>();

            if (teams.Count() >= 2)
            {
                int ranking = 0;
                var teamsOrder = from team in teams
                                 orderby team.Rating.HasValue ? team.Rating : 0 descending
                                 select new TeamRanking
                                 {
                                     Team = team,
                                     Ranking = ranking++,
                                 };

                int i = 0;
                int nextRoundAt = 2;
                int roundNumber = 0;
                int mask = (1 << roundNumber) - 1;
                foreach (var team in teamsOrder)
                {
                    SingleEliminationNode newNode = new SingleEliminationNode(team);
                    newNode.Locked = true;

                    if (nodes.Count == 0)
                    {
                        nodes.Add(newNode);
                        i++;
                        continue;
                    }

                    var match = (from n in nodes
                                 where n.Team != null
                                 where (n.Team.Ranking & mask) == (team.Ranking & mask)
                                 select n).Single();

                    match.MakeSiblingB(newNode);
                    nodes.Add(newNode);
                    nodes.Add(match.Parent);

                    i++;

                    if (i == nextRoundAt)
                    {
                        nextRoundAt *= 2;
                        roundNumber += 1;
                        mask = (1 << roundNumber) - 1;
                    }
                }

                // Move all byes even with the left side of the bracket.
                var maxLevel = nodes.Max(n => n.Level);
                var byeNodes = (from n in nodes
                                where n.Team != null
                                where n.Level != maxLevel
                                select n).ToList();
                foreach (var node in byeNodes)
                {
                    node.MakeSiblingB(null);
                    nodes.Add(node.Parent);
                }
            }

            bool byePaired = false;
            bool byesLocked = false;

            foreach (var round in rounds)
            {
                foreach (var pairing in round.Pairings)
                {
                    if (pairing.TeamScores.Count > 2)
                    {
                        throw new InvalidTournamentStateException("At least one pairing had more than two teams competing.  This is invalid in a single elimination tournament.");
                    }

                    if (pairing.TeamScores.Count == 0)
                    {
                        continue;
                    }

                    if (pairing.TeamScores.Count == 1)
                    {
                        byePaired = true;

                        var team = pairing.TeamScores[0].Team;

                        var byes = from n in nodes
                                   where n.ChildA != null
                                   where n.ChildB == null
                                   select n;

                        var avail = from b in byes
                                    where b.Locked == false
                                    select b;

                        var matched = from a in avail
                                      where a.ChildAMatches(team.TeamId)
                                      select a;

                        if (matched.Count() == 1)
                        {
                            var node = matched.Single();
                            node.Locked = true;
                        }
                        else
                        {
                            // We did not find a matching bye, so we have to create one by swapping.
                            if (avail.Count() == 0)
                            {
                                throw new InvalidTournamentStateException("A bye was listed but no valid bye could be created.");
                            }

                            var byeNode = avail.First();

                            // Find our team in an unlocked node
                            var foundA = from n in nodes
                                         where n.Locked == false
                                         where n.ChildAMatches(team.TeamId)
                                         select n;

                            var foundB = from n in nodes
                                         where n.Locked == false
                                         where n.ChildBMatches(team.TeamId)
                                         select n;

                            // swap out the found node for our bye node.
                            if (foundA.Count() == 1)
                            {
                                var swapNode = foundA.Single();
                                var a = swapNode.ChildA;
                                swapNode.ChildA = byeNode.ChildA;
                                byeNode.ChildA = a;
                            }
                            else if (foundB.Count() == 1)
                            {
                                var swapNode = foundB.Single();
                                var b = swapNode.ChildB;
                                swapNode.ChildB = byeNode.ChildA;
                                byeNode.ChildA = b;
                            }
                            else
                            {
                                throw new InvalidTournamentStateException("A bye was listed for a team that was ineligible for a bye.");
                            }

                            byeNode.Locked = true;
                        }
                    }
                    else
                    {
                        var scoreA = pairing.TeamScores[0].Score;
                        var teamA = pairing.TeamScores[0].Team;
                        var scoreB = pairing.TeamScores[1].Score;
                        var teamB = pairing.TeamScores[1].Team;

                    tryagainwithbyespaired:
                        if (byePaired && !byesLocked)
                        {
                            LockByes(nodes);
                            byesLocked = true;
                        }

                        var pairs = from n in nodes
                                    where n.ChildA != null
                                    where n.ChildB != null
                                    select n;

                        var avail = from p in pairs
                                    where p.Locked == false
                                    select p;

                        var matched = from a in avail
                                      where a.ChildAMatches(teamA.TeamId)
                                      where a.ChildBMatches(teamB.TeamId)
                                      select a;

                        if (matched.Count() == 1)
                        {
                            var node = matched.Single();
                            node.ChildA.Score = scoreA;
                            node.ChildB.Score = scoreB;
                            node.Locked = true;
                        }
                        else
                        {
                            // We did not find a matching pair, so we need to create one by swapping.

                            var teamANodes = from n in nodes
                                             where n.Locked == false
                                             where n.ChildAMatches(teamA.TeamId) || n.ChildBMatches(teamA.TeamId)
                                             select n;

                            var teamBNodes = from n in nodes
                                             where n.Locked == false
                                             where n.ChildAMatches(teamB.TeamId) || n.ChildBMatches(teamB.TeamId)
                                             select n;

                            if (teamANodes.Count() != 1 || teamBNodes.Count() != 1)
                            {
                                throw new InvalidTournamentStateException("At least one pairing involved a team that was not eligible to play.");
                            }

                            var teamANode = teamANodes.Single();
                            var teamBNode = teamBNodes.Single();

                            if (teamANode == teamBNode)
                            {
                                // If the order was merely swapped, swap it back.
                                teamANode.SwapChildren();
                                teamANode.ChildA.Score = scoreA;
                                teamANode.ChildB.Score = scoreB;
                                teamANode.Locked = true;
                            }
                            else
                            {
                                if (teamANode.Level != teamBNode.Level)
                                {
                                    if (byePaired == false)
                                    {
                                        byePaired = true;
                                        goto tryagainwithbyespaired;
                                    }

                                    throw new InvalidTournamentStateException("At least one pairing involved two teams that were not eligible to play against each other.");
                                }

                                // find the first available node in this level.
                                var available = from n in nodes
                                                where n.Locked == false
                                                where n.Level == teamANode.Level
                                                where n.ChildA != null
                                                where n.ChildB != null
                                                select n;

                                if (available.Count() == 0)
                                {
                                    if (byePaired == false)
                                    {
                                        byePaired = true;
                                        goto tryagainwithbyespaired;
                                    }

                                    throw new InvalidTournamentStateException("At least one pairing contained two teams when the tournament was unable to support a pairing with two teams.");
                                }

                                var destination = available.First();

                                if (teamANode.ChildA != null && teamANode.ChildA.Team != null && teamANode.ChildA.Team.Team.TeamId == teamA.TeamId)
                                {
                                    // swap destination A with teamANode A
                                    SwapChildrenAA(destination, teamANode);
                                }
                                else if (teamANode.ChildB != null && teamANode.ChildB.Team != null && teamANode.ChildB.Team.Team.TeamId == teamA.TeamId)
                                {
                                    // swap destination A with teamANode B
                                    SwapChildrenAB(destination, teamANode);
                                }
                                else
                                {
                                    throw new InvalidOperationException();
                                }

                                // since destination could've matched teamBNode and we may have swapped it out, we need to refetch teamBNode
                                teamBNode = teamBNodes.Single();

                                if (teamBNode.ChildA != null && teamBNode.ChildA.Team != null && teamBNode.ChildA.Team.Team.TeamId == teamB.TeamId)
                                {
                                    // swap destination B with teamBNode A
                                    SwapChildrenBA(destination, teamBNode);
                                }
                                else if (teamBNode.ChildB != null && teamBNode.ChildB.Team != null && teamBNode.ChildB.Team.Team.TeamId == teamB.TeamId)
                                {
                                    // swap destination B with teamBNode B
                                    SwapChildrenBB(destination, teamBNode);
                                }
                                else
                                {
                                    throw new InvalidOperationException();
                                }

                                destination.ChildA.Score = scoreA;
                                destination.ChildB.Score = scoreB;
                                destination.Locked = true;
                            }
                        }
                    }
                }
            }

            LockByes(nodes);

            this.loadedNodes = nodes;
            this.loadedTeams = new List<TournamentTeam>(teams);
            this.state = PairingsGeneratorState.Initialized;
        }

        private static void SwapChildrenAA(SingleEliminationNode node1, SingleEliminationNode node2)
        {
            var temp1 = node1.ChildA;
            node1.ChildA = null;
            var temp2 = node2.ChildA;
            node2.ChildA = null;

            node1.ChildA = temp2;
            node2.ChildA = temp1;
        }

        private static void SwapChildrenAB(SingleEliminationNode node1, SingleEliminationNode node2)
        {
            var temp1 = node1.ChildA;
            node1.ChildA = null;
            var temp2 = node2.ChildB;
            node2.ChildB = null;

            node1.ChildA = temp2;
            node2.ChildB = temp1;
        }

        private static void SwapChildrenBA(SingleEliminationNode node1, SingleEliminationNode node2)
        {
            var temp1 = node1.ChildB;
            node1.ChildB = null;
            var temp2 = node2.ChildA;
            node2.ChildA = null;

            node1.ChildB = temp2;
            node2.ChildA = temp1;
        }

        private static void SwapChildrenBB(SingleEliminationNode node1, SingleEliminationNode node2)
        {
            var temp1 = node1.ChildB;
            node1.ChildB = null;
            var temp2 = node2.ChildB;
            node2.ChildB = null;

            node1.ChildB = temp2;
            node2.ChildB = temp1;
        }

        private static void LockByes(List<SingleEliminationNode> nodes)
        {
            var unlockedByes = from n in nodes
                               where n.Locked == false
                               where n.ChildA != null
                               where n.ChildB == null
                               select n;

            foreach (var u in unlockedByes)
            {
                u.Locked = true;
            }
        }

        public TournamentRound CreateNextRound(int? places)
        {
            if (places.HasValue && places.Value < 0)
            {
                throw new ArgumentException("You must specify a number of places that is greater than zero.", "places");
            }

            if (this.state != PairingsGeneratorState.Initialized)
            {
                throw new InvalidTournamentStateException("This generator was never successfully initialized with a valid tournament state.");
            }

            var readyToPlay = from n in this.loadedNodes
                              where n.Locked == false
                              where n.ChildA != null && n.ChildA.Team != null
                              where n.ChildB != null && n.ChildB.Team != null
                              orderby n.Level descending
                              select new TournamentPairing(
                                  new TournamentTeamScore(n.ChildA.Team.Team, null),
                                  new TournamentTeamScore(n.ChildB.Team.Team, null));

            if (readyToPlay.Count() == 0)
            {
                // if this is because the root is locked, return null
                // otherwise, return an error because there is either a tie or an unfinished round.

                var ties = from n in this.loadedNodes
                           where n.Locked == true
                           where n.ChildA != null && n.ChildB != null
                           where n.ChildA.Score != null && n.ChildB.Score != null
                           where n.ChildA.Score == n.ChildB.Score
                           select n;

                if (ties.Count() > 0)
                {
                    throw new InvalidTournamentStateException("The tournament cannot continue because there is at least one pairing still resulting in a tie.  Ties are not allowed in single elimintaion tournaments.");
                }

                var unfinished = from n in this.loadedNodes
                                 where n.Locked == true
                                 where n.ChildA != null && n.ChildB != null
                                 where n.ChildA.Score == null || n.ChildB.Score == null
                                 select n;

                if (unfinished.Count() > 0)
                {
                    throw new InvalidTournamentStateException("The tournament cannot continue because there is at least one pairing that remains unfinished.");
                }

                return null;
            }

            if (places.HasValue)
            {
                return new TournamentRound(readyToPlay.Take(places.Value).ToList());
            }
            else
            {
                return new TournamentRound(readyToPlay.ToList());
            }
        }

        public IEnumerable<TournamentRanking> GenerateRankings()
        {
            if (this.loadedTeams.Count >= 2)
            {
                var maxLevel = this.loadedNodes.Max(n => n.Level);

                var ranks = from t in this.loadedTeams
                            let node = FindTeamsHighestNode(t.TeamId)
                            let finished = node.Locked && node.HasWinner
                            let winner = node.Team != null && node.Team.Team.TeamId == t.TeamId
                            let round = maxLevel - node.Level
                            let rank = node.Level + (winner ? 1 : 2)
                            orderby rank
                            select new TournamentRanking(t, rank, (finished ? (winner ? "Winner" : "Eliminated in heat " + round) : "Continuing"));

                return ranks;
            }
            else
            {
                throw new InvalidTournamentStateException("The tournament is not in a state that allows ranking.");
            }
        }

        private SingleEliminationNode FindTeamsHighestNode(long teamId)
        {
            return (from n in this.loadedNodes
                    where n.ChildAMatches(teamId) || n.ChildBMatches(teamId)
                    orderby n.Level
                    select n).FirstOrDefault();
        }

        public SizeF Measure(IGraphics graphics, TournamentNameTable teamNames)
        {
            var rootNode = (from n in this.loadedNodes
                            where n.Parent == null
                            select n).SingleOrDefault();

            if (rootNode == null)
            {
                return new Size(0, 0);
            }

            var textHeight = GetTextHeight(graphics);

            var size = this.MeasureNode(rootNode, textHeight);

            return new SizeF(size.Width + 10, size.Height + 10);
        }

        public void Render(IGraphics graphics, TournamentNameTable teamNames)
        {
            var rootNode = (from n in this.loadedNodes
                            where n.Parent == null
                            select n).SingleOrDefault();

            if (rootNode == null)
            {
                return;
            }

            var textHeight = GetTextHeight(graphics);

            this.RenderNode(graphics, textHeight, rootNode, 5, 5, teamNames);
        }

        private const float MinBracketWidth = 120;
        private const float BracketPreIndent = 10;
        private const float BracketPostIndent = 10;
        private const float BracketVSpacing = 25;
        private Pen BracketPen = new Pen(Color.Black, 1.0f);
        private const float MinTextHeight = 20;
        private const float TextYOffset = 3;
        private const float TextXOffset = 3;
        private Brush UserboxBrush = new SolidBrush(Color.FromArgb(220, 220, 220));
        private Font UserboxFont = new Font(FontFamily.GenericSansSerif, 10.0f);
        private Brush UserboxFontBrush = new SolidBrush(Color.Black);

        private float GetTextHeight(IGraphics g)
        {
            return Math.Max(g.MeasureString("abfgijlpqyAIJQ170,`'\"", UserboxFont).Height + TextYOffset * 2, MinTextHeight);
        }

        private void RenderNode(IGraphics g, float textHeight, SingleEliminationNode rootNode, float x, float y, TournamentNameTable teamNames)
        {
            var m = this.MeasureNode(rootNode, textHeight);

            if (rootNode.ChildA != null && rootNode.ChildB != null)
            {
                // Preline
                g.DrawLine(
                    BracketPen,
                    new PointF(
                        x + (m.Width - MinBracketWidth),
                        y + m.CenterLine),
                    new PointF(
                        x + (m.Width - MinBracketWidth - BracketPreIndent),
                        y + m.CenterLine));

                var childASize = this.MeasureNode(rootNode.ChildA, textHeight);
                var childBSize = this.MeasureNode(rootNode.ChildB, textHeight);

                // V-Line
                g.DrawLine(
                    BracketPen, 
                    new PointF(
                        x + (m.Width - MinBracketWidth - BracketPreIndent),
                        y + childASize.CenterLine),
                    new PointF(
                        x + (m.Width - MinBracketWidth - BracketPreIndent),
                        y + childASize.Height + BracketVSpacing + childBSize.CenterLine));

                // Post-Line-A
                g.DrawLine(
                    BracketPen,
                    new PointF(
                        x + (m.Width - MinBracketWidth - BracketPreIndent - BracketPostIndent),
                        y + childASize.CenterLine),
                    new PointF(
                        x + (m.Width - MinBracketWidth - BracketPreIndent),
                        y + childASize.CenterLine));

                // Post-Line-B
                g.DrawLine(
                    BracketPen,
                    new PointF(
                        x + (m.Width - MinBracketWidth - BracketPreIndent - BracketPostIndent),
                        y + childASize.Height + BracketVSpacing + childBSize.CenterLine),
                    new PointF(
                        x + (m.Width - MinBracketWidth - BracketPreIndent),
                        y + childASize.Height + BracketVSpacing + childBSize.CenterLine));

                this.RenderNode(g, textHeight, rootNode.ChildA, x, y, teamNames);
                this.RenderNode(g, textHeight, rootNode.ChildB, x, y + childASize.Height + BracketVSpacing, teamNames);
            }

            g.FillRectangle(
                UserboxBrush,
                new RectangleF(
                    new PointF(
                        x + (m.Width - MinBracketWidth),
                        y + m.CenterLine - (textHeight / 2)),
                    new SizeF(
                        MinBracketWidth,
                        textHeight)));

            
            if (rootNode.Team != null)
            {
                g.DrawString(
                    teamNames[rootNode.Team.Team.TeamId],
                    UserboxFont,
                    UserboxFontBrush,
                    new PointF(
                        x + (m.Width - MinBracketWidth) + TextXOffset,
                        y + m.CenterLine - (textHeight / 2) + TextYOffset));
            }

            if (rootNode.Score != null)
            {
                var score = rootNode.Score.ToString();

                var scoreWidth = g.MeasureString(score, UserboxFont).Width;

                g.DrawString(
                    score,
                    UserboxFont,
                    UserboxFontBrush,
                    new PointF(
                        x + m.Width - scoreWidth - TextXOffset,
                        y + m.CenterLine - (textHeight / 2) + TextYOffset));
            }
        }

        private NodeMeasurement MeasureNode(SingleEliminationNode rootNode, float textHeight)
        {
            float width = MinBracketWidth;
            float height = textHeight;
            float center = textHeight / 2;

            if (rootNode.ChildA != null && rootNode.ChildB != null)
            {
                var a = this.MeasureNode(rootNode.ChildA, textHeight);
                var b = this.MeasureNode(rootNode.ChildB, textHeight);
                height = Math.Max(a.Height + BracketVSpacing + b.Height, height);
                width = width + Math.Max(a.Width, b.Width) + BracketPreIndent + BracketPostIndent;
                center = (a.CenterLine + BracketVSpacing + b.CenterLine + a.Height) / 2;
            }
            else if (rootNode.ChildA != null)
            {
                var a = this.MeasureNode(rootNode.ChildA, textHeight);
                width = width + a.Width + BracketPreIndent + BracketPostIndent;
                height = Math.Max(a.Height, height);
                center = a.CenterLine;
            }
            else if (rootNode.ChildB != null)
            {
                var b = this.MeasureNode(rootNode.ChildB, textHeight);
                width = width + b.Width + BracketPreIndent + BracketPostIndent;
                height = Math.Max(b.Height, height);
                center = b.CenterLine;
            }

            return new NodeMeasurement
            {
                Height = height,
                Width = width,
                CenterLine = center
            };
        }

        private class NodeMeasurement
        {
            public float Width
            {
                get;
                set;
            }

            public float Height
            {
                get;
                set;
            }

            public float CenterLine
            {
                get;
                set;
            }
        }
    }
}
