using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tournaments.Standard.Helpers
{
    public class BracketDecider : EliminationDecider
    {
        private List<EliminationNode> bracketRootNodes = new List<EliminationNode>();

        public BracketDecider(IEnumerable<EliminationNode> bracketRootNodes)
        {
           this.bracketRootNodes.AddRange(bracketRootNodes);
        }

        public BracketDecider(params EliminationNode[] bracketRootNodes)
        {
            this.bracketRootNodes.AddRange(bracketRootNodes);
        }

        public override bool IsDecided
        {
            get
            {
                return false;
            }
        }

        public override TournamentTeam GetWinner()
        {
            throw new NotImplementedException();
        }

        public override TournamentTeam GetLoser()
        {
            throw new NotImplementedException();
        }

        public override bool ApplyPairing(TournamentPairing pairing)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<TournamentPairing> FindUndecided()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<EliminationNode> FindNodes(Func<EliminationNode, bool> filter)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<EliminationDecider> FindDeciders(Func<EliminationDecider, bool> filter)
        {
            throw new NotImplementedException();
        }

        public override NodeMeasurement MeasureWinner(Tournaments.Graphics.IGraphics g, TournamentNameTable names, float textHeight, Score score)
        {
            throw new NotImplementedException();
        }

        public override NodeMeasurement MeasureLoser(Tournaments.Graphics.IGraphics g, TournamentNameTable names, float textHeight, Score score)
        {
            throw new NotImplementedException();
        }

        public override void RenderWinner(Tournaments.Graphics.IGraphics g, TournamentNameTable names, float x, float y, float textHeight, Score score)
        {
            throw new NotImplementedException();
        }

        public override void RenderLoser(Tournaments.Graphics.IGraphics g, TournamentNameTable names, float x, float y, float textHeight, Score score)
        {
            throw new NotImplementedException();
        }
    }
}
