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

namespace Tournaments.Standard
{
    using System;
    using System.Collections.Generic;
    using Tournaments.Graphics;

    public class StayDecider : EliminationDecider
    {
        public StayDecider(EliminationNode previousWinnerNode, EliminationNode stayNode)
        {
            this.PreviousWinnerNode = previousWinnerNode ?? throw new ArgumentNullException(nameof(previousWinnerNode));
            this.StayNode = stayNode ?? throw new ArgumentNullException(nameof(stayNode));
        }

        /// <summary>
        /// Gets the winner of the previous pairing.
        /// </summary>
        public EliminationNode PreviousWinnerNode { get; }

        /// <summary>
        /// Gets the node whose winner will stay if necessary.
        /// </summary>
        public EliminationNode StayNode { get; }

        /// <inheritdoc />
        public override bool IsDecided
        {
            get
            {
                return this.PreviousWinnerNode.IsDecided && this.StayNode.IsDecided && ((this.PreviousWinnerNode.Team.TeamId == this.StayNode.Team.TeamId) || (PreviousWinnerNode.Score != null && StayNode.Score != null && PreviousWinnerNode.Score != StayNode.Score));
            }
        }

        /// <inheritdoc />
        public override TournamentTeam GetWinner()
        {
            if (!this.PreviousWinnerNode.IsDecided || !this.StayNode.IsDecided)
            {
                throw new InvalidOperationException("Cannot determine a winner from a node that is undecided.");
            }

            if (this.PreviousWinnerNode.Team.TeamId == this.StayNode.Team.TeamId)
            {
                return this.PreviousWinnerNode.Team;
            }

            if (this.PreviousWinnerNode.Score == null || this.StayNode.Score == null)
            {
                throw new InvalidOperationException("Cannot determine a winner from a node without a score.");
            }

            if (this.PreviousWinnerNode.Score == this.StayNode.Score)
            {
                throw new InvalidOperationException("Cannot determine a winner from between two nodes with the same score.");
            }

            return this.PreviousWinnerNode.Score > this.StayNode.Score ? this.PreviousWinnerNode.Team : this.StayNode.Team;
        }

        /// <inheritdoc />
        public override TournamentTeam GetLoser()
        {
            if (!this.PreviousWinnerNode.IsDecided || !this.StayNode.IsDecided)
            {
                throw new InvalidOperationException("Cannot determine a loser from a node that is undecided.");
            }

            if (this.PreviousWinnerNode.Team.TeamId == this.StayNode.Team.TeamId)
            {
                throw new InvalidOperationException("Cannot determine a loser from a competition between a team and itself.");
            }

            if (this.PreviousWinnerNode.Score == null || this.StayNode.Score == null)
            {
                throw new InvalidOperationException("Cannot determine a loser from a node without a score.");
            }

            if (this.PreviousWinnerNode.Score == this.StayNode.Score)
            {
                throw new InvalidOperationException("Cannot determine a loser from between two nodes with the same score.");
            }

            return this.PreviousWinnerNode.Score > this.StayNode.Score ? this.StayNode.Team : this.PreviousWinnerNode.Team;
        }

