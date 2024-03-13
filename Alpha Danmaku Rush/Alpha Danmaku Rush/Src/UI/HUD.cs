namespace Alpha_Danmaku_Rush.Src.UI;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Alpha_Danmaku_Rush.Src.Entities;
using Alpha_Danmaku_Rush.Src.Managers;

public class HUD
{
    private SpriteFont font; // 用于绘制文本的字体
    private Player player; // 参考玩家对象以访问其生命值
    private ScoreManager scoreManager; // 参考分数管理器以访问分数

    public HUD(SpriteFont font, Player player, ScoreManager scoreManager)
    {
        this.font = font;
        this.player = player;
        this.scoreManager = scoreManager;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // 绘制玩家分数
        string scoreText = $"Score: {scoreManager.Score}";
        spriteBatch.DrawString(font, scoreText, new Vector2(20, 20), Color.White);

        // 绘制玩家生命值
        string healthText = $"Health: {player.Health}";
        spriteBatch.DrawString(font, healthText, new Vector2(20, 50), Color.White);
    }
}
