using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Alpha_Danmaku_Rush_Demo.Src.Managers.Level;

public class LevelData
{
    [JsonPropertyName("waves")]
    public List<WaveData> Waves { get; set; }
}
