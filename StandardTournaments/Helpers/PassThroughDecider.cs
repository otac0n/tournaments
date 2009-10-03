using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tournaments.Standard
{
    public class PassThroughDecider : EliminationDecider
    {
        private EliminationNode node = null;

        public PassThroughDecider(EliminationNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            this.node = node;
        }

        public override bool IsDecidable
        {
            get { return this.node.IsDecided; }
        }

        public override TournamentTeam GetWinner()
        {
            if (!this.node.IsDecided)
            {
                throw new InvalidOperationException("Cannot determine a winner from a node that is undecided.");
            }

            return this.node.GetTeam();
        }

        public override TournamentTeam GetLoser()
        {
            throw new InvalidOperationException("Cannot determine a loser from a pass through.");
        }
    }
}
