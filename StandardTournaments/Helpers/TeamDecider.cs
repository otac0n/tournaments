//-----------------------------------------------------------------------
// <copyright file="TeamDecider.cs" company="(none)">
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
    public class TeamDecider : EliminationDecider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeamDecider"/> class.
        /// </summary>
        /// <param name="team">The team that will be chosen.</param>
        public TeamDecider(TournamentTeam team)
        {
            this.Team = team ?? throw new ArgumentNullException(nameof(team));

            this.Lock();
        }

        /// <summary>
        /// Gets the team that will be chosen.
        /// </summary>
        public TournamentTeam Team { get; }

        /// <inheritdoc />
        public override bool IsDecided
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        public override TournamentTeam GetWinner()
        {
            return this.Team;
        }

        /// <inheritdoc />
        public override TournamentTeam GetLoser()
        {
            throw new InvalidOperationException("Cannot determine a loser from an individual team entry.");
        }

        /// <inheritdoc />
        public override NodeMeasurement MeasureWinner(IGraphics g, TournamentNameTable names, float textHeight, Score score)
        {
            return this.MeasureTextBox(g, textHeight, names[this.Team.TeamId], score);
        }

        /// <inheritdoc />
        public override NodeMeasurement MeasureLoser(IGraphics g, TournamentNameTable names, float textHeight, Score score)
        {
            throw new InvalidOperationException("Cannot determine a loser from an individual team entry.");
        }

        /// <inheritdoc />
        public override void RenderWinner(IGraphics g, TournamentNameTable names, float x, float y, float textHeight, Score score)
        {
            this.RenderTextBox(g, x, y, textHeight, names[this.Team.TeamId], score);
        }

        /// <inheritdoc />
        public override void RenderLoser(IGraphics g, TournamentNameTable names, float x, float y, float textHeight, Score score)
        {
            throw new InvalidOperationException("Cannot determine a loser from an individual team entry.");
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
            yield break;
        }

        /// <inheritdoc />
        public override IEnumerable<EliminationDecider> FindDeciders(Func<EliminationDecider, bool> filter)
        {
            if (filter.Invoke(this))
            {
                yield return this;
            }
        }
    }
}
