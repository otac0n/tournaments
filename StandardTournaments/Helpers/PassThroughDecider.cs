//-----------------------------------------------------------------------
// <copyright file="PassThroughDecider.cs" company="(none)">
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

    public class PassThroughDecider : EliminationDecider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PassThroughDecider"/> class.
        /// </summary>
        /// <param name="passThroughNode">The node whose winner will be passed through.</param>
        public PassThroughDecider(EliminationNode passThroughNode)
        {
            this.PassThroughNode = passThroughNode ?? throw new ArgumentNullException(nameof(passThroughNode));
        }

        /// <summary>
        /// Gets the node whose winner will be passed through.
        /// </summary>
        public EliminationNode PassThroughNode { get; }

        /// <inheritdoc />
        public override bool IsDecided
        {
            get
            {
                return this.PassThroughNode.IsDecided;
            }
        }

        /// <inheritdoc />
        public override TournamentTeam GetWinner()
        {
            if (!this.PassThroughNode.IsDecided)
            {
                throw new InvalidOperationException("Cannot determine a winner from a node that is undecided.");
            }

            return this.PassThroughNode.Team;
        }

        /// <inheritdoc />
        public override TournamentTeam GetLoser()
        {
            throw new InvalidOperationException("Cannot determine a loser from a pass through node.");
        }

        /// <inheritdoc />
        public override NodeMeasurement MeasureWinner(IGraphics g, TournamentNameTable names, float textHeight, Score score)
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

            return this.MeasureTextBox(g, textHeight, teamName, score);
        }

        /// <inheritdoc />
        public override NodeMeasurement MeasureLoser(IGraphics g, TournamentNameTable names, float textHeight, Score score)
        {
            throw new InvalidOperationException("Cannot determine a loser from a pass through node.");
        }

        /// <inheritdoc />
        public override void RenderWinner(IGraphics g, TournamentNameTable names, float x, float y, float textHeight, Score score)
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

            this.RenderTextBox(g, x, y, textHeight, teamName, score);
        }

        /// <inheritdoc />
        public override void RenderLoser(IGraphics g, TournamentNameTable names, float x, float y, float textHeight, Score score)
        {
            throw new InvalidOperationException("Cannot determine a loser from a pass through node.");
        }

        /// <inheritdoc />
        public override bool ApplyPairing(TournamentPairing pairing)
        {
            if (pairing == null)
            {
                throw new ArgumentNullException(nameof(pairing));
            }

            return false;
        }

        /// <inheritdoc />
        public override IEnumerable<TournamentPairing> FindUndecided()
        {
            yield break;
        }

        /// <inheritdoc />
        public override IEnumerable<EliminationNode> FindNodes(Func<EliminationNode, bool> filter)
        {
            if (this.PassThroughNode != null)
            {
                foreach (var match in this.PassThroughNode.FindNodes(filter))
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

            if (this.PassThroughNode != null)
            {
                foreach (var match in this.PassThroughNode.FindDeciders(filter))
                {
                    yield return match;
                }
            }
        }
    }
}
