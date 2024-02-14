using System.Collections.Generic;
using Verse;

namespace Personality
{
    public class PersonalityNodeDef : Def
    {
        public string highDescription;
        public string lowDescription;
        public List<PersonalityNodeTraitModifier> traitModifiers;
        public List<PersonalityNodePreceptModifier> preceptModifiers;
        public List<PersonalityStatModifier> statModifiers;
    }
}
