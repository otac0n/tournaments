//-----------------------------------------------------------------------
// <copyright file="TournamentTeamScore.cs" company="(none)">
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

namespace Tournaments
{
    using System;

    /// <summary>
    /// Describes the score that a team has obtained in a tournament.
    /// </summary>
    public sealed class TournamentTeamScore
    {
        /// <summary>
        /// Holds the team being scored.
        /// </summary>
        private readonly TournamentTeam team;

        /// <summary>
        /// Initializes a new instance of the TournamentTeamScore class.
        /// </summary>
        /// <param name="team">The team being scored.</param>
        /// <param name="score">The score that the team obtained.</param>
        public TournamentTeamScore(TournamentTeam team, Score score)
        {
            if (team == null)
            {
                throw new ArgumentNullException("team");
            }

            this.team = team;
            this.Score = score;
        }

        /// <summary>
        /// Gets the team being scored.
        /// </summary>
        public TournamentTeam Team
        {
            get
            {
                return this.team;
            }
        }

        /// <summary>
        /// Gets or sets the score that the team obtained.
        /// </summary>
        public Score Score
        {
            get;
            set;
        }
    }
}
