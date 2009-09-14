//-----------------------------------------------------------------------
// <copyright file="PluginLoader.cs" company="(none)">
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
    using System.IO;
    using System.Reflection;
    using System.Security;

    /// <summary>
    /// Provides methods to load plugins from external assemblies.
    /// </summary>
    public sealed class PluginLoader
    {
        /// <summary>
        /// Prevents a default instance of the PluginLoader class from being created.
        /// </summary>
        private PluginLoader()
        {
        }

        /// <summary>
        /// Loads the plugins from an assembly specified by filename.
        /// </summary>
        /// <param name="fileName">The filename of the assembly to load.</param>
        /// <returns>The plugin factories contained in the assembly, if the load was successful; null, otherwise.</returns>
        public static IEnumerable<IPluginFactory> LoadPlugins(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return null;
            }
            else
            {
                return LoadPlugins(File.ReadAllBytes(fileName));
            }
        }

        /// <summary>
        /// Loads the plugins from a specified assembly.
        /// </summary>
        /// <param name="rawAssembly">The raw assembly from which to load.</param>
        /// <returns>The plugin factories contained in the assembly, if the load was successful; null, otherwise.</returns>
        public static IEnumerable<IPluginFactory> LoadPlugins(byte[] rawAssembly)
        {
            try
            {
                // TODO: Check if assembly is signed.
                Assembly assembly = Assembly.Load(rawAssembly);

                return LoadPlugins(assembly);
            }
            catch (BadImageFormatException)
            {
                return null;
            }
        }

        /// <summary>
        /// Loads the plugins from a specified assembly.
        /// </summary>
        /// <param name="rawAssembly">The assembly from which to load.</param>
        /// <returns>The plugin factories contained in the assembly, if the load was successful; null, otherwise.</returns>
        public static IEnumerable<IPluginFactory> LoadPlugins(Assembly assembly)
        {
            List<IPluginFactory> factories = new List<IPluginFactory>();

            try
            {
                foreach (Type type in assembly.GetTypes())
                {
                    IPluginEnumerator instance = null;

                    if (type.GetInterface("IPluginEnumerator") != null)
                    {
                        instance = (IPluginEnumerator)Activator.CreateInstance(type);
                    }

                    if (instance != null)
                    {
                        factories.AddRange(instance.EnumerateFactories());
                    }
                }
            }
            catch (SecurityException)
            {
                return null;
            }
            catch (ReflectionTypeLoadException)
            {
                return null;
            }

            return factories.AsReadOnly();
        }
    }
}
