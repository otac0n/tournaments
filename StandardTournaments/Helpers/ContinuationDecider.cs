using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tournaments.Standard
{
    public class ContinuationDecider : EliminationDecider
    {
        private EliminationNode nodeA = null;
        private EliminationNode nodeB = null;

        public ContinuationDecider(EliminationNode nodeA, EliminationNode nodeB)
        {
            if (nodeA == null)
            {
                throw new ArgumentNullException("nodeA");
            }

            if (nodeB == null)
            {
                throw new ArgumentNullException("nodeB");
            }

            this.nodeA = nodeA;
            this.nodeB = nodeB;
        }

        public override bool IsDecidable
        {
            get { return this.nodeA.IsDecided && nodeB.IsDecided && nodeA.Score != null && nodeB.Score != null && nodeA.Score != nodeB.Score; }
        }

        public override TournamentTeam GetWinner()
        {
            if (!this.nodeA.IsDecided)
            {
                throw new InvalidOperationException("Cannot determine a winner from a node that is undecided.");
            }

            if (!this.nodeB.IsDecided)
            {
                throw new InvalidOperationException("Cannot determine a winner from a node that is undecided.");
            }

            if (this.nodeA.Score == null)
            {
                throw new InvalidOperationException("Cannot determine a winner from a node without a score.");
            }

            if (this.nodeB.Score == null)
            {
                throw new InvalidOperationException("Cannot determine a winner from a node without a score.");
            }

            if (this.nodeA.Score == this.nodeB.Score)
            {
                throw new InvalidOperationException("Cannot determine a winner from between two nodes with the same score.");
            }

            return this.nodeA.Score > this.nodeB.Score ? this.nodeA.Team : this.nodeB.Team;
        }

        public override TournamentTeam GetLoser()
        {
            if (!this.nodeA.IsDecided)
            {
                throw new InvalidOperationException("Cannot determine a loser from a node that is undecided.");
            }

            if (!this.nodeB.IsDecided)
            {
                throw new InvalidOperationException("Cannot determine a loser from a node that is undecided.");
            }

            if (this.nodeA.Score == null)
            {
                throw new InvalidOperationException("Cannot determine a loser from a node without a score.");
            }

            if (this.nodeB.Score == null)
            {
                throw new InvalidOperationException("Cannot determine a loser from a node without a score.");
            }

            if (this.nodeA.Score == this.nodeB.Score)
            {
                throw new InvalidOperationException("Cannot determine a loser from between two nodes with the same score.");
            }

            return this.nodeA.Score > this.nodeB.Score ? this.nodeB.Team : this.nodeA.Team;
        }
    }
}
