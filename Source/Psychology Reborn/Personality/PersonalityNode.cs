using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Verse;

namespace Personality
{
    public class PersonalityNode : IExposable
    {
        public Pawn pawn;
        public PersonalityNodeDef def;
        private float baseRating;
        private float cachedRating = 0f;


        public PersonalityNode() { }

        public PersonalityNode(Pawn pawn, PersonalityNodeDef def)
        {
            this.pawn = pawn;
            this.def = def;
        }

        public float AdjustedRating
        {
            get => cachedRating; set => cachedRating = value;
        }

        public float BaseRating { get => baseRating; set => baseRating = value; }

        public override string ToString() => $"{this.def.defName} @ {this.baseRating}";

        public void ExposeData()
        {
            Scribe_Defs.Look(ref this.def, "def");
            Scribe_Values.Look(ref this.baseRating, "baseRating");
        }
    }

}
