using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Personality;

public class IdeoProfile
{
    public Ideo Ideo;

    // dict of <PersonalityNodeDefName, float>
    public Dictionary<string, SemiClampedValue> Values = new();

    public IdeoProfile(Ideo ideo)
    {
        Ideo = ideo;
    }

    public void MakeValues()
    {
        Values.Clear();
        List<PersonalityNodeDef> nodeDefs = PersonalityHelper.GetAll;

        // initialize Values
        foreach (PersonalityNodeDef def in nodeDefs)
        {
            Values.Add(def.defName, new(0f));
        }

        // iterate over precepts and add their values to Values
        foreach (Precept precept in Ideo.PreceptsListForReading)
        {
            Dictionary<string, float> preceptVals = PreceptLedStore.GetValue(precept.def.defName);

            foreach (var def in nodeDefs)
            {
                if (preceptVals != null && preceptVals.TryGetValue(def.defName, out float value))
                {
                    Values[def.defName].OffsetValue(value * 3f);
                }
            }
        }
    }
}