        /// <inheritdoc />
        public override NodeMeasurement MeasureWinner(IGraphics g, TournamentNameTable names, float textHeight, Score score)
        {
            if (!(this.PreviousWinnerNode.IsDecided && this.StayNode.IsDecided) || (this.StayNode.Team == null || this.PreviousWinnerNode.Team == null) || this.PreviousWinnerNode.Team.TeamId == this.StayNode.Team.TeamId)
            {
                return this.PreviousWinnerNode.Measure(g, names, textHeight);
            }
            else
            {
                var m = this.MeasureTree(g, names, textHeight,
                    this.PreviousWinnerNode,
                    this.StayNode);

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

        /// <inheritdoc />
        public override NodeMeasurement MeasureLoser(IGraphics g, TournamentNameTable names, float textHeight, Score score)
        {
            throw new InvalidOperationException("Rendering the loser node of a stay decider is invalid.");
        }

        /// <inheritdoc />
        public override void RenderWinner(IGraphics g, TournamentNameTable names, float x, float y, float textHeight, Score score)
        {
            if (!(this.PreviousWinnerNode.IsDecided && this.StayNode.IsDecided) || (this.StayNode.Team == null || this.PreviousWinnerNode.Team == null) || this.PreviousWinnerNode.Team.TeamId == this.StayNode.Team.TeamId)
            {
                this.PreviousWinnerNode.Render(g, names, x, y, textHeight);
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
                    this.PreviousWinnerNode,
                    this.StayNode);
            }
        }

        /// <inheritdoc />
        public override void RenderLoser(IGraphics g, TournamentNameTable names, float x, float y, float textHeight, Score score)
        {
            throw new InvalidOperationException("Rendering the loser node of a stay decider is invalid.");
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

            if (pairing.TeamScores.Count != 2 || pairing.TeamScores[0] == null || pairing.TeamScores[1] == null || pairing.TeamScores[0].Team == null || pairing.TeamScores[1].Team == null)
            {
                // If the pairing did not contain exactly two teams, or if either of the teams passed was null.
                throw new ArgumentException("A bye was passed as a pairing.", nameof(pairing));
            }

            if (this.PreviousWinnerNode.IsDecided && this.StayNode.IsDecided && !(this.PreviousWinnerNode.Score != null || this.StayNode.Score != null))
            {
                // If our component nodes have played out, but we haven't
                var teamA = pairing.TeamScores[0].Team;
                var scoreA = pairing.TeamScores[0].Score;
                var teamB = pairing.TeamScores[1].Team;
                var scoreB = pairing.TeamScores[1].Score;

                if (this.PreviousWinnerNode.Team.TeamId == teamB.TeamId && this.StayNode.Team.TeamId == teamA.TeamId)
                {
                    // If the order of the pairing is reversed, we will normalize the pairing to us.
                    var teamSwap = teamA;
                    teamB = teamA;
                    teamA = teamSwap;

                    var scoreSwap = scoreA;
                    scoreB = scoreA;
                    scoreA = scoreSwap;
                }

                if (this.PreviousWinnerNode.Team.TeamId == teamA.TeamId && this.StayNode.Team.TeamId == teamB.TeamId)
                {
                    // If we are a match, assign the scores.
                    this.PreviousWinnerNode.Score = scoreA;
                    this.StayNode.Score = scoreB;
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
                return (!this.PreviousWinnerNode.IsDecided && this.PreviousWinnerNode.ApplyPairing(pairing)) || (!this.StayNode.IsDecided && this.StayNode.ApplyPairing(pairing));
            }
        }

        /// <inheritdoc />
        public override IEnumerable<TournamentPairing> FindUndecided()
        {
            if (this.IsDecided)
            {
                yield break;
            }
            else if (this.PreviousWinnerNode.IsDecided && this.StayNode.IsDecided)
            {
                if (this.Locked)
                {
                    yield break;
                }
                else
                {
                    yield return new TournamentPairing(
                        new TournamentTeamScore(this.PreviousWinnerNode.Team, null),
                        new TournamentTeamScore(this.StayNode.Team, null));
                }
            }
            else
            {
                if (!this.PreviousWinnerNode.IsDecided)
                {
                    foreach (var undecided in this.PreviousWinnerNode.FindUndecided())
                    {
                        yield return undecided;
                    }
                }

                if (!this.StayNode.IsDecided)
                {
                    foreach (var undecided in this.StayNode.FindUndecided())
                    {
                        yield return undecided;
                    }
                }
            }
        }

        /// <inheritdoc />
        public override IEnumerable<EliminationNode> FindNodes(Func<EliminationNode, bool> filter)
        {
            if (this.PreviousWinnerNode != null)
            {
                foreach(var match in this.PreviousWinnerNode.FindNodes(filter))
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

            if (this.PreviousWinnerNode != null)
            {
                foreach (var match in this.PreviousWinnerNode.FindDeciders(filter))
                {
                    yield return match;
                }
            }
        }
    }
}
