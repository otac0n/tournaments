using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tournaments.Standard
{
    public class WinnerNode : EliminationNode
    {
        public override TournamentTeam Team
        {
            get { return this.decider.GetWinner(); }
        }
    }
}
