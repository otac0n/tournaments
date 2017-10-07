using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tournaments;

namespace TournamentTests
{
    [TestClass]
    public class LowestPointsScoreTests
    {
        public LowestPointsScoreTests()
        {
        }

        /// <summary>
        /// Tests that the point value of a LowestPointsScore is exactly the same as the value passed into the constructor.
        /// </summary>
        [TestMethod]
        public void Points_PassesValueThrough()
        {
            var scoreA = new LowestPointsScore(1.337);

            Assert.AreEqual(1.337, scoreA.Points);
        }

        /// <summary>
        /// Tests the LowestPointsScore class' comparison functions.
        /// </summary>
        /// <remarks>
        /// This method needs to be refactored to be more clear.
        /// </remarks>
        [TestMethod]
        public void LowestScoreWins()
        {
            var scoreNull = (LowestPointsScore)null;

            Assert.IsNull(scoreNull + scoreNull);

            var scores = new LowestPointsScore[10];
            for (int i = 0; i < scores.Length; i++)
            {
                scores[i] = new LowestPointsScore((double)i);
            }

            for (int a = 0; a < scores.Length; a++)
            {
                var scoreA = scores[a];
                var valueA = scoreA.Points;

                Assert.IsTrue(scoreA.CompareTo(scoreNull) > 0);
                Assert.IsTrue(scoreA > scoreNull);
                Assert.IsTrue(scoreA >= scoreNull);
                Assert.IsFalse(scoreA == scoreNull);
                Assert.IsFalse(scoreA.Equals((object)scoreNull));
                Assert.IsTrue(scoreA != scoreNull);
                Assert.IsFalse(scoreA < scoreNull);
                Assert.IsFalse(scoreA <= scoreNull);

                Assert.IsFalse(scoreNull > scoreA);
                Assert.IsFalse(scoreNull >= scoreA);
                Assert.IsFalse(scoreNull == scoreA);
                Assert.IsTrue(scoreNull != scoreA);
                Assert.IsTrue(scoreNull < scoreA);
                Assert.IsTrue(scoreNull <= scoreA);

                Assert.IsTrue(scoreA == scoreA + scoreNull);
                Assert.IsTrue(scoreA == scoreNull + scoreA);

                for (int b = 0; b < scores.Length; b++)
                {
                    var scoreB = scores[b];
                    var valueB = scoreB.Points;

                    Assert.AreEqual(valueA.CompareTo(valueB), -scoreA.CompareTo(scoreB));
                    Assert.AreEqual(valueA < valueB, scoreA > scoreB);
                    Assert.AreEqual(valueA <= valueB, scoreA >= scoreB);
                    Assert.AreEqual(valueA == valueB, scoreA == scoreB);
                    Assert.AreEqual(valueA != valueB, scoreA != scoreB);
                    Assert.AreEqual(valueA.Equals(valueB), scoreA.Equals((object)scoreB));
                    Assert.AreEqual(valueA >= valueB, scoreA <= scoreB);
                    Assert.AreEqual(valueA > valueB, scoreA < scoreB);
                }
            }
        }
    }

    [TestClass]
    public class ScoreTests
    {
        public ScoreTests()
        {
        }

        [TestMethod]
        public void UnlikeScoresAreIncompatible()
        {
            var scores = new Score[]
            {
                new HighestPointsScore(1),
                new LowestPointsScore(1),
                new HighestTimeScore(1),
                new LowestTimeScore(1),
            };

            for (int i = 0; i < scores.Length; i++)
            {
                for (int j = i + 1; j < scores.Length; j++)
                {
                    try
                    {
                        if (scores[i] == scores[j])
                        {
                            Assert.Fail();
                        }
                        Assert.Fail();
                    }
                    catch (InvalidOperationException)
                    {
                    }

                    try
                    {
                        var q = scores[i] + scores[j];
                        Assert.Fail();
                    }
                    catch (InvalidOperationException)
                    {
                    }
                }
            }
        }

        [TestMethod]
        public void ScoresPassValueThrough()
        {
            var scoreB = new HighestPointsScore(1.337);
            var scoreC = new LowestTimeScore(1337);
            var scoreD = new HighestTimeScore(1337);

            Assert.AreEqual(1.337, scoreB.Points);
            Assert.AreEqual(1337, scoreC.Milliseconds);
            Assert.AreEqual(1337, scoreD.Milliseconds);
        }

        [TestMethod]
        public void HighestScoreWins()
        {
            var scoreNull = (HighestPointsScore)null;

            Assert.IsNull(scoreNull + scoreNull);

            var scores = new HighestPointsScore[10];
            for (int i = 0; i < scores.Length; i++)
            {
                scores[i] = new HighestPointsScore((double)i);
            }

            for (int a = 0; a < scores.Length; a++)
            {
                var scoreA = scores[a];
                var valueA = scoreA.Points;

                Assert.IsTrue(scoreA.CompareTo(scoreNull) > 0);
                Assert.IsTrue(scoreA > scoreNull);
                Assert.IsTrue(scoreA >= scoreNull);
                Assert.IsFalse(scoreA == scoreNull);
                Assert.IsFalse(scoreA.Equals((object)scoreNull));
                Assert.IsTrue(scoreA != scoreNull);
                Assert.IsFalse(scoreA < scoreNull);
                Assert.IsFalse(scoreA <= scoreNull);

                Assert.IsFalse(scoreNull > scoreA);
                Assert.IsFalse(scoreNull >= scoreA);
                Assert.IsFalse(scoreNull == scoreA);
                Assert.IsTrue(scoreNull != scoreA);
                Assert.IsTrue(scoreNull < scoreA);
                Assert.IsTrue(scoreNull <= scoreA);

                Assert.IsTrue(scoreA == scoreA + scoreNull);
                Assert.IsTrue(scoreA == scoreNull + scoreA);

                for (int b = 0; b < scores.Length; b++)
                {
                    var scoreB = scores[b];
                    var valueB = scoreB.Points;

                    Assert.AreEqual(valueA.CompareTo(valueB), scoreA.CompareTo(scoreB));
                    Assert.AreEqual(valueA > valueB, scoreA > scoreB);
                    Assert.AreEqual(valueA >= valueB, scoreA >= scoreB);
                    Assert.AreEqual(valueA == valueB, scoreA == scoreB);
                    Assert.AreEqual(valueA != valueB, scoreA != scoreB);
                    Assert.AreEqual(valueA.Equals(valueB), scoreA.Equals((object)scoreB));
                    Assert.AreEqual(valueA <= valueB, scoreA <= scoreB);
                    Assert.AreEqual(valueA < valueB, scoreA < scoreB);
                }
            }
        }

