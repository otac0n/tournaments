//-----------------------------------------------------------------------
// <copyright file="EliminationDecider.cs" company="(none)">
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
using Tournaments.Graphics;

namespace Tournaments.Standard
{
    public abstract class EliminationDecider
    {
        public abstract bool IsDecidable { get; }
        public abstract TournamentTeam GetWinner();
        public abstract TournamentTeam GetLoser();
        public abstract NodeMeasurement MeasureWinner(IGraphics g, TournamentNameTable names, float textHeight, Score score);
        public abstract NodeMeasurement MeasureLoser(IGraphics g, TournamentNameTable names, float textHeight, Score score);
        public abstract void RenderWinner(IGraphics g, TournamentNameTable names, float x, float y, float textHeight, Score score);
        public abstract void RenderLoser(IGraphics g, TournamentNameTable names, float x, float y, float textHeight, Score score);

        private const float MinBracketWidth = 120;
        private const float TextYOffset = 3;
        private const float TextXOffset = 3;
        private Brush UserboxBrush = new SolidBrush(Color.FromArgb(220, 220, 220));
        private Font UserboxFont = new Font(FontFamily.GenericSansSerif, 10.0f);
        private Brush UserboxFontBrush = new SolidBrush(Color.Black);
        private const float BracketPreIndent = 10;
        private const float BracketPostIndent = 10;
        private const float BracketVSpacing = 25;
        private Pen BracketPen = new Pen(Color.Black, 1.0f);

        protected NodeMeasurement MeasureTextBox(float textHeight)
        {
            return new NodeMeasurement(
                MinBracketWidth,
                textHeight,
                textHeight / 2);
        }

        protected void RenderTextBox(IGraphics g, NodeMeasurement m, float x, float y, float textHeight, string teamName, Score score)
        {
            g.FillRectangle(
                UserboxBrush,
                new RectangleF(
                    new PointF(
                        x + (m.Width - MinBracketWidth),
                        y + m.CenterLine - (textHeight / 2)),
                    new SizeF(
                        MinBracketWidth,
                        textHeight)));

            if (!string.IsNullOrEmpty(teamName))
            {
                g.DrawString(
                    teamName,
                    UserboxFont,
                    UserboxFontBrush,
                    new PointF(
                        x + (m.Width - MinBracketWidth) + TextXOffset,
                        y + m.CenterLine - (textHeight / 2) + TextYOffset));
            }

            if (score != null)
            {
                var scoreString = score.ToString();
                var scoreWidth = g.MeasureString(scoreString, UserboxFont).Width;

                g.DrawString(
                    scoreString,
                    UserboxFont,
                    UserboxFontBrush,
                    new PointF(
                        x + m.Width - scoreWidth - TextXOffset,
                        y + m.CenterLine - (textHeight / 2) + TextYOffset));
            }
        }

        protected NodeMeasurement MeasureTree(IGraphics g, TournamentNameTable names, float textHeight, EliminationNode nodeA, EliminationNode nodeB)
        {
            var mA = nodeA == null ? null : nodeA.Measure(g, names, textHeight);
            var mB = nodeB == null ? null : nodeB.Measure(g, names, textHeight);

            if (mA == null && mB == null)
            {
                return null;
            }
            else if (mA != null && mB != null)
            {
                return new NodeMeasurement(
                    Math.Max(mA.Width, mB.Width) + BracketPreIndent + BracketPostIndent,
                    mA.Height + mB.Height + BracketVSpacing,
                    (mA.CenterLine + (mA.Height + BracketVSpacing + mB.CenterLine)) / 2);
            }
            else if (mA != null)
            {
                return new NodeMeasurement(
                    mA.Width + BracketPreIndent + BracketPostIndent,
                    mA.Height,
                    mA.CenterLine);
            }
            else
            {
                return new NodeMeasurement(
                    mB.Width + BracketPreIndent + BracketPostIndent,
                    mB.Height,
                    mB.CenterLine);
            }
        }


        protected void RenderTree(IGraphics g, TournamentNameTable names, float x, float y, float textHeight, EliminationNode nodeA, EliminationNode nodeB)
        {
            var m = this.MeasureTree(g, names, textHeight, nodeA, nodeB);
            var mA = nodeA == null ? null : nodeA.Measure(g, names, textHeight);
            var mB = nodeB == null ? null : nodeB.Measure(g, names, textHeight);

            if (mA == null && mB == null)
            {
                return;
            }
            else if (mA != null && mB != null)
            {
                // Preline
                g.DrawLine(
                    BracketPen,
                    new PointF(
                        x + (m.Width),
                        y + m.CenterLine),
                    new PointF(
                        x + (m.Width - BracketPreIndent),
                        y + m.CenterLine));

                // V-Line
                g.DrawLine(
                    BracketPen,
                    new PointF(
                        x + (m.Width - BracketPreIndent),
                        y + mA.CenterLine),
                    new PointF(
                        x + (m.Width - BracketPreIndent),
                        y + mA.Height + BracketVSpacing + mB.CenterLine));

                // Post-Line-A
                g.DrawLine(
                    BracketPen,
                    new PointF(
                        x + (m.Width - BracketPreIndent - BracketPostIndent),
                        y + mA.CenterLine),
                    new PointF(
                        x + (m.Width - BracketPreIndent),
                        y + mA.CenterLine));

                // Post-Line-B
                g.DrawLine(
                    BracketPen,
                    new PointF(
                        x + (m.Width - BracketPreIndent - BracketPostIndent),
                        y + mA.Height + BracketVSpacing + mB.CenterLine),
                    new PointF(
                        x + (m.Width - BracketPreIndent),
                        y + mA.Height + BracketVSpacing + mB.CenterLine));

                nodeA.Render(g, names, x, y, textHeight);
                nodeB.Render(g, names, x, y + mA.Height + BracketVSpacing, textHeight);
            }
            else if (mA != null)
            {
                // TODO: Render Lines?
                nodeA.Render(g, names, x, y, textHeight);
            }
            else
            {
                // TODO: Render Lines?
                nodeB.Render(g, names, x, y, textHeight);
            }
        }
    }
}
