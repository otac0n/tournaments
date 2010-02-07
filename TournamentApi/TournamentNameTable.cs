//-----------------------------------------------------------------------
// <copyright file="TournamentNameTable.cs" company="(none)">
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
    /// Encapsulates a list of the names of tournament teams.
    /// </summary>
    public sealed class TournamentNameTable
    {
        /// <summary>
        /// Holds the mappings of team ids to team names.
        /// </summary>
        private readonly Dictionary<long, string> names;

        /// <summary>
        /// Initializes a new instance of the TournamentNameTable class, initialized with the supplied names.
        /// </summary>
        /// <param name="names">A pre-populated mapping of team ids to team names.</param>
        public TournamentNameTable(IDictionary<long, string> names)
        {
            this.names = new Dictionary<long, string>();

            foreach (var key in names.Keys)
            {
                this.names.Add(key, names[key]);
            }
        }

        /// <summary>
        /// Retrieves a team name associated with the supplied team id.
        /// </summary>
        /// <param name="teamId">The id of the team for which to retrieve the name.</param>
        /// <returns>The team name associated with the supplied team id.</returns>
        public string this[long teamId]
        {
            get
            {
                return this.names[teamId];
            }
        }
    }
}
