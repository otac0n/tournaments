//-----------------------------------------------------------------------
// <copyright file="SingleEliminationTournament.cs" company="(none)">
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

namespace Tournaments.Standard
{
    using System;
    using System.Collections.Generic;
    using Tournaments.Graphics;

    public abstract class EliminationNode
    {
        private EliminationDecider primaryParent;
        protected EliminationDecider decider;

        public EliminationNode(EliminationDecider decider)
        {
            this.decider = decider ?? throw new ArgumentNullException(nameof(decider));
        }

        /// <summary>
        /// Gets a value indicating whether or not this node is decided.
        /// </summary>
        public bool IsDecided => this.decider.IsDecided;

        public EliminationDecider PrimaryParent
        {
            get
            {
                return this.primaryParent;
            }

            set
            {
                this.primaryParent = value;
            }
        }

        public int Level =>
            this.primaryParent == null ? 0 :
            this.primaryParent.Level + 1;

        public EliminationNode CommonAncestor =>
            this.primaryParent == null ?  this :
            this.primaryParent.CommonAncestor;

        public bool Locked
        {
            get
            {
                return this.decider.Locked || (this.primaryParent != null && this.primaryParent.Locked);
            }
        }

        public abstract TournamentTeam Team { get; }

        public EliminationDecider Decider
        {
            get
            {
                return this.decider;
            }

            set
            {
                this.decider = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public Score Score { get; set; }

        public abstract NodeMeasurement Measure(IGraphics g, TournamentNameTable names, float textHeight);

        public abstract void Render(IGraphics g, TournamentNameTable names, float x, float y, float textHeight);

        public abstract bool ApplyPairing(TournamentPairing pairing);

        public abstract IEnumerable<TournamentPairing> FindUndecided();

        public abstract IEnumerable<EliminationNode> FindNodes(Func<EliminationNode, bool> filter);

        public abstract IEnumerable<EliminationDecider> FindDeciders(Func<EliminationDecider, bool> filter);
    }
}
