using System.Collections.Generic;
using Verse;

namespace Personality
{
    public class PersonalityNodeDef : Def
    {
        public string highDescription;
        public string lowDescription;
        public string highLabel;
        public string lowLabel;
        public List<PersonalityNodeTraitModifier> traitModifiers;
        public List<PersonalityNodePreceptModifier> preceptModifiers;
        public List<PersonalityNodeModifier<GeneDef>> geneModifiers;

        public List<PersonalityStatEffect> statModifiers;
    }
}