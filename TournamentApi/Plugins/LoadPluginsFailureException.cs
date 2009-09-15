//-----------------------------------------------------------------------
// <copyright file="LoadPluginsFailureException.cs" company="(none)">
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Thrown when the PluginLoader encounters an error when loading from an assembly.
    /// </summary>
    [global::System.Serializable]
    public class LoadPluginsFailureException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the LoadPluginsFailureException class.
        /// </summary>
        public LoadPluginsFailureException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the LoadPluginsFailureException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public LoadPluginsFailureException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the LoadPluginsFailureException class with a specified error
        /// message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public LoadPluginsFailureException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the LoadPluginsFailureException class with serialized data.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        /// <exception cref="System.ArgumentNullException">The info parameter is null.</exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException">The class name is null or System.Exception.HResult is zero (0).</exception>
        protected LoadPluginsFailureException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
