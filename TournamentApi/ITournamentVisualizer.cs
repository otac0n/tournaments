//-----------------------------------------------------------------------
// <copyright file="ITournamentVisualizer.cs" company="(none)">
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
    using System.Drawing;
    using System.Xml;

    /// <summary>
    /// Specifies the interface required for a tournament visualizer.
    /// </summary>
    public interface ITournamentVisualizer
    {
        /// <summary>
        /// Gets the name of the tournament visualizer.
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Loads the specified teams and rounds as a state for the tournament visualizer.
        /// </summary>
        /// <param name="teams">The teams in play.</param>
        /// <param name="rounds">The rounds that have either been played or are scheduled to be played.</param>
        /// <exception cref="ArgumentNullException">When either the teams or the rounds parameter is null.</exception>
        /// <exception cref="InvalidTournamentStateException">When the state teams and rounds passed are in a state considered to be invalid to the tournament visualizer</exception>
        /// <remarks>The visualizer may be as lax or as strict in the enforcement of tournament state as the implementer desires.  However, it is recommended that implementations be lenient in what they accept.</remarks>
        void LoadState(IEnumerable<TournamentTeam> teams, IList<TournamentRound> rounds);

        /// <summary>
        /// Renders the tournament to an SVG image.
        /// </summary>
        /// <param name="teamNames">The names of the teams, for use in drawing team names in the visualization.</param>
        /// <returns>An SVG document containing the rendered image.</returns>
        XmlReader Render(TournamentNameTable teamNames);

        /// <summary>
        /// Measures the tournament visualization.
        /// </summary>
        /// <param name="teamNames">The names of the teams, for use in drawing team names in the visualization.</param>
        /// <returns>The size, in pixels, of the resulting SVG document prefered bounds.</returns>
        Size Measure(TournamentNameTable teamNames);
    }
}
