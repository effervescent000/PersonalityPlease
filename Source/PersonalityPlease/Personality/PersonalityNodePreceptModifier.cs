using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Personality
{
    public class PersonalityNodePreceptModifier
    {
        //public PersonalityNodeDef nodeDef;
        public PreceptDef preceptDef;
        public float modifier;

        public override string ToString()
        {
            return $"Precept: {preceptDef.defName} @ {modifier}";
        }

    }


}
