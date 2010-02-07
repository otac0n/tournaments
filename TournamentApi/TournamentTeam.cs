//-----------------------------------------------------------------------
// <copyright file="TournamentTeam.cs" company="(none)">
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
    using System.Diagnostics;

    /// <summary>
    /// Describes a team that may participate in a tournament.
    /// </summary>
    [DebuggerDisplay("[Team {this.TeamId} @ {this.Rating}]")]
    public sealed class TournamentTeam
    {
        /// <summary>
        /// Holds the unique, application-specific identifier of the team.
        /// </summary>
        private readonly long teamId;

        /// <summary>
        /// Initializes a new instance of the TournamentTeam class.
        /// </summary>
        /// <param name="teamId">The unique, application-specific identifier of the team.</param>
        /// <param name="rating">The team's current rating pertaining to a specific tournament.</param>
        public TournamentTeam(long teamId, int? rating)
        {
            this.teamId = teamId;
            this.Rating = rating;
        }

        /// <summary>
        /// Gets the unique, application-specific identifier of the team.
        /// </summary>
        public long TeamId
        {
            get
            {
                return this.teamId;
            }
        }

        /// <summary>
        /// Gets or sets the team's current rating or seed pertaining to a specific tournament or league.
        /// </summary>
        public int? Rating
        {
            get;
            set;
        }
    }
}
