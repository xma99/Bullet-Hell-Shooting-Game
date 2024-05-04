namespace Alpha_Danmaku_Rush_Demo.Src.Utils;

using Microsoft.Xna.Framework;

public static class ColorHelper
{
    public static Color FromName(string colorName)
    {
        return colorName.ToLower() switch
        {
            "red" => Color.Red,
            "blue" => Color.Blue,
            "green" => Color.Green,
            "yellow" => Color.Yellow,
            // Add more color mappings as needed
            _ => Color.White, // Default color or throw an exception if preferred
        };
    }
}
