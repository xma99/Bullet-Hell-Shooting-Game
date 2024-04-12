using Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies;
using Microsoft.Xna.Framework;

namespace Alpha_Danmaku_Rush_Demo.Src.Managers;

public class ScoreManager : IGameObserver
{
    public int Score { get; private set; }
    private double timeSinceLastIncrement;


    public void OnEnemyKilled(IEnemy enemy)
    {
        Score += 100;  // Increment score when an enemy is killed
    }

    public void OnHealthChanged(int currentHealth)
    {
        // This might not be relevant for ScoreManager; no implementation needed.
    }

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