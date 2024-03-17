using Microsoft.Xna.Framework;

namespace Alpha_Danmaku_Rush_Demo.Src.Managers;

public class ScoreManager
{
    public int Score { get; private set; }
    private double timeSinceLastIncrement;

    public ScoreManager()
    {
        Score = 0;
        timeSinceLastIncrement = 0.0;
    }

    public void Update(GameTime gameTime)
    {
        timeSinceLastIncrement += gameTime.ElapsedGameTime.TotalSeconds;
        
        if (timeSinceLastIncrement >= 1.0)
        {
            Score += 1;
            timeSinceLastIncrement -= 1.0;
        }
    }
    
    public void AddScoreForEnemyKill()
    {
        Score += 100;
    }
    
    public void Reset()
    {
        Score = 0;
        timeSinceLastIncrement = 0.0;
    }

}