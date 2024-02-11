using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Personality
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
            int seed = PersonalityHelper.PawnSeed(this.pawn);
            Random random = new Random(seed);
            foreach (PersonalityNode node in this.nodes)
            {
                float r = random.Next(-100, 100);
                node.BaseRating = r / 100f;
                Log.Message(node.ToString());
            }
        }
    }
}
