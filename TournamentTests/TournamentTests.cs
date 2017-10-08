using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tournaments;
using Tournaments.Standard;

namespace TournamentTests
{
    [TestClass]
    public class TournamentTests
    {
        Random r = new Random();

        private void IPairingsGeneratorThrowsExceptionOnNullParameters(IPairingsGenerator pg)
        {
            try
            {
                pg.LoadState(null, null);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
                return;
            }
        }

        [TestMethod]
        public void TounamentTeamScoreRejectsNullParameters()
        {
            try
            {
                TournamentTeamScore ts = new TournamentTeamScore(null, null);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
                return;
            }
        }

        [TestMethod]
        public void TounamentRoundRejectsNullParameters()
        {
            try
            {
                TournamentRound ts = new TournamentRound(null);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
                return;
            }
        }

        [TestMethod]
        public void BoilOffThrowsExceptionOnNullParameters()
        {
            IPairingsGenerator bopg = new BoilOffPairingsGenerator();
            IPairingsGeneratorThrowsExceptionOnNullParameters(bopg);
        }

        [TestMethod]
        public void BoilOffThrowsExceptionOnUnfinishedRound()
        {
            IPairingsGenerator bopg = new BoilOffPairingsGenerator();
            List<TournamentTeam> teams = new List<TournamentTeam>(CreateTeams(9));
            List<TournamentRound> rounds = new List<TournamentRound>();
            bopg.LoadState(teams, rounds);
            var round = bopg.CreateNextRound(null);
            rounds.Add(round);

            round.Pairings[0].TeamScores[0].Score = new HighestPointsScore(1.0);

            bopg.LoadState(teams, rounds);

            try
            {
                round = bopg.CreateNextRound(null);
                Assert.Fail();
            }
            catch (InvalidTournamentStateException)
            {
                return;
            }
        }

        [TestMethod]
        public void BoilOffManyTeams()
        {
            IPairingsGenerator bopg = new BoilOffPairingsGenerator();

            List<TournamentTeam> teams = new List<TournamentTeam>(CreateTeams(9));
            List<TournamentRound> rounds = new List<TournamentRound>();

            try
            {
                bopg.LoadState(teams, rounds);
                DisplayTournamentRound(bopg.CreateNextRound(null));
            }
            catch (ArgumentNullException)
            {
            }
        }

        [TestMethod]
        public void BoilOffThreeTeamsIsOneRound()
        {
            IPairingsGenerator bopg = new BoilOffPairingsGenerator();

            List<TournamentTeam> teams = new List<TournamentTeam>(CreateTeams(3));
            List<TournamentRound> rounds = new List<TournamentRound>();

            try
            {
                bopg.LoadState(teams, rounds);
                TournamentRound round = bopg.CreateNextRound(null);
                foreach (var pairing in round.Pairings)
                {
                    foreach (var teamScore in pairing.TeamScores)
                    {
                        teamScore.Score = new HighestPointsScore(r.Next(20));
                    }
                }
                rounds.Add(round);

                bopg.LoadState(teams, rounds);
                round = bopg.CreateNextRound(null);
                Assert.AreEqual(null, round);
            }
            catch (ArgumentNullException)
            {
            }
        }

