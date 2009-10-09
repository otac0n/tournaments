//-----------------------------------------------------------------------
// <copyright file="LoserNode.cs" company="(none)">
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tournaments.Standard
{
    public class LoserNode : EliminationNode
    {
        public LoserNode(EliminationDecider decider)
            : base(decider)
        {
        }

        public override TournamentTeam Team
        {
            get
            {
                return this.decider.GetLoser();
            }
        }

        public override NodeMeasurement Measure(Tournaments.Graphics.IGraphics g, TournamentNameTable names, float textHeight)
        {
            return this.decider.MeasureLoser(g, names, textHeight, this.Score);
        }

        public override void Render(Tournaments.Graphics.IGraphics g, TournamentNameTable names, RectangleF region, float textHeight)
        {
            this.decider.RenderLoser(g, names, region, textHeight, this.Score);
        }

        public override bool ApplyPairing(TournamentPairing pairing)
        {
            if (pairing == null)
            {
                throw new ArgumentNullException("pairing");
            }

            return false;
        }

        public override IEnumerable<TournamentPairing> FindUndecided()
        {
            yield break;
        }

        public override IEnumerable<EliminationNode> FindNodes(Func<EliminationNode, bool> filter)
        {
            if (filter.Invoke(this))
            {
                yield return this;
            }
        }

        public override IEnumerable<EliminationDecider> FindDeciders(Func<EliminationDecider, bool> filter)
        {
            yield break;
        }
    }
}
