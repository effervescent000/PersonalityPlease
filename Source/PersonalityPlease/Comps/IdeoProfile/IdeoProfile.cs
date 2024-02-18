using RimWorld;
using System.Collections.Generic;

namespace Personality;

public class IdeoProfile
{
    public Ideo Ideo;

    // dict of <PersonalityNodeDefName, float>
    public Dictionary<string, float> Values = new();

    public IdeoProfile(Ideo ideo)
    {
        Ideo = ideo;
    }

    public void MakeValues(List<PersonalityNodeDef> nodeDefs = null)
    {
        Values.Clear();
        nodeDefs ??= PersonalityHelper.GetAll;

        // initialize Values
        foreach (PersonalityNodeDef def in nodeDefs)
        {
            Values.Add(def.defName, 0f);
        }

        // iterate over precepts and add their values to Values
        foreach (Precept precept in Ideo.PreceptsListForReading)
        {
            Dictionary<string, float> preceptVals = PreceptLedStore.GetValue(precept.def.defName);

            foreach (string key in Values.Keys)
            {
                if (preceptVals.TryGetValue(key, out float value))
                {
                    Values[key] += value * 2f;
                }
            }
        }
    }
}