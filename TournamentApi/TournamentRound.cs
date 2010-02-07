//-----------------------------------------------------------------------
// <copyright file="TournamentRound.cs" company="(none)">
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
    using System.Collections.Generic;

    /// <summary>
    /// Describes a round within a tournament which consists of one or more pairings.
    /// </summary>
    public sealed class TournamentRound
    {
        /// <summary>
        /// Holds the list of pairings in the round.
        /// </summary>
        private readonly List<TournamentPairing> pairings;

        /// <summary>
        /// Initializes a new instance of the TournamentRound class.
        /// </summary>
        /// <param name="pairings">The list of pairings in the round.</param>
        public TournamentRound(IEnumerable<TournamentPairing> pairings)
        {
            if (pairings == null)
            {
                throw new ArgumentNullException("pairings");
            }

            this.pairings = new List<TournamentPairing>(pairings);
        }

        /// <summary>
        /// Initializes a new instance of the TournamentRound class.
        /// </summary>
        /// <param name="pairings">The parameter array of pairings in the round.</param>
        public TournamentRound(params TournamentPairing[] pairings)
        {
            if (pairings == null)
            {
                throw new ArgumentNullException("pairings");
            }

            this.pairings = new List<TournamentPairing>(pairings);
        }

        /// <summary>
        /// Gets a shallow read-only copy of the list of pairings in the round.
        /// </summary>
        public IList<TournamentPairing> Pairings
        {
            get
            {
                return this.pairings.AsReadOnly();
            }
        }
    }
}
