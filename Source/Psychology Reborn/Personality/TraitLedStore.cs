using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Personality
{
    public class TraitLedStore
    {

        public Dictionary<Pair<string, int>, Dictionary<string, float>> traitLedStore = new Dictionary<Pair<string, int>, Dictionary<string, float>>();


        public TraitLedStore()
        {

        }

        public void AppendValue(string personalityNodeName, PersonalityNodeTraitModifier traitMod)
        {
            Pair<string, int> traitValues = new Pair<string, int>(traitMod.trait.defName, traitMod.degree);
            bool success = traitLedStore.TryAdd(traitValues, new Dictionary<string, float>() { { personalityNodeName, traitMod.modifier } });
            if (!success)
            {
                traitLedStore[traitValues].Add(personalityNodeName, traitMod.modifier);

            }
            Log.Message($"Adding {traitValues} to the store with values {personalityNodeName}, {traitMod.modifier}");
        }
    }
}
