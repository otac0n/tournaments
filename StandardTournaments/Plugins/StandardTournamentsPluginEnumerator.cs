//-----------------------------------------------------------------------
// <copyright file="StandardTournamentsPluginEnumerator.cs" company="(none)">
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

namespace Tournaments.Standard
{
    using System;
    using System.Collections.Generic;
    using Tournaments.Plugins;

    /// <summary>
    /// Enumerates the plugins available in this assembly.
    /// </summary>
    public sealed class StandardTournamentsPluginEnumerator : IPluginEnumerator
    {
        /// <summary>
        /// Enumerates the plugin factories available in this assembly.
        /// </summary>
        /// <returns>An enumerable list of the plugin factores avaliable in this assembly.</returns>
        public IEnumerable<IPluginFactory> EnumerateFactories()
        {
            yield return new PairingsGeneratorFactory<RoundRobinPairingsGenerator>();

            yield return new PairingsGeneratorFactory<BoilOffPairingsGenerator>();

            yield return new PairingsGeneratorFactory<SingleEliminationTournament>();
            yield return new TournamentVisualizerFactory<SingleEliminationTournament>();

            yield return new PairingsGeneratorFactory<DoubleEliminationTournament>();
            yield return new TournamentVisualizerFactory<DoubleEliminationTournament>();

            yield break;
        }
    }
}
