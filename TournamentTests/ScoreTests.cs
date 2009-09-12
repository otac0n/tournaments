using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tournaments;

namespace TournamentTests
{
    /// <summary>
    /// Summary description for ScoreTests
    /// </summary>
    [TestClass]
    public class ScoreTests
    {
        public ScoreTests()
        {
        }
        
        [TestMethod]
        public void LowestScoreWins()
        {
            var scoreA = new LowestPointsScore(1.0);
            var scoreB = new LowestPointsScore(2.0);
            var scoreC = new LowestPointsScore(2.0);
            var scoreD = (LowestPointsScore)null;

            Assert.IsTrue(scoreA > scoreD);
            Assert.IsTrue(scoreA >= scoreD);
            Assert.IsTrue(scoreB > scoreD);
            Assert.IsTrue(scoreB >= scoreD);

            Assert.IsTrue(scoreA > scoreB);
            Assert.IsTrue(scoreA >= scoreB);
            Assert.IsTrue(scoreA != scoreB);

            Assert.IsTrue(scoreB >= scoreC);
            Assert.IsTrue(scoreB == scoreC);
            Assert.IsTrue(scoreB <= scoreC);

            Assert.IsTrue(scoreA > scoreC);
            Assert.IsTrue(scoreA >= scoreC);
            Assert.IsTrue(scoreA != scoreC);

            Assert.IsTrue(scoreB < scoreA);
            Assert.IsTrue(scoreB <= scoreA);
            Assert.IsTrue(scoreB != scoreA);

            Assert.IsTrue(scoreC <= scoreB);
            Assert.IsTrue(scoreC == scoreB);
            Assert.IsTrue(scoreC >= scoreB);

            Assert.IsTrue(scoreC < scoreA);
            Assert.IsTrue(scoreC <= scoreA);
            Assert.IsTrue(scoreC != scoreA);
        }

        [TestMethod]
        public void HighestScoreWins()
        {
            var scoreA = new HighestPointsScore(1.0);
            var scoreB = new HighestPointsScore(2.0);
            var scoreC = new HighestPointsScore(2.0);
            var scoreD = (HighestPointsScore)null;

            Assert.IsTrue(scoreA > scoreD);
            Assert.IsTrue(scoreA >= scoreD);
            Assert.IsTrue(scoreB > scoreD);
            Assert.IsTrue(scoreB >= scoreD);

            Assert.IsTrue(scoreA < scoreB);
            Assert.IsTrue(scoreA <= scoreB);
            Assert.IsTrue(scoreA != scoreB);

            Assert.IsTrue(scoreB <= scoreC);
            Assert.IsTrue(scoreB == scoreC);
            Assert.IsTrue(scoreB >= scoreC);

            Assert.IsTrue(scoreA < scoreC);
            Assert.IsTrue(scoreA <= scoreC);
            Assert.IsTrue(scoreA != scoreC);

            Assert.IsTrue(scoreB > scoreA);
            Assert.IsTrue(scoreB >= scoreA);
            Assert.IsTrue(scoreB != scoreA);

            Assert.IsTrue(scoreC >= scoreB);
            Assert.IsTrue(scoreC == scoreB);
            Assert.IsTrue(scoreC <= scoreB);

            Assert.IsTrue(scoreC > scoreA);
            Assert.IsTrue(scoreC >= scoreA);
            Assert.IsTrue(scoreC != scoreA);
        }
    }
}
