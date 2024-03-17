using Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace Alpha_Danmaku_Rush_Demo.Src.Managers;

public class EnemyManager
{
    private List<Enemy> enemies = new List<Enemy>();

    public void Add(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public void Update(GameTime gameTime, Vector2 playerPosition)
    {
        foreach (var enemy in enemies)
        {
            enemy.Update(gameTime, playerPosition);
        }
        enemies.RemoveAll(e => !e.isActive);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var enemy in enemies)
        {
            enemy.Draw(spriteBatch);
        }
    }

    public void Clear()
    {
        enemies.Clear();
    }

    private void SpawnEnemyA()
    {
        Texture2D enemySprite = Content.Load<Texture2D>("a");
        Vector2 spawnPosition = new Vector2(random.Next(GraphicsDevice.Viewport.Width), random.Next(GraphicsDevice.Viewport.Height));
        float enemySpeed = 3.0f; // Adjust as needed
        Enemy enemy = EnemyFactory.CreateEnemy(EnemyType.RegularA, enemySprite, spawnPosition, enemySpeed, enemyABullet);
        enemyManager.Add(enemy);
    }
    private void SpawnEnemyB()
    {
        Texture2D enemySprite = Content.Load<Texture2D>("b");
        Vector2 spawnPosition = new Vector2(random.Next(GraphicsDevice.Viewport.Width), random.Next(GraphicsDevice.Viewport.Height));
        float enemySpeed = 5.0f; // Adjust as needed
        Enemy enemy = EnemyFactory.CreateEnemy(EnemyType.RegularB, enemySprite, spawnPosition, enemySpeed, enemyBBullet);
        enemyManager.Add(enemy);
    }
    private void SpawnEnemyM()
    {
        Texture2D enemySprite = Content.Load<Texture2D>("midBoss");
        Vector2 spawnPosition = new Vector2(random.Next(GraphicsDevice.Viewport.Width), random.Next(GraphicsDevice.Viewport.Height));
        float enemySpeed = 3.0f; // Adjust as needed
        Enemy enemy = EnemyFactory.CreateEnemy(EnemyType.MidBoss, enemySprite, spawnPosition, enemySpeed, midBossBullet);
        enemyManager.Add(enemy);
    }
    private void SpawnEnemyF()
    {
        int screenWidth = GraphicsDevice.Viewport.Width;
        Texture2D enemySprite = Content.Load<Texture2D>("finalBoss");
        Vector2 spawnPosition = new Vector2(screenWidth / 2 - enemySprite.Width / 2, 0);
        float enemySpeed = 3.0f; // Adjust as needed
        Enemy enemy = EnemyFactory.CreateEnemy(EnemyType.FinalBoss, enemySprite, spawnPosition, enemySpeed, finalBossBullet);
        enemyManager.Add(enemy);
    }



}
