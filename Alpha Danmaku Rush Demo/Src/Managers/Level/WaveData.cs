using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Alpha_Danmaku_Rush_Demo.Src.Managers.Level;

public class WaveData
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("time")]
    public List<string> Time { get; set; }

    [JsonPropertyName("enemyType")]
    public string EnemyType { get; set; }

    [JsonPropertyName("enemyAmount")]
    public int EnemyAmount { get; set; }

    [JsonPropertyName("interval")]
    public int Interval { get; set; }

    [JsonPropertyName("enemyBulletType")]
    public EnemyBulletType EnemyBulletType { get; set; }
}