using System;
using Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Alpha_Danmaku_Rush_Demo.Src.Managers;

public class EnemyManager
{
    private List<Enemy> enemies = new List<Enemy>();
    private Random random = new Random();
    private Microsoft.Xna.Framework.Content.ContentManager Content;
    GraphicsDeviceManager _graphics;

    Texture2D enemyABullet;
    Texture2D enemyBBullet;
    Texture2D midBossBullet;
    Texture2D finalBossBullet;

    public EnemyManager(Microsoft.Xna.Framework.Content.ContentManager content, GraphicsDeviceManager gdManager)
    {
        this.Content = content;
        // Load the texture
        enemyABullet = content.Load<Texture2D>("bullettest1");
        enemyBBullet = content.Load<Texture2D>("bullettest1");
        midBossBullet = content.Load<Texture2D>("bubble");
        finalBossBullet = content.Load<Texture2D>("bubble");
    }


    public void Add(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public void Update(GameTime gameTime, Vector2 playerPosition)
    {
        // Update each enemy
        foreach (var enemy in enemies)
        {
            enemy.Update(gameTime, playerPosition);
        }

        // Optional: Handle collisions (This is just a placeholder for where you would handle it)
        // HandleCollisions();

        // Remove inactive enemies from the list
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
        Vector2 spawnPosition = new Vector2(random.Next(_graphics.GraphicsDevice.Viewport.Width), random.Next(_graphics.GraphicsDevice.Viewport.Height));
        float enemySpeed = 3.0f; // Adjust as needed
        Enemy enemy = EnemyFactory.CreateEnemy(EnemyType.RegularA, enemySprite, spawnPosition, enemySpeed, enemyABullet);
        Add(enemy);
    }
    private void SpawnEnemyB()
    {
        Texture2D enemySprite = Content.Load<Texture2D>("b");
        Vector2 spawnPosition = new Vector2(random.Next(_graphics.GraphicsDevice.Viewport.Width), random.Next(_graphics.GraphicsDevice.Viewport.Height));
        float enemySpeed = 5.0f; // Adjust as needed
        Enemy enemy = EnemyFactory.CreateEnemy(EnemyType.RegularB, enemySprite, spawnPosition, enemySpeed, enemyBBullet);
        Add(enemy);
    }
    private void SpawnEnemyM()
    {
        Texture2D enemySprite = Content.Load<Texture2D>("midBoss");
        Vector2 spawnPosition = new Vector2(random.Next(_graphics.GraphicsDevice.Viewport.Width), random.Next(_graphics.GraphicsDevice.Viewport.Height));
        float enemySpeed = 3.0f; // Adjust as needed
        Enemy enemy = EnemyFactory.CreateEnemy(EnemyType.MidBoss, enemySprite, spawnPosition, enemySpeed, midBossBullet);
        Add(enemy);
    }
    private void SpawnEnemyF()
    {
        int screenWidth = _graphics.GraphicsDevice.Viewport.Width;
        Texture2D enemySprite = Content.Load<Texture2D>("finalBoss");
        Vector2 spawnPosition = new Vector2(screenWidth / 2 - enemySprite.Width / 2, 0);
        float enemySpeed = 3.0f; // Adjust as needed
        Enemy enemy = EnemyFactory.CreateEnemy(EnemyType.FinalBoss, enemySprite, spawnPosition, enemySpeed, finalBossBullet);
        Add(enemy);
    }



}
