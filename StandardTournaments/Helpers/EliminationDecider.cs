using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tournaments.Standard
{
    public abstract class EliminationDecider
    {
        public abstract bool IsDecidable { get; }
        public abstract TournamentTeam GetWinner();
        public abstract TournamentTeam GetLoser();
    }
}
