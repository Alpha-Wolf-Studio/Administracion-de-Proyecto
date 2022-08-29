using UnityEngine;

static public class Province
{
    static private readonly Color[] possibleProvinceColors = { Color.blue, Color.white, Color.cyan, Color.magenta, Color.yellow};
    static public Color GetProvinceColor(int index) => possibleProvinceColors[index];
}
