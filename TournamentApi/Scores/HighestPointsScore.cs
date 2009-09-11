//-----------------------------------------------------------------------
// <copyright file="HighestPointsScore.cs" company="LAN Lordz, Inc.">
//  Copyright (c) 2009 LAN Lordz, Inc.
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
    using System.Globalization;

    /// <summary>
    /// Descibes a scoring scenarion where the highest score in points should win.
    /// </summary>
    public sealed class HighestPointsScore : Score
    {
        /// <summary>
        /// Initializes a new instance of the HighestPointsScore class with the specified number of points.
        /// </summary>
        /// <param name="points">The number of points that the new instance will represent.</param>
        public HighestPointsScore(double points)
        {
            this.Points = points;
        }

        /// <summary>
        /// Gets the number of points that this score represents.
        /// </summary>
        public double Points
        {
            get;
            private set;
        }

        /// <summary>
        /// Converts the points value of this instance to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of the value of this instance.</returns>
        public override string ToString()
        {
            return this.Points.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return (int)this.Points;
        }

        /// <summary>
        /// Compares the value of this instance to a specified Score value
        /// and returns an integer that indicates whether this instance is better than,
        /// the same as, or worse than the specified Score value.
        /// </summary>
        /// <param name="other">A Score object to compare.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and the value
        /// parameter.  Value Description: Less than zero: This instance is worse than
        /// value. Zero: This instance is the same as value. Greater than zero: This instance
        /// is better than value.
        /// </returns>
        public override int CompareTo(Score other)
        {
            if (other == null)
            {
                return 1;
            }

            var o = other as HighestPointsScore;

            if (o == null)
            {
                throw new InvalidOperationException();
            }

            return this.Points.CompareTo(o.Points);
        }

        /// <summary>
        /// Adds this instance to the specified score.  Used in overloading the '+' operator.
        /// </summary>
        /// <param name="addend">The other score to add to this instance.</param>
        /// <returns>A new instance of Score representing the sum of this instance and the addend.</returns>
        public override Score Add(Score addend)
        {
            if (addend == null)
            {
                return new HighestPointsScore(this.Points);
            }

            var a = addend as HighestPointsScore;
            
            if (a == null)
            {
                throw new InvalidOperationException();
            }

            return new HighestPointsScore(this.Points + a.Points);
        }
    }
}
