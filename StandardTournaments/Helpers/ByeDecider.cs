using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tournaments.Standard
{
    public class ByeDecider : EliminationDecider
    {
        public override bool IsDecidable
        {
            get { return true; }
        }

        public override TournamentTeam GetWinner()
        {
            return null;
        }

        public override TournamentTeam GetLoser()
        {
            throw new InvalidOperationException("Cannot determine a loser from a bye entry.");
        }
    }
}
