﻿#nullable enable

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
            if (traitLedStore.ContainsKey(trait))
            {
                Dictionary<string, float>? result = traitLedStore.TryGetValue(trait);
                if (result == null)
                {
                    throw new Exception("Trait found in traitLedStore with no modifiers attached");
                }
                return result;
            }
            return null;


        }
    }
}
