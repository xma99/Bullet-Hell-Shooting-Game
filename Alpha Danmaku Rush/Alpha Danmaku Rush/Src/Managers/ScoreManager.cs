namespace Alpha_Danmaku_Rush.Src.Managers;

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class ScoreManager
{
    public int Score { get; private set; } // 当前分数
    private double timeSinceLastIncrement; // 上次分数增加以来的时间

    public ScoreManager()
    {
        Score = 0;
        timeSinceLastIncrement = 0.0;
    }

    public void Update(GameTime gameTime)
    {
        // 累计自上次更新以来经过的时间
        timeSinceLastIncrement += gameTime.ElapsedGameTime.TotalSeconds;

        // 如果超过1秒，分数增加1，并重置累计时间
        if (timeSinceLastIncrement >= 1.0)
        {
            Score += 1;
            timeSinceLastIncrement -= 1.0; // 重置累计时间，保留小数部分
        }
    }

    // 当玩家杀死敌人时调用此方法增加分数
    public void AddScoreForEnemyKill()
    {
        Score += 100;
    }

    // 重置分数，例如在游戏重新开始时调用
    public void Reset()
    {
        Score = 0;
        timeSinceLastIncrement = 0.0;
    }
}
