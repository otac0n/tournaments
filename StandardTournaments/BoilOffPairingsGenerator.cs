//-----------------------------------------------------------------------
// <copyright file="BoilOffPairingsGenerator.cs" company="(none)">
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

namespace Tournaments.Standard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class BoilOffPairingsGenerator : IPairingsGenerator
    {
        private class BORank
        {
            public TournamentTeam Team { get; set; }
            public int Rank { get; set; }
            public int RoundNumber { get; set; }
            public Score Score { get; set; }
        }

        PairingsGeneratorState state = PairingsGeneratorState.NotInitialized;

        public string Name
        {
            get
            {
                return "Boil-off";
            }
        }

        public PairingsGeneratorState State
        {
            get
            {
                return this.state;
            }
        }

        public bool SupportsLateEntry
        {
            get
            {
                return false;
            }
        }

        List<TournamentTeam> loadedTeams;
        List<TournamentRound> loadedRounds;
        List<BORank> loadedEliminated;
        bool loadedCanContinue;

        public void Reset()
        {
            this.loadedTeams = null;
            this.loadedRounds = null;
            this.loadedEliminated = null;
            this.loadedCanContinue = false;
            this.state = PairingsGeneratorState.NotInitialized;
        }

        private const int lastRound = 3;

        public void LoadState(IEnumerable<TournamentTeam> teams, IList<TournamentRound> rounds)
        {
            if (teams == null)
            {
                throw new ArgumentNullException("teams");
            }

            if (rounds == null)
            {
                throw new ArgumentNullException("rounds");
            }

            if (!(teams.Count() < 1073741824))
            {
                throw new InvalidTournamentStateException("A boil off competition can not handle more than 1073741823 competitors.");
            }

            List<TournamentTeam> newTeams = new List<TournamentTeam>(teams);
            List<TournamentRound> newRounds = new List<TournamentRound>(rounds);
            List<BORank> newEliminated = new List<BORank>();

            int ordinalRoundNumber = 1;
            bool canContinue = true;
            foreach (var round in newRounds)
            {
                if (!canContinue)
                {
                    throw new InvalidTournamentStateException("The rounds alread executed in this tournament make it invalid as a boil-off tournament for the following reason:  At least one round was not completed before another was started.");
                }

                List<BORank> rankings = new List<BORank>(GetRoundRankings(round, ordinalRoundNumber));

                if (rankings.Count == 0)
                {
                    canContinue = false;
                    continue;
                }

                var rankingTeams = (from ranking in rankings
                                    select ranking.Team).ToList();

                var ineligible = from rt in rankingTeams
                                 where !newTeams.Contains(rt)
                                 select rt;

                if (ineligible.Any())
                {
                    throw new InvalidTournamentStateException("The rounds alread executed in this tournament make it invalid as a boil-off tournament for the following reason:  There is at least one competitor who should not have competed in a round who did.");
                }

                var required = (from nt in newTeams
                                where !rankingTeams.Contains(nt)
                                select nt).ToList();

                if (required.Any())
                {
                    // If not in the first round, allow additional eliminations. (Given that all players in a tie must be eliminated/kept together.)
                    if (ordinalRoundNumber > 1)
                    {
                        //
                    }

                    throw new InvalidTournamentStateException("The rounds alread executed in this tournament make it invalid as a boil-off tournament for the following reason:  There is at least one competitor who should have competed in a round who did not.");
                }

                int roundNumber = GetRoundNumber(newTeams.Count, BoilOffPairingsGenerator.lastRound);
                newTeams.Clear();

                if (roundNumber > 0)
                {
                    int maxRank = TeamsInRound(roundNumber - 1, BoilOffPairingsGenerator.lastRound);

                    var teamsEliminated = from ranking in rankings
                                          where ranking.Rank > maxRank
                                          select ranking;

                    var teamsLeft = from ranking in rankings
                                    where ranking.Rank <= maxRank
                                    select ranking.Team;

                    newEliminated.AddRange(teamsEliminated);
                    newTeams.AddRange(teamsLeft);
                }

                ordinalRoundNumber++;
            }


            this.loadedTeams = newTeams;
            this.loadedRounds = newRounds;
            this.loadedEliminated = newEliminated;
            this.loadedCanContinue = canContinue;
            this.state = PairingsGeneratorState.Initialized;
        }

        public TournamentRound CreateNextRound(int? places)
        {
            if (this.loadedTeams == null)
            {
                throw new InvalidTournamentStateException("This generator was never successfully initialized with a valid tournament state.");
            }

            if (this.loadedTeams.Count == 0)
            {
                return null;
            }

            if (!this.loadedCanContinue)
            {
                throw new InvalidTournamentStateException("This generator is not in a state that would allow a new round to be generated.");
            }

            IList<TournamentTeamScore> teamScores = GetNextRoundByTeams(this.loadedTeams);

            return new TournamentRound(new TournamentPairing[] { new TournamentPairing(teamScores) });
        }

        private static IList<TournamentTeamScore> GetNextRoundByTeams(IEnumerable<TournamentTeam> teams)
        {
            List<TournamentTeamScore> teamScores = new List<TournamentTeamScore>();
            foreach (var team in teams)
            {
                teamScores.Add(new TournamentTeamScore(team, null));
            }

            return teamScores;
        }

        /// <summary>
        /// The effective round number of the current number of teams.
        /// </summary>
        /// <param name="teamCount">The count of teams left.</param>
        /// <param name="finalCount">The desired number of competitors in the final round.</param>
        /// <returns>The effective round number.  This may not represent the actual round number, because two or more teams may have tied for the last non-eliminated spot.</returns>
        private static int GetRoundNumber(int teamCount, int finalCount)
        {
            // If the number of teams is already smaller than or equal to the desired number of competitors, return zero, indicating no rounds left.
            if (teamCount <= finalCount)
            {
                return 0;
            }

            int previousCount = finalCount;
            for (int round = 1; ; round++)
            {
                int newCount = previousCount * 2;

                if(newCount >= teamCount)
                {
                    return round;
                }

                previousCount = newCount;
            }
        }

        private static int TeamsInRound(int roundNumber, int finalCount)
        {
            return finalCount * (1 << roundNumber);
        }

        private IEnumerable<BORank> GetRoundRankings(TournamentRound round, int roundNumber)
        {
            if (round.Pairings.Count != 1)
            {
                throw new InvalidTournamentStateException("At least one round has more than one pairing set.");
            }

            TournamentPairing pairing = round.Pairings[0];

            foreach (var teamScore in pairing.TeamScores)
            {
                if (teamScore.Score == null)
                {
                    yield break;
                }
            }

            int r = 1, lastRank = 1;
            Score lastScore = null;

            var ranks = from team in pairing.TeamScores
                        orderby team.Score descending
                        select new BORank() { Team = team.Team, Rank = r++, Score = team.Score, RoundNumber = roundNumber };

            foreach (var rank in ranks)
            {
                if (lastScore == rank.Score)
                {
                    rank.Rank = lastRank;
                }

                lastScore = rank.Score;
                lastRank = rank.Rank;

                yield return rank;
            }
        }

        public IEnumerable<TournamentRanking> GenerateRankings()
        {
            if (this.loadedRounds.Count == 0)
            {
                throw new InvalidTournamentStateException("There have not been any rounds executed.");
            }

            var lastRoundRankings = (from ranking in GetRoundRankings(this.loadedRounds[this.loadedRounds.Count - 1], 0)
                                     select ranking).ToList();

            if (this.loadedTeams.Count != 0)
            {
                var continuing = from t in this.loadedTeams
                                 let ranking = lastRoundRankings.Where(r => r.Team == t).SingleOrDefault()
                                 orderby ranking == null ? null : ranking.Score descending
                                 select t;

                int i = 1;
                foreach (var team in continuing)
                {
                    yield return new TournamentRanking(team, i, "Continuing");
                    i++;
                }
            }
            else
            {
                foreach (var ranking in lastRoundRankings)
                {
                    yield return new TournamentRanking(ranking.Team, ranking.Rank, string.Format("Score: {0}", ranking.Score));
                }
            }

            var r2 = from ranking in this.loadedEliminated
                     orderby ranking.Rank
                     select ranking;

            foreach (var ranking in r2)
            {
                yield return new TournamentRanking(ranking.Team, ranking.Rank, string.Format("Eliminated in round {0}. Score: {1}", ranking.RoundNumber, ranking.Score));
            }

            yield break;
        }
    }
}
