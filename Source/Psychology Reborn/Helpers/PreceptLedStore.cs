using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Personality.Helpers
{
    public class PreceptLedStore
    {
        private readonly Dictionary<string, Dictionary<string, float>> preceptLedStore = new();

        public void AppendValue(string persNodeName, PersonalityNodePreceptModifier nodeMod)

        {
            Log.Message($"Adding to the store {persNodeName}: {nodeMod}");
            string preceptName = nodeMod.preceptDef.defName;
            bool success = preceptLedStore.TryAdd(preceptName, new Dictionary<string, float>() { { persNodeName, nodeMod.modifier } });
            if (!success)
            {
                preceptLedStore[preceptName].Add(persNodeName, nodeMod.modifier);
            }


        }
    }
}
