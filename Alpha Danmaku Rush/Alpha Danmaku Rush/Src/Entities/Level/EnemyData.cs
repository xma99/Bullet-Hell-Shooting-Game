using Microsoft.Xna.Framework;

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Alpha_Danmaku_Rush.Src.Entities.Level;

public class EnemyData
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("position")]
    public Vector2 Position { get; set; }

    [JsonPropertyName("spawnTime")]
    public int SpawnTime { get; set; }
}