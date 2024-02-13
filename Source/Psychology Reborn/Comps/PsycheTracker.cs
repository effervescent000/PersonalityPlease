using System;
using System.Collections.Generic;
using Verse;

namespace Personality
{
    public class PsycheTracker : IExposable
    {
        private readonly Pawn pawn;
        public Dictionary<string, PersonalityNode> nodes;

        public PsycheTracker(Pawn pawn)
        {
            this.pawn = pawn;
        }

        public void Initialize()
        {
            Initialize(pawn);
        }

        public void Initialize(Pawn pawn)
        {
            nodes = new Dictionary<string, PersonalityNode>();
            foreach (PersonalityNodeDef def in DefDatabase<PersonalityNodeDef>.AllDefsListForReading)
            {
                nodes.Add(def.defName, new PersonalityNode(def, pawn));
            }
            int seed = PersonalityHelper.PawnSeed(pawn);
            Random random = new(seed);
            foreach (PersonalityNode node in nodes.Values)
            {
                float r = random.Next(0, 100);
                node.BaseRating = r / 100f;
                node.ModifyRating(pawn);
            }
        }

        public void ExposeData()
        {
            Scribe_Collections.Look(ref nodes, "personality", LookMode.Value, LookMode.Deep);
        }

    }
}
