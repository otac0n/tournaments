//-----------------------------------------------------------------------
// <copyright file="TournamentPairing.cs" company="(none)">
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
    using System.Collections.Generic;

    /// <summary>
    /// Describes a pairing between one or more teams in a tournament.
    /// </summary>
    public sealed class TournamentPairing
    {
        /// <summary>
        /// Holds the list of team scores in the pairing.
        /// </summary>
        private readonly List<TournamentTeamScore> teamScores;

        /// <summary>
        /// Initializes a new instance of the TournamentPairing class.
        /// </summary>
        /// <param name="teamScores">The list of teams in this pairing.</param>
        public TournamentPairing(IEnumerable<TournamentTeamScore> teamScores)
        {
            this.teamScores = new List<TournamentTeamScore>(teamScores);
        }

        /// <summary>
        /// Initializes a new instance of the TournamentPairing class.
        /// </summary>
        /// <param name="teamScores">The parameter aray of teams in this pairing.</param>
        public TournamentPairing(params TournamentTeamScore[] teamScores)
        {
            this.teamScores = new List<TournamentTeamScore>(teamScores);
        }

        /// <summary>
        /// Gets a shallow read-only copy of the list of team scores in the pairing.
        /// </summary>
        public IList<TournamentTeamScore> TeamScores
        {
            get
            {
                return this.teamScores.AsReadOnly();
            }
        }
    }
}
