using Alpha_Danmaku_Rush.Src.Entities.Enemy;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Alpha_Danmaku_Rush.Src.Entities.Level;
using SharpDX.Direct2D1;

namespace Alpha_Danmaku_Rush.Src.Managers;

public class LevelManager
{
    public List<Level> levels; // 使用列表来存储所有关卡数据
    public List<Enemy> enemies = new List<Enemy>();
    private Texture2D enemyTexture;
    public Level currentLevel;
    private double elapsedTimeSinceLevelStart = 0; // 从当前关卡开始的经过时间

    public LevelManager(Texture2D enemyTexture, string levelsFilePath)
    {
        this.enemyTexture = enemyTexture;
        LoadLevels(levelsFilePath); // 在构造函数中加载关卡数据
    }

    public void Update(GameTime gameTime)
    {
        // 更新从当前关卡开始的经过时间
        elapsedTimeSinceLevelStart += gameTime.ElapsedGameTime.TotalMilliseconds;

        // 遍历当前关卡的所有敌人数据
        for (int i = 0; i < currentLevel.Enemies.Count; i++)
        {
            EnemyData enemyData = currentLevel.Enemies[i];

            // 检查是否到了生成该敌人的时间
            if (elapsedTimeSinceLevelStart >= enemyData.SpawnTime)
            {
                // 生成敌人
                Vector2 position = new Vector2(enemyData.Position.X, enemyData.Position.Y);
                Enemy enemy = new Enemy(enemyTexture, position, DetermineVelocityBasedOnType(enemyData.Type));
                enemies.Add(enemy);

                // 从敌人数据列表中移除，避免重复生成
                currentLevel.Enemies.RemoveAt(i);
                i--; // 因为移除了一个元素，所以索引减1
            }
        }

        // 更新敌人状态
        foreach (var enemy in enemies)
        {
            enemy.Update(gameTime);
        }
        // 移除非活动敌人
        enemies.RemoveAll(e => !e.IsActive);
    }

    public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
    {
        // 绘制所有敌人
        foreach (var enemy in enemies)
        {
            if (enemy.IsActive)
            {
                enemy.Draw(spriteBatch);
            }
        }
    }

    private Vector2 DetermineVelocityBasedOnType(string type)
    {
        // 根据敌人类型确定速度，这需要根据游戏设计具体实现
        // 这里仅为示例
        switch (type)
        {
            case "basic":
                return new Vector2(0, 1); // 基础敌人向下移动
            case "advanced":
                return new Vector2(0, 2); // 高级敌人移动更快
            default:
                return Vector2.Zero; // 未知类型不移动
        }
    }

    private void LoadLevels(string filePath)
    {
        string jsonString = File.ReadAllText(filePath);
        var levelData = JsonSerializer.Deserialize<LevelData>(jsonString);
        levels = levelData?.Levels;

        if (levels != null && levels.Count > 0)
        {
            currentLevel = levels[0]; // 初始化为第一个关卡
        }
    }

    public void NextLevel()
    {
        if (levels != null && currentLevel != null)
        {
            int currentIndex = levels.IndexOf(currentLevel);
            if (currentIndex + 1 < levels.Count)
            {
                currentLevel = levels[currentIndex + 1];
                ResetForNewLevel();
            }
            else
            {
                // 已经是最后一关，可以处理游戏完成逻辑
            }
        }
    }

    private void ResetForNewLevel()
    {
        elapsedTimeSinceLevelStart = 0; // 重置关卡计时
        enemies.Clear(); // 清空当前敌人列表
    }
}


