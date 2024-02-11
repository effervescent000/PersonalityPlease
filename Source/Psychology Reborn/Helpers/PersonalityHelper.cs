using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Personality
{
    public static class PersonalityHelper
    {

        public static int PawnSeed(Pawn pawn)
        {
            int thingID = pawn.thingIDNumber;
            int worldID = Find.World.info.Seed;
            return Gen.HashCombineInt(thingID, worldID);
        }


    }
}
