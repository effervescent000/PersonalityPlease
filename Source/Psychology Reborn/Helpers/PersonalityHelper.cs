
using System.Collections.Generic;
using Verse;

namespace Personality
{
    [StaticConstructorOnStartup]
    public static class PersonalityHelper
    {
        public static TraitLedStore traitLedStore = new();
        public static PreceptLedStore preceptLedStore = new();

        private static List<PersonalityNodeDef> PersonalityNodeDefList => DefDatabase<PersonalityNodeDef>.AllDefsListForReading;

        static PersonalityHelper()
        {
            foreach (PersonalityNodeDef nodeDef in PersonalityNodeDefList)
            {
                if (!nodeDef.traitModifiers.NullOrEmpty())
                {
                    foreach (PersonalityNodeTraitModifier traitMod in nodeDef.traitModifiers)
                    {
                        traitLedStore.AppendValue(nodeDef.defName, traitMod);
                    }
                }
                if (!nodeDef.preceptModifiers.NullOrEmpty())
                {
                    foreach (PersonalityNodePreceptModifier preceptMod in nodeDef.preceptModifiers)
                    {
                        preceptLedStore.AppendValue(nodeDef.defName, preceptMod);
                    }
                }

            }

        }

        public static int PawnSeed(Pawn pawn)
        {
            int thingID = pawn.thingIDNumber;
            int worldID = Find.World.info.Seed;
            return Gen.HashCombineInt(thingID, worldID);
        }

        public static PsychologyComp Comp(Pawn pawn)
        {
            return pawn.GetComp<PsychologyComp>();
        }

        public static List<PersonalityNodeDef> GetAll
        {
            get => DefDatabase<PersonalityNodeDef>.AllDefsListForReading;
        }
    }
}
