//-----------------------------------------------------------------------
// <copyright file="PairingsChooserView.cs" company="(none)">
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using Tournaments.Plugins;

    public partial class PairingsChooserView : Form
    {
        private bool okPressed = false;

        private IPairingsGeneratorFactory[] factories;

        public PairingsChooserView(IEnumerable<IPairingsGeneratorFactory> factories)
        {
            InitializeComponent();

            this.factories = factories.ToArray();

            this.Generators.Items.AddRange((from f in this.factories
                                            select f.Name).ToArray());
        }

        public IPairingsGeneratorFactory ChosenGenerator
        {
            get;
            private set;
        }

        public IPairingsGeneratorFactory Run()
        {
            Application.Run(this);

            return this.ChosenGenerator;
        }

        private void Generators_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var factory = this.factories[this.Generators.SelectedIndex];
            this.ChosenGenerator = factory;
            this.Ok.Enabled = true;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            this.okPressed = true;
            this.Close();
        }

        private void PairingsChooserView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!okPressed)
            {
                this.ChosenGenerator = null;
            }
        }
    }
}
