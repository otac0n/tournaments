//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="(none)">
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

namespace Tournaments.Sample
{
    using System;
    using System.Linq;
    using System.Windows.Forms;
    using Tournaments.Plugins;
    using Tournaments.Standard;

    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var p = new StandardTournamentsPluginEnumerator();

            var factories = from f in p.EnumerateFactories()
                            let pf = f as IPairingsGeneratorFactory
                            where pf != null
                            select pf;

            var chosen = (new PairingsChooserView(factories)).Run();

            if (chosen != null)
            {
                IPairingsGenerator pg = chosen.Create();

                if (pg == null)
                {
                    MessageBox.Show("There was an error creating an instance of the '" + chosen.Name + "' pairings generator.  :(");
                    return;
                }

                (new TournamentView(pg)).Run();
            }
        }
    }
}
