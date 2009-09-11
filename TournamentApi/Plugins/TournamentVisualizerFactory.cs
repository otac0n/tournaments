//-----------------------------------------------------------------------
// <copyright file="TournamentVisualizerFactory.cs" company="(none)">
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

namespace Tournaments.Plugins
{
    using System;

    /// <summary>
    /// Implements a ITournamentVisualizerFactory for a tournament visualizer with a simple or default constructor.
    /// </summary>
    /// <typeparam name="T">An ITournamentVisualizerFactory with a simple or default constructor.</typeparam>
    public sealed class TournamentVisualizerFactory<T> : ITournamentVisualizerFactory where T : ITournamentVisualizer, new()
    {
        /// <summary>
        /// Holds the name of the ITournamentVisualizerFactory managed my this class.
        /// </summary>
        private string name;

        /// <summary>
        /// Initializes a new instance of the TournamentVisualizerFactory class.
        /// </summary>
        public TournamentVisualizerFactory()
        {
            T temp = new T();
            this.name = temp.Name;
        }

        /// <summary>
        /// Gets the name of this pairings tournament visualizer.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Creates a new ITournamentVisualizer instance.
        /// </summary>
        /// <returns>The new ITournamentVisualizer instance.</returns>
        public ITournamentVisualizer Create()
        {
            ITournamentVisualizer t = new T();

            if (t == null)
            {
                throw new InvalidOperationException("Unable to create a tournament generator of type " + typeof(T).ToString() + ".");
            }
            else
            {
                return t;
            }
        }
    }
}
