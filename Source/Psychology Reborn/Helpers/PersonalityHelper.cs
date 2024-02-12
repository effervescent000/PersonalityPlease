
using System.Collections.Generic;
using Verse;

namespace Personality
{
    [StaticConstructorOnStartup]
    public static class PersonalityHelper
    {
        public static TraitLedStore traitLedStore = new();

        private static List<PersonalityNodeDef> PersonalityNodeDefList => DefDatabase<PersonalityNodeDef>.AllDefsListForReading;
        //private static List<TraitDef> TraitDefList => DefDatabase<TraitDef>.AllDefsListForReading;

        static PersonalityHelper()
        {
            foreach (var nodeDef in PersonalityNodeDefList)
            {
                if (nodeDef.traitModifiers.NullOrEmpty())
                {
                    continue;
                }
                foreach (var traitMod in nodeDef.traitModifiers)
                {
                    traitLedStore.AppendValue(nodeDef.defName, traitMod);
                }
            }

        }

        public static int PawnSeed(Pawn pawn)
        {
            int thingID = pawn.thingIDNumber;
            int worldID = Find.World.info.Seed;
            return Gen.HashCombineInt(thingID, worldID);
        }


    }
}
