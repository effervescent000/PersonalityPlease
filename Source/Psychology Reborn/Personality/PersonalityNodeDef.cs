using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Personality
{
    public class PersonalityNodeDef : Def
    {
        public string highDescription;
        public string lowDescription;
        public List<PersonalityNodeTraitModifier> traitModifiers;
        public List<PersonalityNodePreceptModifier> preceptModifiers;
    }
}
