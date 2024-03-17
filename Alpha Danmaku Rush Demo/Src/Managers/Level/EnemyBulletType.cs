using System.Text.Json.Serialization;

namespace Alpha_Danmaku_Rush_Demo.Src.Managers.Level;

public class EnemyBulletType
{
    [JsonPropertyName("color")]
    public string Color { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("amount")]
    public int Amount { get; set; }

    [JsonPropertyName("speed")]
    public int Speed { get; set; }
}