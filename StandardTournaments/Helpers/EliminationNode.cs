using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tournaments.Standard
{
    public abstract class EliminationNode
    {
        protected EliminationDecider decider = null;
        
        public EliminationNode(EliminationDecider decider)
        {
            if (decider == null)
            {
                throw new ArgumentNullException("decider");
            }

            this.decider = decider;
        }

        public bool IsDecided
        {
            get
            {
                return this.decider.IsDecidable;
            }
        }
        public abstract TournamentTeam Team { get; }
        public Score Score { get; set; }
        
    }
}