namespace Alpha_Danmaku_Rush.Src.Managers;

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class ScoreManager
{
    public int Score { get; private set; }
    private SpriteFont font;
    private Vector2 position;

    public ScoreManager(SpriteFont font, Vector2 position)
    {
        this.font = font;
        this.position = position;
        Score = 0;
    }

    public void AddScore(int points)
    {
        Score += points;
    }

    public void ResetScore()
    {
        Score = 0;
    }

    public void Update(GameTime gameTime)
    {
        // Update logic, if any, goes here.
        // For example, you might have time-based scoring.
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(font, $"Score: {Score}", position, Color.White);
    }
}
