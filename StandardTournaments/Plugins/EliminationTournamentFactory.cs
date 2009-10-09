using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tournaments.Plugins;

namespace Tournaments.Standard.Plugins
{
    public class EliminationTournamentFactory : IPairingsGeneratorFactory, ITournamentVisualizerFactory
    {
        public string Name
        {
            get
            {
                return EliminationTournament.GetTupleName(eliminations) + "-elimination";
            }
        }

        int eliminations;

        public EliminationTournamentFactory(int eliminations)
        {
            if (eliminations <= 0)
            {
                throw new ArgumentOutOfRangeException("eliminations");
            }

            this.eliminations = eliminations;
        }

        private EliminationTournament Create()
        {
            return new EliminationTournament(eliminations);
        }

        IPairingsGenerator IPairingsGeneratorFactory.Create()
        {
            return this.Create();
        }

        ITournamentVisualizer ITournamentVisualizerFactory.Create()
        {
            return this.Create();
        }
    }
}