        [TestMethod]
        public void LowestTimeWins()
        {
            var scoreNull = (LowestTimeScore)null;

            Assert.IsNull(scoreNull + scoreNull);

            var scores = new LowestTimeScore[10];
            for (int i = 0; i < scores.Length; i++)
            {
                scores[i] = new LowestTimeScore(i);
            }

            for (int a = 0; a < scores.Length; a++)
            {
                var scoreA = scores[a];
                var valueA = scoreA.Milliseconds;

                Assert.IsTrue(scoreA.CompareTo(scoreNull) > 0);
                Assert.IsTrue(scoreA > scoreNull);
                Assert.IsTrue(scoreA >= scoreNull);
                Assert.IsFalse(scoreA == scoreNull);
                Assert.IsFalse(scoreA.Equals((object)scoreNull));
                Assert.IsTrue(scoreA != scoreNull);
                Assert.IsFalse(scoreA < scoreNull);
                Assert.IsFalse(scoreA <= scoreNull);

                Assert.IsFalse(scoreNull > scoreA);
                Assert.IsFalse(scoreNull >= scoreA);
                Assert.IsFalse(scoreNull == scoreA);
                Assert.IsTrue(scoreNull != scoreA);
                Assert.IsTrue(scoreNull < scoreA);
                Assert.IsTrue(scoreNull <= scoreA);

                Assert.IsTrue(scoreA == scoreA + scoreNull);
                Assert.IsTrue(scoreA == scoreNull + scoreA);

                for (int b = 0; b < scores.Length; b++)
                {
                    var scoreB = scores[b];
                    var valueB = scoreB.Milliseconds;

                    Assert.AreEqual(valueA.CompareTo(valueB), -scoreA.CompareTo(scoreB));
                    Assert.AreEqual(valueA < valueB, scoreA > scoreB);
                    Assert.AreEqual(valueA <= valueB, scoreA >= scoreB);
                    Assert.AreEqual(valueA == valueB, scoreA == scoreB);
                    Assert.AreEqual(valueA != valueB, scoreA != scoreB);
                    Assert.AreEqual(valueA.Equals(valueB), scoreA.Equals((object)scoreB));
                    Assert.AreEqual(valueA >= valueB, scoreA <= scoreB);
                    Assert.AreEqual(valueA > valueB, scoreA < scoreB);
                }
            }
        }

        [TestMethod]
        public void HighestTimeWins()
        {
            var scoreNull = (HighestTimeScore)null;

            Assert.IsNull(scoreNull + scoreNull);

            var scores = new HighestTimeScore[10];
            for (int i = 0; i < scores.Length; i++)
            {
                scores[i] = new HighestTimeScore(i);
            }

            for (int a = 0; a < scores.Length; a++)
            {
                var scoreA = scores[a];
                var valueA = scoreA.Milliseconds;

                Assert.IsTrue(scoreA.CompareTo(scoreNull) > 0);
                Assert.IsTrue(scoreA > scoreNull);
                Assert.IsTrue(scoreA >= scoreNull);
                Assert.IsFalse(scoreA == scoreNull);
                Assert.IsFalse(scoreA.Equals((object)scoreNull));
                Assert.IsTrue(scoreA != scoreNull);
                Assert.IsFalse(scoreA < scoreNull);
                Assert.IsFalse(scoreA <= scoreNull);

                Assert.IsFalse(scoreNull > scoreA);
                Assert.IsFalse(scoreNull >= scoreA);
                Assert.IsFalse(scoreNull == scoreA);
                Assert.IsTrue(scoreNull != scoreA);
                Assert.IsTrue(scoreNull < scoreA);
                Assert.IsTrue(scoreNull <= scoreA);

                Assert.IsTrue(scoreA == scoreA + scoreNull);
                Assert.IsTrue(scoreA == scoreNull + scoreA);

                for (int b = 0; b < scores.Length; b++)
                {
                    var scoreB = scores[b];
                    var valueB = scoreB.Milliseconds;

                    Assert.AreEqual(valueA.CompareTo(valueB), scoreA.CompareTo(scoreB));
                    Assert.AreEqual(valueA > valueB, scoreA > scoreB);
                    Assert.AreEqual(valueA >= valueB, scoreA >= scoreB);
                    Assert.AreEqual(valueA == valueB, scoreA == scoreB);
                    Assert.AreEqual(valueA != valueB, scoreA != scoreB);
                    Assert.AreEqual(valueA.Equals(valueB), scoreA.Equals((object)scoreB));
                    Assert.AreEqual(valueA <= valueB, scoreA <= scoreB);
                    Assert.AreEqual(valueA < valueB, scoreA < scoreB);
                }
            }
        }
    }
}