//-----------------------------------------------------------------------
// <copyright file="IPairingsGenerator.cs" company="(none)">
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
    /// Describes a pairings generator that will run a tournament.
    /// </summary>
    public interface IPairingsGenerator
    {
        /// <summary>
        /// Gets the name of the pairings generator.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the state of the pairings generator.
        /// </summary>
        PairingsGeneratorState State { get; }

        /// <summary>
        /// Gets a value indicating whether or not the pairings generator supports adding teams once a round has been generated.
        /// </summary>
        bool SupportsLateEntry { get; }

        /// <summary>
        /// Resets the pairings generator's initial state.
        /// </summary>
        void Reset();

        /// <summary>
        /// Loads the specified teams and rounds as a state for the pairings generator.
        /// </summary>
        /// <param name="teams">The teams in play.</param>
        /// <param name="rounds">The rounds that have either been played or are scheduled to be played.</param>
        /// <exception cref="ArgumentNullException">When either the teams or the rounds parameter is null.</exception>
        /// <exception cref="InvalidTournamentStateException">When the state teams and rounds passed are in a state considered to be invalid to the pairings generator.</exception>
        /// <remarks>The generator may be as lax or as strict in the enforcement of tournament state as the implementer desires.  However, it is recommended that implementations be lenient in what they accept.</remarks>
        void LoadState(IEnumerable<TournamentTeam> teams, IList<TournamentRound> rounds);

        /// <summary>
        /// Creates the next round for the current internal state.
        /// </summary>
        /// <param name="places">The number of places (such as game servers or basketball courts) available for users to compete on.  Must be null or greater than zero.</param>
        /// <returns>The next round of pairings for the tournament.</returns>
        TournamentRound CreateNextRound(int? places);

        /// <summary>
        /// Creates the list of rankings for the current state of the tournament.
        /// </summary>
        /// <returns>The list of rankings.</returns>
        IEnumerable<TournamentRanking> GenerateRankings();
    }
}
