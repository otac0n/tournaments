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
using System.Drawing;

namespace Tournaments.Standard
{
    public class StayDecider : EliminationDecider
    {
        private EliminationNode previousWinnerNode = null;
        private EliminationNode stayNode = null;

        public StayDecider(EliminationNode previousWinnerNode, EliminationNode stayNode)
        {
            if (previousWinnerNode == null)
            {
                throw new ArgumentNullException("previousWinnerNode");
            }

            if (stayNode == null)
            {
                throw new ArgumentNullException("stayNode");
            }

            this.previousWinnerNode = previousWinnerNode;
            this.stayNode = stayNode;
        }

        public override bool IsDecided
        {
            get
            {
                return this.previousWinnerNode.IsDecided && this.stayNode.IsDecided && ((this.previousWinnerNode.Team.TeamId == this.stayNode.Team.TeamId) || (previousWinnerNode.Score != null && stayNode.Score != null && previousWinnerNode.Score != stayNode.Score));
            }
        }

        public override TournamentTeam GetWinner()
        {
            if (!this.previousWinnerNode.IsDecided || !this.stayNode.IsDecided)
            {
                throw new InvalidOperationException("Cannot determine a winner from a node that is undecided.");
            }

            if (this.previousWinnerNode.Team.TeamId == this.stayNode.Team.TeamId)
            {
                return this.previousWinnerNode.Team;
            }

            if (this.previousWinnerNode.Score == null || this.stayNode.Score == null)
            {
                throw new InvalidOperationException("Cannot determine a winner from a node without a score.");
            }

            if (this.previousWinnerNode.Score == this.stayNode.Score)
            {
                throw new InvalidOperationException("Cannot determine a winner from between two nodes with the same score.");
            }

            return this.previousWinnerNode.Score > this.stayNode.Score ? this.previousWinnerNode.Team : this.stayNode.Team;
        }

        public override TournamentTeam GetLoser()
        {
            if (!this.previousWinnerNode.IsDecided || !this.stayNode.IsDecided)
            {
                throw new InvalidOperationException("Cannot determine a loser from a node that is undecided.");
            }

            if (this.previousWinnerNode.Team.TeamId == this.stayNode.Team.TeamId)
            {
                throw new InvalidOperationException("Cannot determine a loser from a competition between a team and itself.");
            }

            if (this.previousWinnerNode.Score == null || this.stayNode.Score == null)
            {
                throw new InvalidOperationException("Cannot determine a loser from a node without a score.");
            }

            if (this.previousWinnerNode.Score == this.stayNode.Score)
            {
                throw new InvalidOperationException("Cannot determine a loser from between two nodes with the same score.");
            }

            return this.previousWinnerNode.Score > this.stayNode.Score ? this.stayNode.Team : this.previousWinnerNode.Team;
        }

        public override NodeMeasurement MeasureWinner(IGraphics g, TournamentNameTable names, float textHeight, Score score)
        {
            if (!(this.previousWinnerNode.IsDecided && this.stayNode.IsDecided) || (this.stayNode.Team == null || this.previousWinnerNode.Team == null) || this.previousWinnerNode.Team.TeamId == this.stayNode.Team.TeamId)
            {
                return this.previousWinnerNode.Measure(g, names, textHeight);
            }
            else
            {
                var m = this.MeasureTree(g, names, textHeight,
                    this.previousWinnerNode,
                    this.stayNode);

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

                return new NodeMeasurement(m.Width + t.Width, m.Height, m.CenterLine);
            }
        }

        public override NodeMeasurement MeasureLoser(IGraphics g, TournamentNameTable names, float textHeight, Score score)
        {
            throw new InvalidOperationException("Rendering the loser node of a stay decider is invalid.");
        }

        public override void RenderWinner(IGraphics g, TournamentNameTable names, float x, float y, float textHeight, Score score)
        {
            if (!(this.previousWinnerNode.IsDecided && this.stayNode.IsDecided) || (this.stayNode.Team == null || this.previousWinnerNode.Team == null) || this.previousWinnerNode.Team.TeamId == this.stayNode.Team.TeamId)
            {
                this.previousWinnerNode.Render(g, names, x, y, textHeight);
            }
            else
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

                var r = this.MeasureTextBox(g, textHeight, teamName, score);

                this.RenderTextBox(g, x + m.Width - r.Width, y + m.CenterLine - r.CenterLine, textHeight, teamName, score);
                this.RenderTree(g, names, x, y, textHeight,
                    this.previousWinnerNode,
                    this.stayNode);
            }
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

            if (this.previousWinnerNode.IsDecided && this.stayNode.IsDecided && !(this.previousWinnerNode.Score != null || this.stayNode.Score != null))
            {
                // If our component nodes have played out, but we haven't
                var teamA = pairing.TeamScores[0].Team;
                var scoreA = pairing.TeamScores[0].Score;
                var teamB = pairing.TeamScores[1].Team;
                var scoreB = pairing.TeamScores[1].Score;

                if (this.previousWinnerNode.Team.TeamId == teamB.TeamId && this.stayNode.Team.TeamId == teamA.TeamId)
                {
                    // If the order of the pairing is reversed, we will normalize the pairing to us.
                    var teamSwap = teamA;
                    teamB = teamA;
                    teamA = teamSwap;

                    var scoreSwap = scoreA;
                    scoreB = scoreA;
                    scoreA = scoreSwap;
                }

                if (this.previousWinnerNode.Team.TeamId == teamA.TeamId && this.stayNode.Team.TeamId == teamB.TeamId)
                {
                    // If we are a match, assign the scores.
                    this.previousWinnerNode.Score = scoreA;
                    this.stayNode.Score = scoreB;
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
                return (!this.previousWinnerNode.IsDecided && this.previousWinnerNode.ApplyPairing(pairing)) || (!this.stayNode.IsDecided && this.stayNode.ApplyPairing(pairing));
            }
        }

        public override IEnumerable<TournamentPairing> FindUndecided()
        {
            if (this.IsDecided)
            {
                yield break;
            }
            else if (this.previousWinnerNode.IsDecided && this.stayNode.IsDecided)
            {
                if (this.Locked)
                {
                    yield break;
                }
                else
                {
                    yield return new TournamentPairing(
                            new TournamentTeamScore(this.previousWinnerNode.Team, null),
                            new TournamentTeamScore(this.stayNode.Team, null));
                }
            }
            else
            {
                if (!this.previousWinnerNode.IsDecided)
                {
                    foreach (var undecided in this.previousWinnerNode.FindUndecided())
                    {
                        yield return undecided;
                    }
                }

                if (!this.stayNode.IsDecided)
                {
                    foreach (var undecided in this.stayNode.FindUndecided())
                    {
                        yield return undecided;
                    }
                }
            }
        }

        public override IEnumerable<EliminationNode> FindNodes(Func<EliminationNode, bool> filter)
        {
            if (this.previousWinnerNode != null)
            {
                foreach(var match in this.previousWinnerNode.FindNodes(filter))
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

            if (this.previousWinnerNode != null)
            {
                foreach (var match in this.previousWinnerNode.FindDeciders(filter))
                {
                    yield return match;
                }
            }
        }
    }
}
