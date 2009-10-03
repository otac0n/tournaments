using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tournaments.Standard
{
    public class LoserNode : EliminationNode
    {
        public override TournamentTeam Team
        {
            get { return this.decider.GetLoser(); }
        }
    }
}
