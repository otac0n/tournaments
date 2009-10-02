using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tournaments.Standard
{
    public class EliminationNode
    {
        private EliminationNode parent;
        private EliminationNode childA;
        private EliminationNode childB;
        private EliminationNode loserNode;
        private TeamRanking team;
        private bool locked;
        private Score score;
        private int eliminationLevel;

        public EliminationNode(TeamRanking team, int eliminationLevel)
        {
            this.team = team;
            this.eliminationLevel = eliminationLevel;
        }

        public EliminationNode(int eliminationLevel)
        {
            this.eliminationLevel = eliminationLevel;
        }

        public bool Locked
        {
            get
            {
                return this.locked;
            }

            set
            {
                this.locked = value;
            }
        }

        public Score Score
        {
            get
            {
                return this.score;
            }

            set
            {
                this.score = value;
            }
        }

        public int EliminationLevel
        {
            get
            {
                return this.eliminationLevel;
            }
        }

        public EliminationNode LoserNode
        {
            get
            {
                return this.loserNode;
            }
            set
            {
                this.loserNode = value;
            }
        }

        public EliminationNode Parent
        {
            get
            {
                return this.parent;
            }

            private set
            {
                if (this.parent != null)
                {
                    if (this.parent.childA == this)
                    {
                        this.parent.childA = null;
                    }
                    else if (this.parent.childB == this)
                    {
                        this.parent.childB = null;
                    }
                }

                this.parent = value;
            }
        }

        public TeamRanking Team
        {
            get
            {
                if (this.team != null)
                {
                    return this.team;
                }
                else if (this.locked)
                {
                    if (this.childA == null && this.childB == null)
                    {
                        return null;
                    }
                    else if (this.childA != null && this.childB == null)
                    {
                        return this.childA.Team;
                    }
                    else if (this.childA == null && this.childB != null)
                    {
                        return this.childB.Team;
                    }
                    else
                    {
                        if (this.childA.Score != null && this.childB.Score != null)
                        {
                            if (this.childA.Score > this.childB.Score)
                            {
                                return this.childA.Team;
                            }
                            else if (this.childA.Score < this.childB.Score)
                            {
                                return this.childB.Team;
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public int Level
        {
            get
            {
                if (this.parent != null)
                {
                    return this.parent.Level + 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public EliminationNode ChildA
        {
            get
            {
                return this.childA;
            }

            set
            {
                if (this.team != null)
                {
                    throw new InvalidOperationException("You cannot assign children to a node with a team");
                }

                if (this.childA != value)
                {
                    if (this.childA != null)
                    {
                        this.childA.parent = null;
                    }

                    if (value != null)
                    {
                        if (value.parent != null)
                        {
                            if (value.parent.childA == value)
                            {
                                value.parent.childA = null;
                            }
                            else if (value.parent.childB == value)
                            {
                                value.parent.childB = null;
                            }
                        }
                        value.parent = this;
                    }

                    this.childA = value;
                }
            }
        }

        public EliminationNode ChildB
        {
            get
            {
                return this.childB;
            }

            set
            {
                if (this.team != null)
                {
                    throw new InvalidOperationException("You cannot assign children to a node with a team");
                }

                if (this.childB != value)
                {
                    if (this.childB != null)
                    {
                        this.childB.Parent = null;
                    }

                    if (value != null)
                    {
                        if (value.parent != null)
                        {
                            if (value.parent.childA == value)
                            {
                                value.parent.childA = null;
                            }
                            else if (value.parent.childB == value)
                            {
                                value.parent.childB = null;
                            }
                        }

                        value.parent = this;
                    }

                    this.childB = value;
                }
            }
        }

        public bool ChildAMatches(long teamId)
        {
            return this.childA != null && this.childA.Team != null && this.childA.Team.Team.TeamId == teamId;
        }

        public bool ChildBMatches(long teamId)
        {
            return this.childB != null && this.childB.Team != null && this.childB.Team.Team.TeamId == teamId;
        }

        public bool ChildAHasScore
        {
            get
            {
                return this.childA != null && this.childA.score != null;
            }
        }

        public bool ChildBHasScore
        {
            get
            {
                return this.childB != null && this.childB.score != null;
            }
        }

        public bool HasWinner
        {
            get
            {
                return this.ChildAHasScore && this.ChildBHasScore && this.childA.Score != this.childB.Score;
            }
        }

        public void MakeSiblingA(EliminationNode siblingA)
        {
            if (siblingA != null && siblingA.eliminationLevel != this.eliminationLevel)
            {
                throw new InvalidOperationException();
            }

            EliminationNode newParent = new EliminationNode(this.eliminationLevel);
            newParent.ChildA = siblingA;

            if (this.parent != null)
            {
                if (this.parent.childA == this)
                {
                    this.parent.ChildA = newParent;
                }
                else if (this.parent.childB == this)
                {
                    this.parent.ChildB = newParent;
                }
            }

            newParent.ChildB = this;
        }

        public void MakeSiblingB(EliminationNode siblingB)
        {
            if (siblingB != null && siblingB.eliminationLevel != this.eliminationLevel)
            {
                throw new InvalidOperationException();
            }

            EliminationNode newParent = new EliminationNode(this.eliminationLevel);
            newParent.ChildB = siblingB;

            if (this.parent != null)
            {
                if (this.parent.childA == this)
                {
                    this.parent.ChildA = newParent;
                }
                else if (this.parent.childB == this)
                {
                    this.parent.ChildB = newParent;
                }
            }

            newParent.ChildA = this;
        }

        public void SwapChildren()
        {
            EliminationNode temp = this.childA;
            this.childA = this.childB;
            this.childB = temp;
        }
    }

}