        [TestMethod]
        public void BoilOffHandlesManyCompetitorsWell()
        {
            IPairingsGenerator pg = new BoilOffPairingsGenerator();

            List<TournamentTeam> teams = new List<TournamentTeam>(CreateTeams(50));
            List<TournamentRound> rounds = new List<TournamentRound>();

            try
            {
                RunTournament(pg, teams, rounds, true, null);

                DisplayTournamentRounds(rounds);
                DisplayTournamentRankings(pg.GenerateRankings());
            }
            catch (InvalidTournamentStateException)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void RoundRobinThrowsExceptionOnNullParameters()
        {
            IPairingsGenerator rrpg = new RoundRobinPairingsGenerator();
            IPairingsGeneratorThrowsExceptionOnNullParameters(rrpg);
        }

        [TestMethod]
        public void RoundRobinCreatesThreeRoundsForThreeCompetitors()
        {
            IPairingsGenerator rrpg = new RoundRobinPairingsGenerator();
            List<TournamentTeam> teams = new List<TournamentTeam>(CreateTeams(3));

            try
            {
                List<TournamentRound> rounds = new List<TournamentRound>();
                rounds.AddRange(RounRobinBuildAllPairings(teams, rrpg));

                Assert.AreEqual(3, rounds.Count);

                DisplayTournamentRounds(rounds);
            }
            catch (InvalidTournamentStateException)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void RoundRobinHandlesManyCompetitorsWell()
        {
            IPairingsGenerator pg = new RoundRobinPairingsGenerator();

            List<TournamentTeam> teams = new List<TournamentTeam>(CreateTeams(30));
            List<TournamentRound> rounds = new List<TournamentRound>();

            try
            {
                RunTournament(pg, teams, rounds, true, null);

                DisplayTournamentRounds(rounds);
                DisplayTournamentRankings(pg.GenerateRankings());
            }
            catch (InvalidTournamentStateException)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void RoundRobinTieForSecond()
        {
            IPairingsGenerator rrpg = new RoundRobinPairingsGenerator();

            List<TournamentTeam> teams = new List<TournamentTeam>(CreateTeams(3));

            try
            {
                List<TournamentRound> rounds = new List<TournamentRound>();
                rounds.AddRange(RounRobinBuildAllPairings(teams, rrpg));

                Assert.AreEqual(3, rounds.Count);

                rounds[0].Pairings[0].TeamScores[0].Score = new HighestPointsScore(1.0d);  // A: Win
                rounds[0].Pairings[0].TeamScores[1].Score = new HighestPointsScore(0.0d);  // B: Loss

                rounds[1].Pairings[0].TeamScores[0].Score = new HighestPointsScore(1.0d);  // A: Win
                rounds[1].Pairings[0].TeamScores[1].Score = new HighestPointsScore(0.0d);  // C: Loss

                rounds[2].Pairings[0].TeamScores[0].Score = new HighestPointsScore(0.5d);  // B: Draw
                rounds[2].Pairings[0].TeamScores[1].Score = new HighestPointsScore(0.5d);  // C: Draw

                DisplayTournamentRounds(rounds);

                rrpg.LoadState(teams, rounds);
                List<TournamentRanking> rankings = new List<TournamentRanking>(rrpg.GenerateRankings());

                DisplayTournamentRankings(rankings);

                Assert.AreEqual(3, rankings.Count);
                Assert.AreEqual(1, rankings[0].Rank);
                Assert.AreEqual(2, rankings[1].Rank);
                Assert.AreEqual(2, rankings[2].Rank);
            }
            catch (InvalidTournamentStateException)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void SingleEliminationThrowsExceptionOnNullParameters()
        {
            IPairingsGenerator pg = new EliminationTournament(1);

            try
            {
                pg.LoadState(null, null);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
            }
        }

        [TestMethod]
        public void SingleEliminationLastPersonNeverGetsABye()
        {
            IPairingsGenerator pg = new EliminationTournament(1);

            for (int i = 2; i <= 33; i++)
            {
                List<TournamentTeam> teams = new List<TournamentTeam>(CreateTeams(i));
                List<TournamentRound> rounds = new List<TournamentRound>();

                pg.LoadState(teams, rounds);

                TournamentTeam targetTeam = teams[teams.Count - 1];

                TournamentRound round = pg.CreateNextRound(null);

                var targetTeamPairings = from p in round.Pairings
                                         where p.TeamScores.Where(ts => ts.Team.TeamId == targetTeam.TeamId).Any()
                                         select p;

                var pairingsThatAreByes = from ttp in targetTeamPairings
                                          where ttp.TeamScores.Count() == 1
                                          select ttp;

                if (pairingsThatAreByes.Any())
                {
                    Assert.Fail();
                }
            }
        }

        [TestMethod]
        public void SingleEliminationTieDisallowed()
        {
            IPairingsGenerator pg = new EliminationTournament(1);

            List<TournamentTeam> teams = new List<TournamentTeam>(CreateTeams(2));
            List<TournamentRound> rounds = new List<TournamentRound>();

            pg.LoadState(teams, rounds);
            TournamentRound round = pg.CreateNextRound(null);
            round.Pairings[0].TeamScores[0].Score = new HighestPointsScore(10);
            round.Pairings[0].TeamScores[1].Score = new HighestPointsScore(10);
            rounds.Add(round);

            try
            {
                RunTournament(pg, teams, rounds, false, null);
                Assert.Fail();
            }
            catch (InvalidTournamentStateException)
            {
                return;
            }
        }

        [TestMethod]
        public void SingleEliminationHandlesManyCompetitorsWell()
        {
            IPairingsGenerator pg = new EliminationTournament(1);

            for (int i = 30; i <= 40; i++)
            {
                List<TournamentTeam> teams = new List<TournamentTeam>(CreateTeams(i));
                List<TournamentRound> rounds = new List<TournamentRound>();

                Dictionary<long, string> teamNames = new Dictionary<long, string>();
                foreach (var team in teams)
                {
                    teamNames.Add(team.TeamId, "Team#" + team.TeamId);
                }

                TournamentNameTable nameTable = new TournamentNameTable(teamNames);

                try
                {
                    RunTournament(pg, teams, rounds, false, nameTable);

                    DisplayTournamentRounds(rounds);
                    DisplayTournamentRankings(pg.GenerateRankings());
                }
                catch (InvalidTournamentStateException)
                {
                    throw;
                }
            }
        }

        [TestMethod]
        public void SingleEliminationHandlesOutOfOrderCompetitors()
        {
            for (int i = 0; i < 10; i++)
            {
                var teamNames = new Dictionary<long, string>();
                teamNames[0] = "A";
                teamNames[1] = "B";
                teamNames[2] = "C";
                teamNames[3] = "D";
                teamNames[4] = "E";
                teamNames[5] = "F";

                var teams = (from k in teamNames.Keys
                             orderby k
                             select new TournamentTeam(k, r.Next(1000))).ToList();

                var round1 = new TournamentRound(
                                new TournamentPairing(
                                    new TournamentTeamScore(teams[0], Score(1)),
                                    new TournamentTeamScore(teams[1], Score(2))),
                                new TournamentPairing(
                                    new TournamentTeamScore(teams[2], Score(3)),
                                    new TournamentTeamScore(teams[3], Score(4))));
                var round2 = new TournamentRound(
                                new TournamentPairing(
                                    new TournamentTeamScore(teams[1], Score(5)),
                                    new TournamentTeamScore(teams[3], Score(6))));
                var round3 = new TournamentRound(
                                new TournamentPairing(
                                    new TournamentTeamScore(teams[4], Score(7)),
                                    new TournamentTeamScore(teams[5], Score(8))));
                var round4 = new TournamentRound(
                                new TournamentPairing(
                                    new TournamentTeamScore(teams[5], Score(9)),
                                    new TournamentTeamScore(teams[3], Score(10))));

                var rounds = (new[] { round1, round2, round3, round4 }).ToList();

                IPairingsGenerator pg = new EliminationTournament(1);

                pg.LoadState(teams, rounds);
                Assert.IsNull(pg.CreateNextRound(null));
                DisplayTournamentRankings(pg.GenerateRankings());
            }

            for (int i = 0; i < 10; i++)
            {
                var teamNames = new Dictionary<long, string>();
                teamNames[0] = "A";
                teamNames[1] = "B";
                teamNames[2] = "C";
                teamNames[3] = "D";
                teamNames[4] = "E";
                teamNames[5] = "F";

                var teams = (from k in teamNames.Keys
                             orderby k
                             select new TournamentTeam(k, r.Next(1000))).ToList();

                var round1 = new TournamentRound(
                                new TournamentPairing(
                                    new TournamentTeamScore(teams[0], Score(1)),
                                    new TournamentTeamScore(teams[1], Score(2))),
                                new TournamentPairing(
                                    new TournamentTeamScore(teams[2], Score(3)),
                                    new TournamentTeamScore(teams[3], Score(4))));
                var round2 = new TournamentRound(
                                new TournamentPairing(
                                    new TournamentTeamScore(teams[1], Score(5)),
                                    new TournamentTeamScore(teams[4], Score(6))));
                var round3 = new TournamentRound(
                                new TournamentPairing(
                                    new TournamentTeamScore(teams[3], Score(7)),
                                    new TournamentTeamScore(teams[5], Score(8))));
                var round4 = new TournamentRound(
                                new TournamentPairing(
                                    new TournamentTeamScore(teams[4], Score(9)),
                                    new TournamentTeamScore(teams[5], Score(10))));

                var rounds = (new[] { round1, round2, round3, round4 }).ToList();

                IPairingsGenerator pg = new EliminationTournament(1);

                pg.LoadState(teams, rounds);
                Assert.IsNull(pg.CreateNextRound(null));
                DisplayTournamentRankings(pg.GenerateRankings());
            }
        }

        [TestMethod]
        public void DoubleEliminationTieDisallowed()
        {
            IPairingsGenerator pg = new EliminationTournament(2);

            List<TournamentTeam> teams = new List<TournamentTeam>(CreateTeams(2));
            List<TournamentRound> rounds = new List<TournamentRound>();

            pg.LoadState(teams, rounds);
            TournamentRound round = pg.CreateNextRound(null);
            round.Pairings[0].TeamScores[0].Score = new HighestPointsScore(10);
            round.Pairings[0].TeamScores[1].Score = new HighestPointsScore(10);
            rounds.Add(round);

            try
            {
                RunTournament(pg, teams, rounds, false, null);
                Assert.Fail();
            }
            catch (InvalidTournamentStateException)
            {
                return;
            }
        }

        [TestMethod]
        public void DoubleEliminationLongTournament()
        {
            IPairingsGenerator pg = new EliminationTournament(2);

            List<TournamentTeam> teams = new List<TournamentTeam>(CreateTeams(5));
            List<TournamentRound> rounds = new List<TournamentRound>();

            pg.LoadState(teams, rounds);
            TournamentRound round1 = pg.CreateNextRound(null);
            round1.Pairings[0].TeamScores[0].Score = new HighestPointsScore(1);
            round1.Pairings[0].TeamScores[1].Score = new HighestPointsScore(2);
            round1.Pairings[1].TeamScores[0].Score = new HighestPointsScore(1);
            round1.Pairings[1].TeamScores[1].Score = new HighestPointsScore(2);
            rounds.Add(round1);
            pg.LoadState(teams, rounds);
            TournamentRound round2 = pg.CreateNextRound(null);
            round2.Pairings[0].TeamScores[0].Score = new HighestPointsScore(1);
            round2.Pairings[0].TeamScores[1].Score = new HighestPointsScore(2);
            rounds.Add(round2);
            pg.LoadState(teams, rounds);
            TournamentRound round3 = pg.CreateNextRound(null);
            round3.Pairings[0].TeamScores[0].Score = new HighestPointsScore(2);
            round3.Pairings[0].TeamScores[1].Score = new HighestPointsScore(1);
            round3.Pairings[1].TeamScores[0].Score = new HighestPointsScore(2);
            round3.Pairings[1].TeamScores[1].Score = new HighestPointsScore(1);
            rounds.Add(round3);
            pg.LoadState(teams, rounds);
            TournamentRound round4 = pg.CreateNextRound(null);
            round4.Pairings[0].TeamScores[0].Score = new HighestPointsScore(2);
            round4.Pairings[0].TeamScores[1].Score = new HighestPointsScore(1);
            rounds.Add(round4);
            pg.LoadState(teams, rounds);
            TournamentRound round5 = pg.CreateNextRound(null);
            round5.Pairings[0].TeamScores[0].Score = new HighestPointsScore(1);
            round5.Pairings[0].TeamScores[1].Score = new HighestPointsScore(2);
            rounds.Add(round5);
            pg.LoadState(teams, rounds);
            TournamentRound round6 = pg.CreateNextRound(null);
            round6.Pairings[0].TeamScores[0].Score = new HighestPointsScore(2);
            round6.Pairings[0].TeamScores[1].Score = new HighestPointsScore(1);
            rounds.Add(round6);

            RunTournament(pg, teams, rounds, false, null);

            DisplayTournamentRounds(rounds);
            DisplayTournamentRankings(pg.GenerateRankings());
        }

        private Score Score(double score)
        {
            return new HighestPointsScore(score);
        }

        public void RunTournament(IPairingsGenerator pg, List<TournamentTeam> teams, List<TournamentRound> rounds, bool allowTies, TournamentNameTable nameTable)
        {
            ITournamentVisualizer viz = null;
            if (nameTable != null)
            {
                viz = pg as ITournamentVisualizer;
            }

            while (true)
            {
                pg.LoadState(teams, rounds);
                TournamentRound newRound = pg.CreateNextRound(null);

                if (viz != null)
                {
                    var gfx = new SystemGraphics();
                    var q2 = viz.Measure(gfx, nameTable);
                    viz.Render(gfx, nameTable);
                }

                if (newRound == null)
                {
                    pg.LoadState(teams, rounds);
                    newRound = pg.CreateNextRound(null);
                    break;
                }

                if (allowTies)
                {
                    foreach (var pairing in newRound.Pairings)
                    {
                        foreach (var teamScore in pairing.TeamScores)
                        {
                            teamScore.Score = new HighestPointsScore(r.Next(20));
                        }
                    }
                }
                else
                {
                    foreach (var pairing in newRound.Pairings)
                    {
                        List<double> scoresUsed = new List<double>();
                        foreach (var teamScore in pairing.TeamScores)
                        {
                            double score;
                            do
                            {
                                score = r.NextDouble();
                            } while (scoresUsed.Where(s => s == score).Any());

                            teamScore.Score = new HighestPointsScore(score);
                        }
                    }
                }

                rounds.Add(newRound);
            }
        }

        private IEnumerable<TournamentTeam> CreateTeams(int teamCount)
        {
            for (int i = 1; i <= teamCount; i++)
            {
                yield return new TournamentTeam(i, 1000);
            }

            yield break;
        }

        private IEnumerable<TournamentRound> RounRobinBuildAllPairings(IEnumerable<TournamentTeam> teams, IPairingsGenerator rrpg)
        {
            List<TournamentRound> rounds = new List<TournamentRound>();

            while (true)
            {
                rrpg.LoadState(teams, rounds);

                TournamentRound newRound = rrpg.CreateNextRound(null);

                if (newRound != null)
                {
                    rounds.Add(newRound);
                }
                else
                {
                    break;
                }
            }

            foreach (TournamentRound round in rounds)
            {
                yield return round;
            }

            yield break;
        }

        private void DisplayTournamentRound(TournamentRound round)
        {
            if (round == null)
            {
                return;
            }

            foreach (TournamentPairing pairing in round.Pairings)
            {
                Console.Write(" ");
                foreach (TournamentTeamScore teamScore in pairing.TeamScores)
                {
                    Console.Write(" " + teamScore.Team.TeamId.ToString().PadLeft(2));
                }
                Console.Write("\n");
            }
            Console.WriteLine();
        }

        private void DisplayTournamentRounds(IEnumerable<TournamentRound> rounds)
        {
            if (rounds == null)
            {
                return;
            }

            int i = 1;

            foreach (TournamentRound round in rounds)
            {
                if (round == null)
                    continue;

                Console.WriteLine("Round " + i);
                foreach (TournamentPairing pairing in round.Pairings)
                {
                    Console.Write(" ");
                    foreach (TournamentTeamScore teamScore in pairing.TeamScores)
                    {
                        Console.Write(" " + teamScore.Team.TeamId.ToString().PadLeft(2));
                    }
                    Console.Write("\n");
                }
                Console.WriteLine();
                i++;
            }
        }

        private void DisplayTournamentRankings(IEnumerable<TournamentRanking> rankings)
        {
            if (rankings == null)
            {
                return;
            }

            foreach (TournamentRanking ranking in rankings)
            {
                Console.WriteLine("Rank " + ranking.Rank.ToString().PadRight(2) + "  Team " + ranking.Team.TeamId.ToString().PadRight(2) + "  Reason: " + ranking.ScoreDescription);
            }
            Console.WriteLine();
        }
    }
}
