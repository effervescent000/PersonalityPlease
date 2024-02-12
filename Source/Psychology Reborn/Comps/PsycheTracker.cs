using System;
using System.Collections.Generic;
using Verse;

namespace Personality
{
    public class PsycheTracker
    {
        private readonly Pawn pawn;
        public Dictionary<string, PersonalityNode> nodes;

        public PsycheTracker(Pawn pawn)
        {
            this.pawn = pawn;
        }

        public void Initialize()
        {
            nodes = new Dictionary<string, PersonalityNode>();
            foreach (PersonalityNodeDef def in DefDatabase<PersonalityNodeDef>.AllDefsListForReading)
            {
                nodes.Add(def.defName, new PersonalityNode(pawn, def));
            }
            int seed = PersonalityHelper.PawnSeed(this.pawn);
            Random random = new(seed);
            foreach (PersonalityNode node in this.nodes.Values)
            {
                float r = random.Next(-100, 100);
                node.BaseRating = r / 100f;
                node.ModifyRating();
            }
        }
    }
}
