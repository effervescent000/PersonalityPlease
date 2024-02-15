#nullable enable

using System.Collections.Generic;
using Verse;

namespace Personality
{
    public class PreceptLedStore
    {
        private readonly Dictionary<string, Dictionary<string, float>> preceptLedStore = new();

        public void AppendValue(string persNodeName, PersonalityNodePreceptModifier nodeMod)
        {
            
            string preceptName = nodeMod.preceptDef.defName;
            bool success = preceptLedStore.TryAdd(preceptName, new Dictionary<string, float>() { { persNodeName, nodeMod.modifier } });
            if (!success)
            {
                preceptLedStore[preceptName].Add(persNodeName, nodeMod.modifier);
            }
            Log.Message($"Added to the store {persNodeName}: {nodeMod}");
        }

        public Dictionary<string, float>? GetValue(string preceptName)
        {
            if (preceptLedStore.TryGetValue(preceptName, out var value))
            {
                return value;
            }
            return null;

        }
    }
}
