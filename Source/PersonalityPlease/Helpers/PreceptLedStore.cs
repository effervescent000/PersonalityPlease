using System.Collections.Generic;

namespace Personality
{
    public static class PreceptLedStore
    {
        private static readonly Dictionary<string, Dictionary<string, float>> store = new();

        public static void AppendValue(string persNodeName, PersonalityNodePreceptModifier nodeMod)
        {
            string preceptName = nodeMod.preceptDef.defName;
            bool success = store.TryAdd(preceptName, new Dictionary<string, float>() { { persNodeName, nodeMod.modifier } });
            if (!success)
            {
                store[preceptName].Add(persNodeName, nodeMod.modifier);
            }
        }

        public static Dictionary<string, float> GetValue(string preceptName)
        {
            if (store.TryGetValue(preceptName, out Dictionary<string, float> value))
            {
                return value;
            }
            return null;
        }

        public static Dictionary<string, Dictionary<string, float>> GetAll => store;
    }
}