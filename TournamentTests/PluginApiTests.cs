using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tournaments.Plugins;

namespace TournamentTests
{
    [TestClass]
    public class PluginApiTests
    {
        [TestMethod]
        public void BadImageReturnsEmptySet()
        {
            var plugins = PluginLoader.LoadPlugins(@"c:\WINDOWS\System32\user32.dll");
            Assert.AreEqual(null, plugins);
        }

        [TestMethod]
        public void FileNotFoundReturnsNull()
        {
            var plugins = PluginLoader.LoadPlugins(@"C:\filenotfound.dll");
            Assert.AreEqual(null, plugins);
        }
    }
}
