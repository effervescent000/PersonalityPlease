using System.Collections.Generic;
using Verse;

namespace Personality;

public class ModifierTracker
{

    private readonly Dictionary<string, ModifierValues> storedValues = new();

    public ModifierTracker()
    {

    }

    public void AppendValue(string key, ModifierValues newValues)
    {
        bool keyPresent = storedValues.ContainsKey(key);
        if (!keyPresent)
        {
            storedValues.Add(key, new ModifierValues());
        }

        storedValues[key].Factor += 1 - newValues.Factor;
        storedValues[key].Offset += newValues.Offset;
    }

    public Dictionary<string, ModifierValues> StoredValues => storedValues;

    public ModifierValues GetValue(string key) {
        if (storedValues.TryGetValue(key, out ModifierValues values)) {
            return values;
        }
        return new ModifierValues();
    }

}
