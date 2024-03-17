using System.Collections.Generic;

namespace Alpha_Danmaku_Rush_Demo.Src.Managers.Level;

public class WaveData
{
    public string Id { get; set; }
    public List<string> Time { get; set; }
    public string EnemyType { get; set; }
    public int EnemyAmount { get; set; }
    public int Interval { get; set; }
    public EnemyBulletType EnemyBulletType { get; set; }
    public string BossType { get; set; }
}