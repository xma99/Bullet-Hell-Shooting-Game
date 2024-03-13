using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Alpha_Danmaku_Rush.Src.Entities.Level;

public class LevelData
{
    [JsonPropertyName("levels")]
    public List<Level> Levels { get; set; }
}