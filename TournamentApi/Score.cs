//-----------------------------------------------------------------------
// <copyright file="Score.cs" company="(none)">
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
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Provides an abstraction of a score in a tournament.
    /// </summary>
    public abstract class Score : IComparable<Score>, IEquatable<Score>
    {
        /// <summary>
        /// Adds one Score to another yielding their sum.
        /// </summary>
        /// <param name="lhs">The first addend.</param>
        /// <param name="rhs">The second addend.</param>
        /// <returns>A new Score representing the sum of the two addends.</returns>
        public static Score operator +(Score lhs, Score rhs)
        {
            if (lhs != null)
            {
                return lhs.AddWith(rhs);
            }
            else if (rhs != null)
            {
                return rhs.AddWith(lhs);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        ///  Determines whether two specified instances of Score are equal.
        /// </summary>
        /// <param name="lhs">The first score for which to check equality.</param>
        /// <param name="rhs">The second score for which to check equality.</param>
        /// <returns>true if rhs and lhs represent the same score; otherwise, false.</returns>
        public static bool operator ==(Score lhs, Score rhs)
        {
            if (object.ReferenceEquals(lhs, rhs))
            {
                return true;
            }
            else if ((object)lhs == null || (object)rhs == null)
            {
                return false;
            }
            else
            {
                return lhs.CompareTo(rhs) == 0;
            }
        }

        /// <summary>
        ///  Determines whether two specified instances of Score are not equal.
        /// </summary>
        /// <param name="lhs">The first score for which to check inequality.</param>
        /// <param name="rhs">The second score for which to check inequality.</param>
        /// <returns>true if rhs and lhs represent a different score; otherwise, false.</returns>
        public static bool operator !=(Score lhs, Score rhs)
        {
            if (object.ReferenceEquals(lhs, rhs))
            {
                return false;
            }
            else if ((object)lhs == null || (object)rhs == null)
            {
                return true;
            }
            else
            {
                return lhs.CompareTo(rhs) != 0;
            }
        }

        /// <summary>
        /// Determines whether or not one specified Score is better than another specified Score.
        /// </summary>
        /// <param name="lhs">The first score for which to check inequality.</param>
        /// <param name="rhs">The second score for which to check inequality.</param>
        /// <returns>true if lhs represent a better score than rhs; otherwise, false.</returns>
        public static bool operator >(Score lhs, Score rhs)
        {
            if (object.ReferenceEquals(lhs, rhs))
            {
                return false;
            }
            else if ((object)lhs != null || (object)rhs == null)
            {
                return true;
            }
            else if ((object)lhs == null || (object)rhs != null)
            {
                return false;
            }
            else
            {
                return lhs.CompareTo(rhs) > 0;
            }
        }

        /// <summary>
        /// Determines whether or not one specified Score is worse than another specified Score.
        /// </summary>
        /// <param name="lhs">The first score for which to check inequality.</param>
        /// <param name="rhs">The second score for which to check inequality.</param>
        /// <returns>true if lhs represent a worse score than rhs; otherwise, false.</returns>
        public static bool operator <(Score lhs, Score rhs)
        {
            if (object.ReferenceEquals(lhs, rhs))
            {
                return false;
            }
            else if ((object)lhs != null || (object)rhs == null)
            {
                return false;
            }
            else if ((object)lhs == null || (object)rhs != null)
            {
                return true;
            }
            else
            {
                return lhs.CompareTo(rhs) < 0;
            }
        }

        /// <summary>
        /// Determines whether or not one specified Score is better than or equal to another specified Score.
        /// </summary>
        /// <param name="lhs">The first score for which to check inequality.</param>
        /// <param name="rhs">The second score for which to check inequality.</param>
        /// <returns>true if lhs represent a score that is better than or equal to rhs; otherwise, false.</returns>
        public static bool operator >=(Score lhs, Score rhs)
        {
            if (object.ReferenceEquals(lhs, rhs))
            {
                return true;
            }
            else if ((object)lhs != null || (object)rhs == null)
            {
                return true;
            }
            else if ((object)lhs == null || (object)rhs != null)
            {
                return false;
            }
            else
            {
                return lhs.CompareTo(rhs) >= 0;
            }
        }

        /// <summary>
        /// Determines whether or not one specified Score is worse than or equal to another specified Score.
        /// </summary>
        /// <param name="lhs">The first score for which to check inequality.</param>
        /// <param name="rhs">The second score for which to check inequality.</param>
        /// <returns>true if lhs represent a score that is worse than or equal to rhs; otherwise, false.</returns>
        public static bool operator <=(Score lhs, Score rhs)
        {
            if (object.ReferenceEquals(lhs, rhs))
            {
                return true;
            }
            else if ((object)lhs != null || (object)rhs == null)
            {
                return false;
            }
            else if ((object)lhs == null || (object)rhs != null)
            {
                return true;
            }
            else
            {
                return lhs.CompareTo(rhs) <= 0;
            }
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to the specified Score instance.
        /// </summary>
        /// <param name="value">A Score instance to compare to this instance.</param>
        /// <returns>true if the value parameter equals the value of this instance; otherwise, false.</returns>
        public bool Equals(Score value)
        {
            return this == value;
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to the specified object.
        /// </summary>
        /// <param name="value">An object to compare to this instance.</param>
        /// <returns>true if the value parameter is a Score instance and equals the value of this instance; otherwise, false.</returns>
        public override bool Equals(object value)
        {
            if (!(value is Score))
            {
                return false;
            }

            return this.Equals((Score)value);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public abstract override int GetHashCode();

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
        public abstract int CompareTo(Score other);

        /// <summary>
        /// Adds this instance to the specified score.  Used in overloading the '+' operator.
        /// </summary>
        /// <param name="addend">The other score to add to this instance.</param>
        /// <returns>A new instance of Score representing the sum of this instance and the addend.</returns>
        protected abstract Score AddWith(Score addend);
    }
}
