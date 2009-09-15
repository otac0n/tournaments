using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tournaments.Plugins;
using Tournaments;
using System.Reflection;

namespace TournamentTests
{
    public class TestPluginEnumerator : IPluginEnumerator
    {
        public IEnumerable<IPluginFactory> EnumerateFactories()
        {
            yield return new PairingsGeneratorFactory<StubTournament>();
            yield return new TournamentVisualizerFactory<StubTournament>();
        }
    }

    public class StubTournament : ITournamentVisualizer, IPairingsGenerator
    {
        public string Name
        {
            get { return "Stub"; }
        }

        #region IPairingsGenerator Members


        public PairingsGeneratorState State
        {
            get { throw new NotImplementedException(); }
        }

        public bool SupportsLateEntry
        {
            get { throw new NotImplementedException(); }
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public TournamentRound CreateNextRound(int? places)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TournamentRanking> GenerateRankings()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ITournamentVisualizer Members

        public void LoadState(IEnumerable<TournamentTeam> teams, IList<TournamentRound> rounds)
        {
            throw new NotImplementedException();
        }

        public System.Xml.XmlReader Render(TournamentNameTable teamNames)
        {
            throw new NotImplementedException();
        }

        public System.Drawing.Size Measure(TournamentNameTable teamNames)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    [TestClass]
    public class PluginApiTests
    {
        [TestMethod]
        public void BadImageThrowsException()
        {
            try
            {
                var plugins = PluginLoader.LoadPlugins(@"c:\WINDOWS\System32\user32.dll");
                Assert.Fail();
            }
            catch (LoadPluginsFailureException)
            {
                return;
            }
        }

        [TestMethod]
        public void FileNotFoundThrowsException()
        {
            try
            {
                var plugins = PluginLoader.LoadPlugins(@"C:\filenotfound.dll");
                Assert.Fail();
            }
            catch (LoadPluginsFailureException)
            {
                return;
            }
        }

        [TestMethod]
        public void TournamentVisualizerFactoryWorks()
        {
            var f = new TournamentVisualizerFactory<StubTournament>();
            var t = f.Create();
            Assert.IsNotNull(t);
            Assert.AreEqual(t.Name, f.Name);
        }

        [TestMethod]
        public void PairingsGeneratorFactoryWorks()
        {
            var f = new PairingsGeneratorFactory<StubTournament>();
            var t = f.Create();
            Assert.IsNotNull(t);
            Assert.AreEqual(t.Name, f.Name);
        }

        [TestMethod]
        public void PluginLoaderLoadsFromCurrentAssembly()
        {
            var plugins = PluginLoader.LoadPlugins(Assembly.GetExecutingAssembly());
            Assert.IsNotNull(plugins);
        }

        [TestMethod]
        public void PluginLoaderLoadsStandardTournaments()
        {
            var plugins = PluginLoader.LoadPlugins("StandardTournaments.dll");
            Assert.IsNotNull(plugins);
        }
    }
}
