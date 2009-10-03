//-----------------------------------------------------------------------
// <copyright file="SingleEliminationTournament.cs" company="(none)">
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
    using System.Drawing;
    using System.Linq;
    using System.Xml;
    using System.Globalization;
    using Tournaments.Graphics;

    /// <summary>
    /// Implements a Singe Elmination Tournament
    /// </summary>
    public class SingleEliminationTournament : IPairingsGenerator, ITournamentVisualizer
    {
        private List<TournamentTeam> loadedTeams;
        private List<EliminationNode> loadedNodes;
        private PairingsGeneratorState state = PairingsGeneratorState.NotInitialized;

        public string Name
        {
            get
            {
                return "Single-elimination";
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

        public void Reset()
        {
            this.loadedTeams = null;
            this.loadedNodes = null;
            this.state = PairingsGeneratorState.NotInitialized;
        }

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

            List<EliminationNode> nodes = new List<EliminationNode>();
            Dictionary<EliminationNode, EliminationDecider> parents = new Dictionary<EliminationNode, EliminationDecider>();

            if (teams.Count() >= 2)
            {
                int ranking = 0;
                var teamsOrder = from team in teams
                                 orderby team.Rating.HasValue ? team.Rating : 0 descending
                                 select new TeamRanking
                                 {
                                     Team = team,
                                     Ranking = ranking++,
                                 };

                int i = 0;
                int nextRoundAt = 2;
                int roundNumber = 0;
                int mask = (1 << roundNumber) - 1;
                var teamRankings = teamsOrder.ToList();
                foreach (var teamRanking in teamRankings)
                {
                    if (i == nextRoundAt)
                    {
                        nextRoundAt *= 2;
                        roundNumber += 1;
                        mask = (1 << roundNumber) - 1;
                    }

                    EliminationNode newNode = new WinnerNode(new TeamDecider(teamRanking.Team));
                    newNode.Locked = true;

                    if (nodes.Count == 0)
                    {
                        nodes.Add(newNode);
                        parents.Add(newNode, null);
                        newNode.Level = 0;
                        i++;
                        continue;
                    }

                    var match = (from n in nodes
                                 let d = n.Decider as TeamDecider
                                 where d != null
                                 where (teamRankings.Where(tr => tr.Team == n.Team).Single().Ranking & mask) == (teamRanking.Ranking & mask)
                                 select n).Single();

                    var oldParent = parents.ContainsKey(match) ? parents[match] as ContinuationDecider : null;
                    var newParent = MakeSiblings(match, newNode);
                    nodes.Add(newNode);
                    nodes.Add(newParent);
                    if (oldParent != null)
                    {
                        if (oldParent.ChildA == match)
                        {
                            oldParent.ChildA = newParent;
                            parents[newParent] = oldParent;
                        }
                        else if (oldParent.ChildB == match)
                        {
                            oldParent.ChildB = newParent;
                            parents[newParent] = oldParent;
                        }
                    }
                    parents[match] = newParent.Decider;
                    parents[newNode] = newParent.Decider;

                    i++;
                }

                // Add in byes to even out the left side of the bracket.
                for (ranking = teamRankings.Count; ranking < nextRoundAt; ranking++)
                {
                    EliminationNode newNode = new WinnerNode(new ByeDecider());
                    newNode.Locked = true;

                    var match = (from n in nodes
                                 let d = n.Decider as TeamDecider
                                 where d != null
                                 where (teamRankings.Where(tr => tr.Team == n.Team).Single().Ranking & mask) == (ranking & mask)
                                 select n).Single();

                    var oldParent = parents.ContainsKey(match) ? parents[match] as ContinuationDecider : null;
                    var newParent = MakeSiblings(match, newNode);
                    nodes.Add(newNode);
                    nodes.Add(newParent);
                    if (oldParent != null)
                    {
                        if (oldParent.ChildA == match)
                        {
                            oldParent.ChildA = newParent;
                            parents[newParent] = oldParent;
                        }
                        else if (oldParent.ChildB == match)
                        {
                            oldParent.ChildB = newParent;
                            parents[newParent] = oldParent;
                        }
                    }
                    parents[match] = newParent.Decider;
                    parents[newNode] = newParent.Decider;
                }
            }

            bool byePaired = false;
            bool byesLocked = false;

            foreach (var round in rounds)
            {
                foreach (var pairing in round.Pairings)
                {
                    if (pairing.TeamScores.Count > 2)
                    {
                        throw new InvalidTournamentStateException("At least one pairing had more than two teams competing.  This is invalid in a single elimination tournament.");
                    }

                    if (pairing.TeamScores.Count == 0)
                    {
                        continue;
                    }

                    if (pairing.TeamScores.Count == 1)
                    {
                        byePaired = true;

                        var team = pairing.TeamScores[0].Team;

                        var byes = from n in nodes
                                   let d = n.Decider as ContinuationDecider
                                   where d != null
                                   where d.ChildA.Team != null
                                   where d.ChildB.Team == null
                                   select new { Node = n, Decider = d };

                        var avail = from b in byes
                                    where b.Node.Locked == false
                                    select b;

                        var matched = from a in avail
                                      where ChildAMatches(a.Decider, team.TeamId)
                                      select a;

                        if (matched.Count() == 1)
                        {
                            var node = matched.Single();
                            node.Node.Locked = true;
                        }
                        else
                        {
                            // We did not find a matching bye, so we have to create one by swapping.
                            if (avail.Count() == 0)
                            {
                                throw new InvalidTournamentStateException("A bye was listed but no valid bye could be created.");
                            }

                            var byeNode = avail.First();

                            // Find our team in an unlocked node
                            var foundA = from n in nodes
                                         let d = n.Decider as ContinuationDecider
                                         where d != null
                                         where n.Locked == false
                                         where ChildAMatches(d, team.TeamId)
                                         select new { Node = n, Decider = d };

                            var foundB = from n in nodes
                                         let d = n.Decider as ContinuationDecider
                                         where d != null
                                         where n.Locked == false
                                         where ChildBMatches(d, team.TeamId)
                                         select new { Node = n, Decider = d };

                            // swap out the found node for our bye node.
                            if (foundA.Count() == 1)
                            {
                                var swapNode = foundA.Single();
                                var a = swapNode.Decider.ChildA;
                                swapNode.Decider.ChildA = byeNode.Decider.ChildA;
                                byeNode.Decider.ChildA = a;
                            }
                            else if (foundB.Count() == 1)
                            {
                                var swapNode = foundB.Single();
                                var b = swapNode.Decider.ChildB;
                                swapNode.Decider.ChildB = byeNode.Decider.ChildA;
                                byeNode.Decider.ChildA = b;
                            }
                            else
                            {
                                throw new InvalidTournamentStateException("A bye was listed for a team that was ineligible for a bye.");
                            }

                            byeNode.Node.Locked = true;
                        }
                    }
                    else
                    {
                        var scoreA = pairing.TeamScores[0].Score;
                        var teamA = pairing.TeamScores[0].Team;
                        var scoreB = pairing.TeamScores[1].Score;
                        var teamB = pairing.TeamScores[1].Team;

                    tryagainwithbyespaired:
                        if (byePaired && !byesLocked)
                        {
                            LockByes(nodes);
                            byesLocked = true;
                        }

                        var pairs = from n in nodes
                                    let d = n.Decider as ContinuationDecider
                                    where d != null
                                    where d.ChildA != null
                                    where d.ChildB != null
                                    select new { Node = n, Decider = d };

                        var avail = from p in pairs
                                    where p.Node.Locked == false
                                    select p;

                        var matched = from a in avail
                                      where ChildAMatches(a.Decider, teamA.TeamId)
                                      where ChildBMatches(a.Decider, teamB.TeamId)
                                      select a;

                        if (matched.Count() == 1)
                        {
                            var node = matched.Single();
                            node.Decider.ChildA.Score = scoreA;
                            node.Decider.ChildB.Score = scoreB;
                            node.Node.Locked = true;
                        }
                        else
                        {
                            // We did not find a matching pair, so we need to create one by swapping.

                            var teamANodes = from n in nodes
                                             let d = n.Decider as ContinuationDecider
                                             where d != null
                                             where n.Locked == false
                                             where ChildAMatches(d, teamA.TeamId) || ChildBMatches(d, teamA.TeamId)
                                             select new { Node = n, Decider = d };

                            var teamBNodes = from n in nodes
                                             let d = n.Decider as ContinuationDecider
                                             where d != null
                                             where n.Locked == false
                                             where ChildAMatches(d, teamB.TeamId) || ChildBMatches(d, teamB.TeamId)
                                             select new { Node = n, Decider = d };

                            if (teamANodes.Count() != 1 || teamBNodes.Count() != 1)
                            {
                                throw new InvalidTournamentStateException("At least one pairing involved a team that was not eligible to play.");
                            }

                            var teamANode = teamANodes.Single();
                            var teamBNode = teamBNodes.Single();

                            if (teamANode.Node == teamBNode.Node)
                            {
                                // If the order was merely swapped, swap it back.
                                teamANode.Decider.SwapChildren();
                                teamANode.Decider.ChildA.Score = scoreA;
                                teamANode.Decider.ChildB.Score = scoreB;
                                teamANode.Node.Locked = true;
                            }
                            else
                            {
                                if (teamANode.Node.Level != teamBNode.Node.Level)
                                {
                                    if (byePaired == false)
                                    {
                                        byePaired = true;
                                        goto tryagainwithbyespaired;
                                    }

                                    throw new InvalidTournamentStateException("At least one pairing involved two teams that were not eligible to play against each other.");
                                }

                                // find the first available node in this level.
                                var available = from n in nodes
                                                let d = n.Decider as ContinuationDecider
                                                where d != null
                                                where n.Locked == false
                                                where n.Level == teamANode.Node.Level
                                                where d.ChildA != null
                                                where d.ChildB != null
                                                select new { Node = n, Decider = d };

                                if (available.Count() == 0)
                                {
                                    if (byePaired == false)
                                    {
                                        byePaired = true;
                                        goto tryagainwithbyespaired;
                                    }

                                    throw new InvalidTournamentStateException("At least one pairing contained two teams when the tournament was unable to support a pairing with two teams.");
                                }

                                var destination = available.First();

                                if (teamANode.Decider.ChildA != null && teamANode.Decider.ChildA.Team != null && teamANode.Decider.ChildA.Team.TeamId == teamA.TeamId)
                                {
                                    // swap destination A with teamANode A
                                    SwapChildrenAA(destination.Decider, teamANode.Decider);
                                }
                                else if (teamANode.Decider.ChildB != null && teamANode.Decider.ChildB.Team != null && teamANode.Decider.ChildB.Team.TeamId == teamA.TeamId)
                                {
                                    // swap destination A with teamANode B
                                    SwapChildrenAB(destination.Decider, teamANode.Decider);
                                }
                                else
                                {
                                    throw new InvalidOperationException();
                                }

                                // since destination could've matched teamBNode and we may have swapped it out, we need to refetch teamBNode
                                teamBNode = teamBNodes.Single();

                                if (teamBNode.Decider.ChildA != null && teamBNode.Decider.ChildA.Team != null && teamBNode.Decider.ChildA.Team.TeamId == teamB.TeamId)
                                {
                                    // swap destination B with teamBNode A
                                    SwapChildrenBA(destination.Decider, teamBNode.Decider);
                                }
                                else if (teamBNode.Decider.ChildB != null && teamBNode.Decider.ChildB.Team != null && teamBNode.Decider.ChildB.Team.TeamId == teamB.TeamId)
                                {
                                    // swap destination B with teamBNode B
                                    SwapChildrenBB(destination.Decider, teamBNode.Decider);
                                }
                                else
                                {
                                    throw new InvalidOperationException();
                                }

                                destination.Decider.ChildA.Score = scoreA;
                                destination.Decider.ChildB.Score = scoreB;
                                destination.Node.Locked = true;
                            }
                        }
                    }
                }
            }

            LockByes(nodes);

            this.loadedNodes = nodes;
            this.loadedTeams = new List<TournamentTeam>(teams);
            this.state = PairingsGeneratorState.Initialized;
        }

        private bool ChildAMatches(ContinuationDecider decider, long teamId)
        {
            return decider != null && decider.ChildA != null && decider.ChildA.IsDecided && decider.ChildA.Team != null && decider.ChildA.Team.TeamId == teamId;
        }

        private bool ChildBMatches(ContinuationDecider decider, long teamId)
        {
            return decider != null && decider.ChildB != null && decider.ChildB.IsDecided && decider.ChildB.Team != null && decider.ChildB.Team.TeamId == teamId;
        }

        private EliminationNode MakeSiblings(EliminationNode nodeA, EliminationNode nodeB)
        {
            var maxLevel = Math.Max(nodeA.Level, nodeB.Level);
            var newNode = new WinnerNode(new ContinuationDecider(nodeA, nodeB));
            newNode.Level = maxLevel;
            nodeA.Level = nodeB.Level = maxLevel + 1;
            return newNode;
        }

        private static void SwapChildrenAA(ContinuationDecider node1, ContinuationDecider node2)
        {
            var temp1 = node1.ChildA;
            node1.ChildA = null;
            var temp2 = node2.ChildA;
            node2.ChildA = null;

            node1.ChildA = temp2;
            node2.ChildA = temp1;
        }

        private static void SwapChildrenAB(ContinuationDecider node1, ContinuationDecider node2)
        {
            var temp1 = node1.ChildA;
            node1.ChildA = null;
            var temp2 = node2.ChildB;
            node2.ChildB = null;

            node1.ChildA = temp2;
            node2.ChildB = temp1;
        }

        private static void SwapChildrenBA(ContinuationDecider node1, ContinuationDecider node2)
        {
            var temp1 = node1.ChildB;
            node1.ChildB = null;
            var temp2 = node2.ChildA;
            node2.ChildA = null;

            node1.ChildB = temp2;
            node2.ChildA = temp1;
        }

        private static void SwapChildrenBB(ContinuationDecider node1, ContinuationDecider node2)
        {
            var temp1 = node1.ChildB;
            node1.ChildB = null;
            var temp2 = node2.ChildB;
            node2.ChildB = null;

            node1.ChildB = temp2;
            node2.ChildB = temp1;
        }

        private static void LockByes(List<EliminationNode> nodes)
        {
            var unlockedByes = from n in nodes
                               let d = n.Decider as ContinuationDecider
                               where d != null
                               where n.Locked == false
                               where d.ChildA != null
                               where d.ChildB == null
                               select new { Node = n, Decider = d };

            foreach (var u in unlockedByes)
            {
                u.Node.Locked = true;
            }
        }

        public TournamentRound CreateNextRound(int? places)
        {
            if (places.HasValue && places.Value < 0)
            {
                throw new ArgumentException("You must specify a number of places that is greater than zero.", "places");
            }

            if (this.state != PairingsGeneratorState.Initialized)
            {
                throw new InvalidTournamentStateException("This generator was never successfully initialized with a valid tournament state.");
            }

            var readyToPlay = from n in this.loadedNodes
                              let d = n.Decider as ContinuationDecider
                              where d != null
                              where n.Locked == false
                              where d.ChildA != null && d.ChildA.IsDecided && d.ChildA.Team != null
                              where d.ChildB != null && d.ChildB.IsDecided && d.ChildB.Team != null
                              orderby n.Level descending
                              select new TournamentPairing(
                                  new TournamentTeamScore(d.ChildA.Team, null),
                                  new TournamentTeamScore(d.ChildB.Team, null));

            if (readyToPlay.Count() == 0)
            {
                // if this is because the root is locked, return null
                // otherwise, return an error because there is either a tie or an unfinished round.

                var ties = from n in this.loadedNodes
                           let d = n.Decider as ContinuationDecider
                           where d != null
                           where n.Locked == true
                           where d.ChildA != null && d.ChildB != null
                           where d.ChildA.Score != null && d.ChildB.Score != null
                           where d.ChildA.Score == d.ChildB.Score
                           select n;

                if (ties.Count() > 0)
                {
                    throw new InvalidTournamentStateException("The tournament cannot continue because there is at least one pairing still resulting in a tie.  Ties are not allowed in single elimintaion tournaments.");
                }

                var unfinished = from n in this.loadedNodes
                                 let d = n.Decider as ContinuationDecider
                                 where d != null
                                 where n.Locked == true
                                 where d.ChildA != null && d.ChildB != null
                                 where d.ChildA.Score == null || d.ChildB.Score == null
                                 select n;

                if (unfinished.Count() > 0)
                {
                    throw new InvalidTournamentStateException("The tournament cannot continue because there is at least one pairing that remains unfinished.");
                }

                return null;
            }

            if (places.HasValue)
            {
                return new TournamentRound(readyToPlay.Take(places.Value).ToList());
            }
            else
            {
                return new TournamentRound(readyToPlay.ToList());
            }
        }

        public IEnumerable<TournamentRanking> GenerateRankings()
        {
            if (this.loadedTeams.Count >= 2)
            {
                var maxLevel = this.loadedNodes.Max(n => n.Level);

                var ranks = from t in this.loadedTeams
                            let node = FindTeamsHighestNode(t.TeamId)
                            let finished = node.Locked && node.IsDecided
                            let winner = node.IsDecided && node.Team != null && node.Team.TeamId == t.TeamId
                            let round = maxLevel - node.Level
                            let rank = node.Level + (winner ? 1 : 2)
                            orderby rank
                            select new TournamentRanking(t, rank, (finished ? (winner ? "Winner" : "Eliminated in heat " + round) : "Continuing"));

                return ranks;
            }
            else
            {
                throw new InvalidTournamentStateException("The tournament is not in a state that allows ranking.");
            }
        }

        private EliminationNode FindTeamsHighestNode(long teamId)
        {
            return (from n in this.loadedNodes
                    let d = n.Decider as ContinuationDecider
                    where d != null
                    where ChildAMatches(d, teamId) || ChildBMatches(d, teamId)
                    orderby n.Level
                    select n).FirstOrDefault();
        }

        public SizeF Measure(IGraphics graphics, TournamentNameTable teamNames)
        {
            var rootNode = (from n in this.loadedNodes
                            where n.Level == 0
                            select n).SingleOrDefault();

            if (rootNode == null)
            {
                return new Size(0, 0);
            }

            var textHeight = GetTextHeight(graphics);

            var size = rootNode.Measure(graphics, teamNames, textHeight);

            return new SizeF(size.Width + 10, size.Height + 10);
        }

        public void Render(IGraphics graphics, TournamentNameTable teamNames)
        {
            var rootNode = (from n in this.loadedNodes
                            where n.Level == 0
                            select n).SingleOrDefault();

            if (rootNode == null)
            {
                return;
            }

            var textHeight = GetTextHeight(graphics);

            rootNode.Render(graphics, teamNames, 5, 5, textHeight);
        }

        private const float TextYOffset = 3.0f;
        private Font UserboxFont = new Font(FontFamily.GenericSansSerif, 10.0f);
        private const float MinTextHeight = 20.0f;

        private float GetTextHeight(IGraphics g)
        {
            return Math.Max(g.MeasureString("abfgijlpqyAIJQ170,`'\"", UserboxFont).Height + TextYOffset * 2, MinTextHeight);
        }
    }
}
