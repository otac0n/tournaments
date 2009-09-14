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

    /// <summary>
    /// Implements a Singe Elmination Tournament
    /// </summary>
    public class SingleEliminationTournament : IPairingsGenerator, ITournamentVisualizer
    {
        private List<TournamentTeam> loadedTeams;
        private List<SingleEliminationNode> loadedNodes;
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

            List<SingleEliminationNode> nodes = new List<SingleEliminationNode>();

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
                foreach (var team in teamsOrder)
                {
                    SingleEliminationNode newNode = new SingleEliminationNode(team);
                    newNode.Locked = true;

                    if (nodes.Count == 0)
                    {
                        nodes.Add(newNode);
                        i++;
                        continue;
                    }

                    var match = (from n in nodes
                                 where n.Team != null
                                 where (n.Team.Ranking & mask) == (team.Ranking & mask)
                                 select n).Single();

                    match.MakeSiblingB(newNode);
                    nodes.Add(newNode);
                    nodes.Add(match.Parent);

                    i++;

                    if (i == nextRoundAt)
                    {
                        nextRoundAt *= 2;
                        roundNumber += 1;
                        mask = (1 << roundNumber) - 1;
                    }
                }

                // Move all byes even with the left side of the bracket.
                var maxLevel = nodes.Max(n => n.Level);
                var byeNodes = (from n in nodes
                                where n.Team != null
                                where n.Level != maxLevel
                                select n).ToList();
                foreach (var node in byeNodes)
                {
                    node.MakeSiblingB(null);
                    nodes.Add(node.Parent);
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
                                   where n.ChildA != null
                                   where n.ChildB == null
                                   select n;

                        var avail = from b in byes
                                    where b.Locked == false
                                    select b;

                        var matched = from a in avail
                                      where a.ChildAMatches(team.TeamId)
                                      select a;

                        if (matched.Count() == 1)
                        {
                            var node = matched.Single();
                            node.Locked = true;
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
                                         where n.Locked == false
                                         where n.ChildAMatches(team.TeamId)
                                         select n;

                            var foundB = from n in nodes
                                         where n.Locked == false
                                         where n.ChildBMatches(team.TeamId)
                                         select n;

                            // swap out the found node for our bye node.
                            if (foundA.Count() == 1)
                            {
                                var swapNode = foundA.Single();
                                var a = swapNode.ChildA;
                                swapNode.ChildA = byeNode.ChildA;
                                byeNode.ChildA = a;
                            }
                            else if (foundB.Count() == 1)
                            {
                                var swapNode = foundB.Single();
                                var b = swapNode.ChildB;
                                swapNode.ChildB = byeNode.ChildA;
                                byeNode.ChildA = b;
                            }
                            else
                            {
                                throw new InvalidTournamentStateException("A bye was listed for a team that was ineligible for a bye.");
                            }

                            byeNode.Locked = true;
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
                                    where n.ChildA != null
                                    where n.ChildB != null
                                    select n;

                        var avail = from p in pairs
                                    where p.Locked == false
                                    select p;

                        var matched = from a in avail
                                      where a.ChildAMatches(teamA.TeamId)
                                      where a.ChildBMatches(teamB.TeamId)
                                      select a;

                        if (matched.Count() == 1)
                        {
                            var node = matched.Single();
                            node.ChildA.Score = scoreA;
                            node.ChildB.Score = scoreB;
                            node.Locked = true;
                        }
                        else
                        {
                            // We did not find a matching pair, so we need to create one by swapping.

                            var teamANodes = from n in nodes
                                             where n.Locked == false
                                             where n.ChildAMatches(teamA.TeamId) || n.ChildBMatches(teamA.TeamId)
                                             select n;

                            var teamBNodes = from n in nodes
                                             where n.Locked == false
                                             where n.ChildAMatches(teamB.TeamId) || n.ChildBMatches(teamB.TeamId)
                                             select n;

                            if (teamANodes.Count() != 1 || teamBNodes.Count() != 1)
                            {
                                throw new InvalidTournamentStateException("At least one pairing involved a team that was not eligible to play.");
                            }

                            var teamANode = teamANodes.Single();
                            var teamBNode = teamBNodes.Single();

                            if (teamANode == teamBNode)
                            {
                                // If the order was merely swapped, swap it back.
                                teamANode.SwapChildren();
                                teamANode.ChildA.Score = scoreA;
                                teamANode.ChildB.Score = scoreB;
                                teamANode.Locked = true;
                            }
                            else
                            {
                                if (teamANode.Level != teamBNode.Level)
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
                                                where n.Locked == false
                                                where n.Level == teamANode.Level
                                                where n.ChildA != null
                                                where n.ChildB != null
                                                select n;

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

                                if (teamANode.ChildA != null && teamANode.ChildA.Team != null && teamANode.ChildA.Team.Team.TeamId == teamA.TeamId)
                                {
                                    // swap destination A with teamANode A
                                    SwapChildrenAA(destination, teamANode);

                                }
                                else if (teamANode.ChildB != null && teamANode.ChildB.Team != null && teamANode.ChildB.Team.Team.TeamId == teamA.TeamId)
                                {
                                    // swap destination A with teamANode B
                                    SwapChildrenAB(destination, teamANode);
                                }
                                else
                                {
                                    throw new InvalidOperationException();
                                }

                                // since destination could've matched teamBNode and we may have swapped it out, we need to refetch teamBNode
                                teamBNode = teamBNodes.Single();

                                if (teamBNode.ChildA != null && teamBNode.ChildA.Team != null && teamBNode.ChildA.Team.Team.TeamId == teamB.TeamId)
                                {
                                    // swap destination B with teamBNode A
                                    SwapChildrenBA(destination, teamBNode);
                                }
                                else if (teamBNode.ChildB != null && teamBNode.ChildB.Team != null && teamBNode.ChildB.Team.Team.TeamId == teamB.TeamId)
                                {
                                    // swap destination B with teamBNode B
                                    SwapChildrenBB(destination, teamBNode);
                                }
                                else
                                {
                                    throw new InvalidOperationException();
                                }

                                destination.ChildA.Score = scoreA;
                                destination.ChildB.Score = scoreB;
                                destination.Locked = true;
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

        private static void SwapChildrenAA(SingleEliminationNode node1, SingleEliminationNode node2)
        {
            var temp1 = node1.ChildA;
            node1.ChildA = null;
            var temp2 = node2.ChildA;
            node2.ChildA = null;

            node1.ChildA = temp2;
            node2.ChildA = temp1;
        }

        private static void SwapChildrenAB(SingleEliminationNode node1, SingleEliminationNode node2)
        {
            var temp1 = node1.ChildA;
            node1.ChildA = null;
            var temp2 = node2.ChildB;
            node2.ChildB = null;

            node1.ChildA = temp2;
            node2.ChildB = temp1;
        }

        private static void SwapChildrenBA(SingleEliminationNode node1, SingleEliminationNode node2)
        {
            var temp1 = node1.ChildB;
            node1.ChildB = null;
            var temp2 = node2.ChildA;
            node2.ChildA = null;

            node1.ChildB = temp2;
            node2.ChildA = temp1;
        }

        private static void SwapChildrenBB(SingleEliminationNode node1, SingleEliminationNode node2)
        {
            var temp1 = node1.ChildB;
            node1.ChildB = null;
            var temp2 = node2.ChildB;
            node2.ChildB = null;

            node1.ChildB = temp2;
            node2.ChildB = temp1;
        }

        private static void LockByes(List<SingleEliminationNode> nodes)
        {
            var unlockedByes = from n in nodes
                               where n.Locked == false
                               where n.ChildA != null
                               where n.ChildB == null
                               select n;

            foreach (var u in unlockedByes)
            {
                u.Locked = true;
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
                              where n.Locked == false
                              where n.ChildA != null && n.ChildA.Team != null
                              where n.ChildB != null && n.ChildB.Team != null
                              orderby n.Level descending
                              select new TournamentPairing(
                                  new TournamentTeamScore(n.ChildA.Team.Team, null),
                                  new TournamentTeamScore(n.ChildB.Team.Team, null));

            if (readyToPlay.Count() == 0)
            {
                // if this is because the root is locked, return null
                // otherwise, return an error because there is either a tie or an unfinished round.

                var ties = from n in this.loadedNodes
                           where n.Locked == true
                           where n.ChildA != null && n.ChildB != null
                           where n.ChildA.Score != null && n.ChildB.Score != null
                           where n.ChildA.Score == n.ChildB.Score
                           select n;

                if (ties.Count() > 0)
                {
                    throw new InvalidTournamentStateException("The tournament cannot continue because there is at least one pairing still resulting in a tie.  Ties are not allowed in single elimintaion tournaments.");
                }

                var unfinished = from n in this.loadedNodes
                                 where n.Locked == true
                                 where n.ChildA != null && n.ChildB != null
                                 where n.ChildA.Score == null || n.ChildB.Score == null
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
                            let finished = node.Locked && node.HasWinner
                            let winner = node.Team != null && node.Team.Team.TeamId == t.TeamId
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

        private SingleEliminationNode FindTeamsHighestNode(long teamId)
        {
            return (from n in this.loadedNodes
                    where n.ChildAMatches(teamId) || n.ChildBMatches(teamId)
                    orderby n.Level
                    select n).FirstOrDefault();
        }

        public Size Measure(TournamentNameTable teamNames)
        {
            var rootNode = (from n in this.loadedNodes
                            where n.Parent == null
                            select n).SingleOrDefault();

            if (rootNode == null)
            {
                return new Size(0, 0);
            }

            var size = this.MeasureNode(rootNode);

            return new Size(size.Width + 10, size.Height + 10);
        }

        public XmlReader Render(TournamentNameTable teamNames)
        {
            XmlDocument doc = new XmlDocument();
            string xmlns = "http://www.w3.org/2000/svg";
            doc.LoadXml(@"<svg xmlns=""http://www.w3.org/2000/svg"" width=""100%"" height=""100%"" onload=""Initialize(evt)""></svg>");

            var rootNode = (from n in this.loadedNodes
                            where n.Parent == null
                            select n).SingleOrDefault();

            if (rootNode == null)
            {
                return null;
            }

            doc.DocumentElement.AppendChild(CreateScriptNode(doc, xmlns));

            IEnumerable<XmlNode> renderedNodes = this.RenderNode(rootNode, 5, 5, teamNames, doc, xmlns);

            foreach (XmlNode node in renderedNodes)
            {
                doc.DocumentElement.AppendChild(node);
            }

            return new XmlNodeReader(doc);
        }

        private static XmlNode CreateScriptNode(XmlDocument doc, string xmlns)
        {
            XmlElement script = doc.CreateElement("script", xmlns);
            XmlAttribute script_type = doc.CreateAttribute("type");
            script.Attributes.Append(script_type);

            script_type.Value = "text/ecmascript";

            XmlCDataSection script_data = doc.CreateCDataSection(@"
            SVGDocument = null;
            top.SetTournamentTextStyle = SetTournamentTextStyle;
            top.SetTournamentTextContrastStyle = SetTournamentTextContrastStyle;
            top.SetTournamentAccentStyle = SetTournamentAccentStyle;

            function Initialize(LoadEvent) {
              SVGDocument = LoadEvent.target.ownerDocument
            }

            function SetTournamentTextStyle(style) {
              setElementsAttribute('text', 'style', style);
            }

            function SetTournamentTextContrastStyle(style) {
              setElementsAttribute('rect', 'style', style);
            }

            function SetTournamentAccentStyle(style) {
              setElementsAttribute('line', 'style', style);
            }

            function setElementsAttribute(tag, attribute, value) {
              var elements = SVGDocument.getElementsByTagName(tag);
              for (var i = 0; i < elements.length; i++) {
                elements.item(i).setAttribute(attribute, value);
              }
            }
            ");

            script.AppendChild(script_data);

            return script;
        }

        private const int BracketWidth = 120;
        private const int BracketPreIndent = 10;
        private const int BracketPostIndent = 10;
        private const int BracketVSpacing = 25;
        private const string BracketStyle = "stroke:black;stroke-width:1px;";
        private const int TextHeight = 20;
        private const int TextYOffset = (TextHeight / 2) - 5;
        private const int TextXOffset = 5;
        private const string UserboxStyle = "fill:rgb(220,220,220);";

        private IEnumerable<XmlNode> RenderNode(SingleEliminationNode rootNode, int x, int y, TournamentNameTable teamNames, XmlDocument doc, string xmlns)
        {
            List<XmlNode> allNodes = new List<XmlNode>();

            var m = this.MeasureNode(rootNode);

            if (rootNode.ChildA != null && rootNode.ChildB != null)
            {
                XmlElement preline = doc.CreateElement("line", xmlns);
                XmlAttribute preline_x1 = doc.CreateAttribute("x1");
                XmlAttribute preline_y1 = doc.CreateAttribute("y1");
                XmlAttribute preline_x2 = doc.CreateAttribute("x2");
                XmlAttribute preline_y2 = doc.CreateAttribute("y2");
                XmlAttribute preline_style = doc.CreateAttribute("style");
                preline.Attributes.Append(preline_x1);
                preline.Attributes.Append(preline_y1);
                preline.Attributes.Append(preline_x2);
                preline.Attributes.Append(preline_y2);
                preline.Attributes.Append(preline_style);

                preline_x1.Value = (x + (m.Width - BracketWidth)).ToString(CultureInfo.InvariantCulture);
                preline_y1.Value = (y + m.CenterLine).ToString(CultureInfo.InvariantCulture);
                preline_x2.Value = (x + (m.Width - BracketWidth - BracketPreIndent)).ToString(CultureInfo.InvariantCulture);
                preline_y2.Value = (y + m.CenterLine).ToString(CultureInfo.InvariantCulture);
                preline_style.Value = BracketStyle;

                var renderedA = this.RenderNode(rootNode.ChildA, x, y, teamNames, doc, xmlns);
                var childASize = this.MeasureNode(rootNode.ChildA);
                var renderedB = this.RenderNode(rootNode.ChildB, x, y + childASize.Height + BracketVSpacing, teamNames, doc, xmlns);
                var childBSize = this.MeasureNode(rootNode.ChildB);

                XmlElement vline = doc.CreateElement("line", xmlns);
                XmlAttribute vline_x1 = doc.CreateAttribute("x1");
                XmlAttribute vline_y1 = doc.CreateAttribute("y1");
                XmlAttribute vline_x2 = doc.CreateAttribute("x2");
                XmlAttribute vline_y2 = doc.CreateAttribute("y2");
                XmlAttribute vline_style = doc.CreateAttribute("style");
                vline.Attributes.Append(vline_x1);
                vline.Attributes.Append(vline_y1);
                vline.Attributes.Append(vline_x2);
                vline.Attributes.Append(vline_y2);
                vline.Attributes.Append(vline_style);

                XmlElement postlineA = doc.CreateElement("line", xmlns);
                XmlAttribute postlineA_x1 = doc.CreateAttribute("x1");
                XmlAttribute postlineA_y1 = doc.CreateAttribute("y1");
                XmlAttribute postlineA_x2 = doc.CreateAttribute("x2");
                XmlAttribute postlineA_y2 = doc.CreateAttribute("y2");
                XmlAttribute postlineA_style = doc.CreateAttribute("style");
                postlineA.Attributes.Append(postlineA_x1);
                postlineA.Attributes.Append(postlineA_y1);
                postlineA.Attributes.Append(postlineA_x2);
                postlineA.Attributes.Append(postlineA_y2);
                postlineA.Attributes.Append(postlineA_style);

                XmlElement postlineB = doc.CreateElement("line", xmlns);
                XmlAttribute postlineB_x1 = doc.CreateAttribute("x1");
                XmlAttribute postlineB_y1 = doc.CreateAttribute("y1");
                XmlAttribute postlineB_x2 = doc.CreateAttribute("x2");
                XmlAttribute postlineB_y2 = doc.CreateAttribute("y2");
                XmlAttribute postlineB_style = doc.CreateAttribute("style");
                postlineB.Attributes.Append(postlineB_x1);
                postlineB.Attributes.Append(postlineB_y1);
                postlineB.Attributes.Append(postlineB_x2);
                postlineB.Attributes.Append(postlineB_y2);
                postlineB.Attributes.Append(postlineB_style);

                vline_x1.Value = (x + (m.Width - BracketWidth - BracketPreIndent)).ToString(CultureInfo.InvariantCulture);
                vline_y1.Value = (y + childASize.CenterLine).ToString(CultureInfo.InvariantCulture);
                vline_x2.Value = (x + (m.Width - BracketWidth - BracketPreIndent)).ToString(CultureInfo.InvariantCulture);
                vline_y2.Value = (y + childASize.Height + BracketVSpacing + childBSize.CenterLine).ToString(CultureInfo.InvariantCulture);
                vline_style.Value = BracketStyle;

                postlineA_x1.Value = (x + (m.Width - BracketWidth - BracketPreIndent - BracketPostIndent)).ToString(CultureInfo.InvariantCulture);
                postlineA_y1.Value = (y + childASize.CenterLine).ToString(CultureInfo.InvariantCulture);
                postlineA_x2.Value = (x + (m.Width - BracketWidth - BracketPreIndent)).ToString(CultureInfo.InvariantCulture);
                postlineA_y2.Value = (y + childASize.CenterLine).ToString(CultureInfo.InvariantCulture);
                postlineA_style.Value = BracketStyle;

                postlineB_x1.Value = (x + (m.Width - BracketWidth - BracketPreIndent - BracketPostIndent)).ToString(CultureInfo.InvariantCulture);
                postlineB_y1.Value = (y + childASize.Height + BracketVSpacing + childBSize.CenterLine).ToString(CultureInfo.InvariantCulture);
                postlineB_x2.Value = (x + (m.Width - BracketWidth - BracketPreIndent)).ToString(CultureInfo.InvariantCulture);
                postlineB_y2.Value = (y + childASize.Height + BracketVSpacing + childBSize.CenterLine).ToString(CultureInfo.InvariantCulture);
                postlineB_style.Value = BracketStyle;

                allNodes.Add(preline);
                allNodes.Add(vline);
                allNodes.Add(postlineA);
                allNodes.Add(postlineB);
                allNodes.AddRange(renderedA);
                allNodes.AddRange(renderedB);
            }

            XmlElement userbox = doc.CreateElement("rect", xmlns);
            XmlAttribute userbox_x = doc.CreateAttribute("x");
            XmlAttribute userbox_y = doc.CreateAttribute("y");
            XmlAttribute userbox_w = doc.CreateAttribute("width");
            XmlAttribute userbox_h = doc.CreateAttribute("height");
            XmlAttribute userbox_style = doc.CreateAttribute("style");
            userbox.Attributes.Append(userbox_x);
            userbox.Attributes.Append(userbox_y);
            userbox.Attributes.Append(userbox_w);
            userbox.Attributes.Append(userbox_h);
            userbox.Attributes.Append(userbox_style);

            userbox_x.Value = (x + (m.Width - BracketWidth)).ToString(CultureInfo.InvariantCulture);
            userbox_y.Value = (y + m.CenterLine - (TextHeight / 2)).ToString(CultureInfo.InvariantCulture);
            userbox_w.Value = BracketWidth.ToString(CultureInfo.InvariantCulture);
            userbox_h.Value = TextHeight.ToString(CultureInfo.InvariantCulture);
            userbox_style.Value = UserboxStyle;
            
            allNodes.Add(userbox);

            if (rootNode.Team != null)
            {
                XmlElement username = doc.CreateElement("text", xmlns);
                XmlAttribute username_x = doc.CreateAttribute("x");
                XmlAttribute username_y = doc.CreateAttribute("y");
                username.Attributes.Append(username_x);
                username.Attributes.Append(username_y);

                username_x.Value = (x + (m.Width - BracketWidth) + TextXOffset).ToString(CultureInfo.InvariantCulture);
                username_y.Value = (y + m.CenterLine + TextYOffset).ToString(CultureInfo.InvariantCulture);

                username.InnerText = teamNames[rootNode.Team.Team.TeamId];

                allNodes.Add(username);
            }

            if (rootNode.Score != null)
            {
                XmlElement score = doc.CreateElement("text", xmlns);
                XmlAttribute score_x = doc.CreateAttribute("x");
                XmlAttribute score_y = doc.CreateAttribute("y");
                XmlAttribute score_textanchor = doc.CreateAttribute("text-anchor");
                score.Attributes.Append(score_x);
                score.Attributes.Append(score_y);
                score.Attributes.Append(score_textanchor);

                score_x.Value = (x + m.Width - TextXOffset).ToString(CultureInfo.InvariantCulture);
                score_y.Value = (y + m.CenterLine + TextYOffset).ToString(CultureInfo.InvariantCulture);
                score_textanchor.Value = "end";

                score.InnerText = rootNode.Score.ToString();

                allNodes.Add(score);
            }

            return allNodes.AsReadOnly();
        }

        private NodeMeasurement MeasureNode(SingleEliminationNode rootNode)
        {
            int width = BracketWidth;
            int height = TextHeight;
            int center = TextHeight / 2;

            if (rootNode.ChildA != null && rootNode.ChildB != null)
            {
                var a = this.MeasureNode(rootNode.ChildA);
                var b = this.MeasureNode(rootNode.ChildB);
                height = Math.Max(a.Height + BracketVSpacing + b.Height, height);
                width = width + Math.Max(a.Width, b.Width) + BracketPreIndent + BracketPostIndent;
                center = (a.CenterLine + BracketVSpacing + b.CenterLine + a.Height) / 2;
            }
            else if (rootNode.ChildA != null)
            {
                var a = this.MeasureNode(rootNode.ChildA);
                width = width + a.Width + BracketPreIndent + BracketPostIndent;
                height = Math.Max(a.Height, height);
                center = a.CenterLine;
            }
            else if (rootNode.ChildB != null)
            {
                var b = this.MeasureNode(rootNode.ChildB);
                width = width + b.Width + BracketPreIndent + BracketPostIndent;
                height = Math.Max(b.Height, height);
                center = b.CenterLine;
            }

            return new NodeMeasurement
            {
                Height = height,
                Width = width,
                CenterLine = center
            };
        }

        private class NodeMeasurement
        {
            public int Width
            {
                get;
                set;
            }

            public int Height
            {
                get;
                set;
            }

            public int CenterLine
            {
                get;
                set;
            }
        }
    }
}
