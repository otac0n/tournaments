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
using System.Linq;
using System.Text;
using System.Drawing;
using Tournaments.Graphics;

namespace Tournaments.Standard
{
    public class ContinuationDecider : EliminationDecider
    {
        private EliminationNode nodeA = null;
        private EliminationNode nodeB = null;

        public ContinuationDecider(EliminationNode nodeA, EliminationNode nodeB)
        {
            if (nodeA == null)
            {
                throw new ArgumentNullException("nodeA");
            }

            if (nodeB == null)
            {
                throw new ArgumentNullException("nodeB");
            }

            this.nodeA = nodeA;
            this.nodeB = nodeB;
        }

        public EliminationNode ChildA
        {
            get
            {
                return this.nodeA;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                this.nodeA = value;
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
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                this.nodeB = value;
            }
        }

        public override bool IsDecided
        {
            get
            {
                return this.nodeA.IsDecided && nodeB.IsDecided && ((nodeA.Team == null || nodeB.Team == null) || (nodeA.Score != null && nodeB.Score != null && nodeA.Score != nodeB.Score));
            }
        }

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

        public override bool ApplyPairing(TournamentPairing pairing)
        {
            if (pairing == null)
            {
                throw new ArgumentNullException("pairing");
            }

            if (this.Locked)
            {
                // If we (and, all of our decentants) are decided, return false, indicating that no node below us is in a state that needs a score.
                return false;
            }

            if (pairing.TeamScores.Count != 2 || pairing.TeamScores[0] == null || pairing.TeamScores[1] == null || pairing.TeamScores[0].Team == null || pairing.TeamScores[1].Team == null)
            {
                // If the pairing did not contain exactly two teams, or if either of the teams passed was null.
                throw new ArgumentException("A bye was passed as a pairing.", "pairing");
            }

            if (this.nodeA.IsDecided && this.nodeB.IsDecided && !(this.nodeA.Score != null || this.nodeB.Score != null))
            {
                // If our component nodes have played out, but we haven't
                var teamA = pairing.TeamScores[0].Team;
                var scoreA = pairing.TeamScores[0].Score;
                var teamB = pairing.TeamScores[1].Team;
                var scoreB = pairing.TeamScores[1].Score;

                if (this.nodeA.Team.TeamId == teamB.TeamId && this.nodeB.Team.TeamId == teamA.TeamId)
                {
                    // If the order of the pairing is reversed, we will normalize ourself to the pairing.
                    var swap = this.nodeA;
                    this.nodeA = this.nodeB;
                    this.nodeB = swap;
                }

                if (this.nodeA.Team.TeamId == teamA.TeamId && this.nodeB.Team.TeamId == teamB.TeamId)
                {
                    // If we are a match, assign the scores.
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
            else
            {
                return (!this.nodeA.IsDecided && this.nodeA.ApplyPairing(pairing)) || (!this.nodeB.IsDecided && this.nodeB.ApplyPairing(pairing));
            }
        }

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
