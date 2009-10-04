//-----------------------------------------------------------------------
// <copyright file="StayDecider.cs" company="(none)">
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
using Tournaments.Graphics;

namespace Tournaments.Standard
{
    public class StayDecider : EliminationDecider
    {
        private EliminationNode nodeA = null;
        private EliminationNode nodeB = null;

        public StayDecider(EliminationNode nodeA, EliminationNode nodeB)
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

        public override bool IsDecided
        {
            get { return this.nodeA.IsDecided && nodeB.IsDecided && ((this.nodeA.Team.TeamId == this.nodeB.Team.TeamId) || (nodeA.Score != null && nodeB.Score != null && nodeA.Score != nodeB.Score)); }
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

            if (this.nodeA.Team.TeamId == this.nodeB.Team.TeamId)
            {
                return this.nodeA.Team;
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

            if (this.nodeA.Team.TeamId == this.nodeB.Team.TeamId)
            {
                throw new InvalidOperationException("Cannot determine a loser from a competition between a team and itself.");
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

        public override NodeMeasurement MeasureWinner(IGraphics g, TournamentNameTable names, float textHeight, Score score)
        {
            var m = this.MeasureTree(g, names, textHeight,
                this.nodeA,
                this.nodeB);

            var t = this.MeasureTextBox(textHeight);

            return new NodeMeasurement(m.Width + t.Width, m.Height, m.CenterLine);
        }

        public override NodeMeasurement MeasureLoser(IGraphics g, TournamentNameTable names, float textHeight, Score score)
        {
            throw new InvalidOperationException("Rendering the loser node of a stay decider is invalid.");
        }

        public override void RenderWinner(IGraphics g, TournamentNameTable names, float x, float y, float textHeight, Score score)
        {
            var m = this.MeasureWinner(g, names, textHeight, score);

            string teamName = "";
            if (this.IsDecided)
            {
                teamName = names[this.GetWinner().TeamId];
            }

            this.RenderTextBox(g, m, x, y, textHeight, teamName, score);
            this.RenderTree(g, names, x, y, textHeight,
                this.nodeA,
                this.nodeB);
        }

        public override void RenderLoser(IGraphics g, TournamentNameTable names, float x, float y, float textHeight, Score score)
        {
            throw new InvalidOperationException("Rendering the loser node of a stay decider is invalid.");
        }

        public override bool ApplyPairing(TournamentPairing pairing)
        {
            if (pairing == null)
            {
                throw new ArgumentNullException("pairing");
            }

            throw new NotImplementedException();
        }

        public override IEnumerable<TournamentPairing> FindUndecided()
        {
            if (this.IsDecided)
            {
                yield break;
            }
            else if (this.nodeA.IsDecided && this.nodeB.IsDecided && (this.nodeA.Score != null || this.nodeB.Score != null))
            {
                // TODO: This could be an issue if the pairing has already been built, but came in with null scores.
                yield break;
            }
            else if (this.nodeA.IsDecided && this.nodeB.IsDecided)
            {
                yield return new TournamentPairing(
                        new TournamentTeamScore(this.nodeA.Team, null),
                        new TournamentTeamScore(this.nodeB.Team, null));
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
                    foreach (var undecided in this.nodeA.FindUndecided())
                    {
                        yield return undecided;
                    }
                }
            }
        }
    }
}
