#nullable enable

using RimWorld;
using System;
using System.Collections.Generic;
using Verse;

namespace Personality
{
    public class TraitLedStore
    {
        private readonly Dictionary<Pair<string, int>, Dictionary<string, float>> traitLedStore = new();


        public TraitLedStore() { }

        public void AppendValue(string personalityNodeName, PersonalityNodeTraitModifier traitMod)
        {
            Pair<string, int> traitValues = new(traitMod.trait.defName, traitMod.degree);
            bool success = traitLedStore.TryAdd(traitValues, new Dictionary<string, float>() { { personalityNodeName, traitMod.modifier } });
            if (!success)
            {
                traitLedStore[traitValues].Add(personalityNodeName, traitMod.modifier);

            }
            Log.Message($"Adding {traitValues} to the store with values {personalityNodeName}, {traitMod.modifier}");
        }

        public Dictionary<string, float>? GetValue(Pair<string, int> trait)
        {
            Log.Message("checking traitPair value: " + trait.ToString());
            if (traitLedStore.TryGetValue(trait, out var value))
            {
                if (value == null)
                {
                    throw new Exception("Trait found in traitLedStore with no modifiers attached");
                }
                return value;
            }
            return null;


        }
    }
}
