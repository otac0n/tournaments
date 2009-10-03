using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tournaments.Standard
{
    public class TeamDecider : EliminationDecider
    {
        private TournamentTeam team = null;
                
        public TeamDecider(TournamentTeam team)
        {
            if (team == null)
            {
                throw new ArgumentNullException("team");
            }

            this.team = team;
        }

        public override bool IsDecidable
        {
            get { return true; }
        }

        public override TournamentTeam GetWinner()
        {
            return this.team;
        }

        public override TournamentTeam GetLoser()
        {
            throw new InvalidOperationException("Cannot determine a loser from an individual team entry.");
        }
    }
}
