using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Verse;

namespace Psychology_Reborn
{
    public class PersonalityNode
    {
        public Pawn pawn;
        public PersonalityNodeDef def;
        public float value = 0;

        public PersonalityNode() { }

        public PersonalityNode(Pawn pawn, PersonalityNodeDef def) { 
        
            this.pawn = pawn;
            this.def = def;
        }
        public override string ToString() => this.def.defName;
    }

}
