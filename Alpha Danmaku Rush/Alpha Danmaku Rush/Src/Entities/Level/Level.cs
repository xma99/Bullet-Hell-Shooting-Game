using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace Alpha_Danmaku_Rush.Src.Entities.Level
{
    public class Level
    {
        [JsonPropertyName("levelNumber")]
        public int LevelNumber { get; set; }
        [JsonPropertyName("enemies")]
        public List<EnemyData> Enemies { get; set; }
    }
}
