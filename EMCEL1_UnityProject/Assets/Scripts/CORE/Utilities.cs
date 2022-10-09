using UnityEngine;

namespace CORE
{
    public static class Utilities
    {
        public static bool RandomBool(float trueChance)
        {
            trueChance = Mathf.Clamp(trueChance, 0, 100);
            if (Random.Range(0, 100) <= trueChance) return true;
            return false;
        }

        public static Color ColorWithTransparencies(Color colorValue, float transparencies)
        {
            return new Color(colorValue.r, colorValue.g, colorValue.b, transparencies);
        }
    }
}