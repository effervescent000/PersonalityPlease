using System;
using Verse;

namespace Personality
{
    public class PersonalityComparison
    {
        public string personalityDefName;
        public float initValue;
        public float reciValue;

        public PersonalityComparison(float initValue, float reciValue, string personalityDefName)
        {
            this.personalityDefName = personalityDefName;
            this.initValue = initValue;
            this.reciValue = reciValue;
        }

        public float Difference
        {
            get
            {
                float diff = Math.Abs(initValue - reciValue);
                //Log.Message($"initValue: {initValue}, reciValue: {reciValue}, diff: {diff}");
                return diff;
            }
        }

    }
}
