using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Psychology_Reborn
{
    public class PsycheTracker
    {
        private Pawn pawn;
        private HashSet<PersonalityNode> nodes;

        public HashSet<PersonalityNode> Nodes => nodes;

        public PsycheTracker(Pawn pawn)
        {
            this.pawn = pawn;
        }

        public void Initialize()
        {
            this.nodes = new HashSet<PersonalityNode>();
            foreach (PersonalityNodeDef def in DefDatabase<PersonalityNodeDef>.AllDefsListForReading)
            {
                this.nodes.Add(new PersonalityNode(this.pawn, def));
            }
            Log.Message("PERSONALITY NODES");
            foreach (PersonalityNode node in this.nodes)
            {
                Log.Message(node.ToString());
            }
        }
    }
}
