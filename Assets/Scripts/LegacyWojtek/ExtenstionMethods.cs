using UnityEngine;

public static class ExtenstionMethods 
{

    public static float Round(this float value, float roundTo) => Mathf.Round(value * roundTo) / roundTo;

    
}
