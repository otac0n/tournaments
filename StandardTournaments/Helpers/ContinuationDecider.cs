//-----------------------------------------------------------------------
// <copyright file="ContinuationDecider.cs" company="(none)">
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
// <author>Katie Johnson</author>
// <author>John Gietzen</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Tournaments.Graphics;

namespace Tournaments.Standard
{
    public class ContinuationDecider : EliminationDecider
    {
        private EliminationNode nodeA = null;
        private EliminationNode nodeB = null;

        public ContinuationDecider(EliminationNode nodeA, EliminationNode nodeB)
        {
            this.nodeA = nodeA ?? throw new ArgumentNullException(nameof(nodeA));
            this.nodeB = nodeB ?? throw new ArgumentNullException(nameof(nodeB));
        }

        public EliminationNode ChildA
        {
            get
            {
                return this.nodeA;
            }

            set
            {
                this.nodeA = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public EliminationNode ChildB
        {
            get
            {
                return this.nodeB;
            }

            set
            {
                this.nodeB = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        /// <inheritdoc />
        public override bool IsDecided
        {
            get
            {
                return this.nodeA.IsDecided && nodeB.IsDecided && ((nodeA.Team == null || nodeB.Team == null) || (nodeA.Score != null && nodeB.Score != null && nodeA.Score != nodeB.Score));
            }
        }

        /// <inheritdoc />
        public override TournamentTeam GetWinner()
        {
            if (!this.nodeA.IsDecided)
            {
                throw new InvalidOperationException("Cannot determine a winner from a node that is undecided.");
            }

            if (!this.nodeB.IsDecided)
            {
                throw new InvalidOperationException("Cannot determine a winner from a node that is undecided.");
            }

            if (this.nodeB.Team == null)
            {
                return this.nodeA.Team;
            }
            else if (this.nodeA.Team == null)
            {
                return this.nodeB.Team;
            }

            if (this.nodeA.Score == null)
            {
                throw new InvalidOperationException("Cannot determine a winner from a node without a score.");
            }

            if (this.nodeB.Score == null)
            {
                throw new InvalidOperationException("Cannot determine a winner from a node without a score.");
            }

            if (this.nodeA.Score == this.nodeB.Score)
            {
                throw new InvalidOperationException("Cannot determine a winner from between two nodes with the same score.");
            }

            return this.nodeA.Score > this.nodeB.Score ? this.nodeA.Team : this.nodeB.Team;
        }

        /// <inheritdoc />
        public override TournamentTeam GetLoser()
        {
            if (!this.nodeA.IsDecided)
            {
                throw new InvalidOperationException("Cannot determine a loser from a node that is undecided.");
            }

            if (!this.nodeB.IsDecided)
            {
                throw new InvalidOperationException("Cannot determine a loser from a node that is undecided.");
            }

            if (this.nodeA.Team == null)
            {
                return this.nodeA.Team;
            }
            else if (this.nodeB.Team == null)
            {
                return this.nodeB.Team;
            }

            if (this.nodeA.Score == null)
            {
                throw new InvalidOperationException("Cannot determine a loser from a node without a score.");
            }

            if (this.nodeB.Score == null)
            {
                throw new InvalidOperationException("Cannot determine a loser from a node without a score.");
            }

            if (this.nodeA.Score == this.nodeB.Score)
            {
                throw new InvalidOperationException("Cannot determine a loser from between two nodes with the same score.");
            }

            return this.nodeA.Score > this.nodeB.Score ? this.nodeB.Team : this.nodeA.Team;
        }

        public bool HasTeam(TournamentTeam team)
        {
            return this.HasTeam(team.TeamId);
        }

        public bool HasTeam(long teamId)
        {
            return (this.ChildA != null && this.ChildA.IsDecided && this.ChildA.Team.TeamId == teamId) || (this.ChildB != null && this.ChildB.IsDecided && this.ChildB.Team.TeamId == teamId);
        }

        public void SwapChildren()
        {
            var tempNode = this.nodeA;
            this.nodeA = this.nodeB;
            this.nodeB = tempNode;
        }

        /// <inheritdoc />
        public override NodeMeasurement MeasureWinner(Tournaments.Graphics.IGraphics g, TournamentNameTable names, float textHeight, Score score)
        {
            string teamName = "";
            if (this.IsDecided)
            {
                var winner = this.GetWinner();
                if (winner != null)
                {
                    teamName = names[winner.TeamId];
                }
                else
                {
                    teamName = "bye";
                }
            }

            var t = this.MeasureTextBox(g, textHeight, teamName, score);

            var mA = this.nodeA.Measure(g, names, textHeight);
            var mB = this.nodeB.Measure(g, names, textHeight);

            if (mA == null && mB == null)
            {
                return null;
            }
            else if (mB == null)
            {
                return t;
            }
            else if (mA == null)
            {
                return t;
            }
            else
            {
                var m = this.MeasureTree(g, names, textHeight,
                    this.nodeA,
                    this.nodeB);

                return new NodeMeasurement(m.Width + t.Width, m.Height, m.CenterLine);
            }
        }

        /// <inheritdoc />
        public override NodeMeasurement MeasureLoser(IGraphics g, TournamentNameTable names, float textHeight, Score score)
        {
            string teamName = "";
            if (this.IsDecided)
            {
                var loser = this.GetLoser();
                if (loser != null)
                {
                    teamName = names[loser.TeamId];
                }
                else
                {
                    return null;
                }
            }

            return this.MeasureTextBox(g, textHeight, teamName, score);
        }

        /// <inheritdoc />
        public override void RenderWinner(IGraphics g, TournamentNameTable names, float x, float y, float textHeight, Score score)
        {
            var m = this.MeasureWinner(g, names, textHeight, score);

            string teamName = "";
            if (this.IsDecided)
            {
                var winner = this.GetWinner();
                if (winner != null)
                {
                    teamName = names[winner.TeamId];
                }
                else
                {
                    teamName = "bye";
                }
            }

            var t = this.MeasureTextBox(g, textHeight, teamName, score);

            this.RenderTextBox(g, x + m.Width - t.Width, y + m.CenterLine - t.CenterLine, textHeight, teamName, score);

            var mA = this.nodeA.Measure(g, names, textHeight);
            var mB = this.nodeB.Measure(g, names, textHeight);

            if (mA == null || mB == null)
            {
                return;
            }
            else
            {
                this.RenderTree(g, names, x, y, textHeight,
                    this.nodeA,
                    this.nodeB);
            }
        }

        /// <inheritdoc />
        public override void RenderLoser(IGraphics g, TournamentNameTable names, float x, float y, float textHeight, Score score)
        {
            string teamName = "";
            if (this.IsDecided)
            {
                var loser = this.GetLoser();
                if (loser != null)
                {
                    teamName = names[loser.TeamId];
                }
                else
                {
                    return;
                }
            }

            this.RenderTextBox(g, x, y, textHeight, teamName, score);
        }

        /// <inheritdoc />
        public override bool ApplyPairing(TournamentPairing pairing)
        {
            if (pairing == null)
            {
                throw new ArgumentNullException(nameof(pairing));
            }

            if (this.Locked)
            {
                // If we (and, all of our decentants) are decided, return false, indicating that no node below us is in a state that needs a score.
                return false;
            }

            if (!this.nodeA.IsDecided || !this.nodeA.Locked || !this.nodeB.IsDecided || !this.nodeB.Locked)
            {
                return (!this.nodeA.IsDecided && this.nodeA.ApplyPairing(pairing)) || (!this.nodeB.IsDecided && this.nodeB.ApplyPairing(pairing));
            }
            else
            {
                var teamA = pairing.TeamScores[0].Team;
                var scoreA = pairing.TeamScores[0].Score;
                var teamB = pairing.TeamScores[1].Team;
                var scoreB = pairing.TeamScores[1].Score;

                if (teamA == null)
                {
                    teamA = teamB;
                    scoreA = scoreB;
                    teamB = null;
                    scoreB = null;
                }

                if(!TeamsMatch(teamA, this.nodeA.Team) || !TeamsMatch(teamB, this.nodeB.Team))
                {
                    var teamSwap = teamA;
                    var scoreSwap = scoreA;
                    teamA = teamB;
                    scoreA = scoreB;
                    teamB = teamSwap;
                    scoreB = scoreSwap;
                }

                if (TeamsMatch(teamA, this.nodeA.Team) && TeamsMatch(teamB, this.nodeB.Team))
                {
                    this.nodeA.Score = scoreA;
                    this.nodeB.Score = scoreB;
                    this.Lock();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool TeamsMatch(TournamentTeam teamA, TournamentTeam teamB)
        {
            if (object.ReferenceEquals(teamA, teamB))
            {
                return true;
            }
            else if (teamA == null || teamB == null)
            {
                return false;
            }
            else
            {
                return (teamA.TeamId == teamB.TeamId);
            }
        }

        /// <inheritdoc />
        public override IEnumerable<TournamentPairing> FindUndecided()
        {
            if (this.IsDecided)
            {
                yield break;
            }
            else if (this.nodeA.IsDecided && this.nodeB.IsDecided)
            {
                if (this.Locked)
                {
                    yield break;
                }
                else
                {
                    yield return new TournamentPairing(
                            new TournamentTeamScore(this.nodeA.Team, null),
                            new TournamentTeamScore(this.nodeB.Team, null));
                }
            }
            else
            {
                if (!this.nodeA.IsDecided)
                {
                    foreach (var undecided in this.nodeA.FindUndecided())
                    {
                        yield return undecided;
                    }
                }

                if (!this.nodeB.IsDecided)
                {
                    foreach (var undecided in this.nodeB.FindUndecided())
                    {
                        yield return undecided;
                    }
                }
            }
        }

        /// <inheritdoc />
        public override IEnumerable<EliminationNode> FindNodes(Func<EliminationNode, bool> filter)
        {
            if (this.nodeA != null)
            {
                foreach (var match in this.nodeA.FindNodes(filter))
                {
                    yield return match;
                }
            }

            if (this.nodeB != null)
            {
                foreach (var match in this.nodeB.FindNodes(filter))
                {
                    yield return match;
                }
            }
        }

        /// <inheritdoc />
        public override IEnumerable<EliminationDecider> FindDeciders(Func<EliminationDecider, bool> filter)
        {
            if (filter.Invoke(this))
            {
                yield return this;
            }

            if (this.nodeA != null)
            {
                foreach (var match in this.nodeA.FindDeciders(filter))
                {
                    yield return match;
                }
            }

            if (this.nodeB != null)
            {
                foreach (var match in this.nodeB.FindDeciders(filter))
                {
                    yield return match;
                }
            }
        }
    }
}
