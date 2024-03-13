using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Alpha_Danmaku_Rush.Src.Entities.Level;

public class Position
{
    [JsonPropertyName("x")]
    public float X { get; set; }

    [JsonPropertyName("y")]
    public float Y { get; set; }
}