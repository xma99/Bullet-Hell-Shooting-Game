using System;
using Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Alpha_Danmaku_Rush_Demo.Src.Managers.Level;

namespace Alpha_Danmaku_Rush_Demo.Src.Managers;

public class EnemyManager
{
    public List<Enemy> enemies = new List<Enemy>();
    private Random random = new Random();
    private ContentManager Content;
    GraphicsDeviceManager _graphics;

    public EnemyManager(ContentManager content, GraphicsDeviceManager gdManager)
    {
        this.Content = content;

        _graphics = gdManager;
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
        enemies.RemoveAll(e => !e.IsActive);
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

    public void SpawnEnemyA(EnemyBulletType bulletType)
    {
        Vector2 spawnPosition = new Vector2(random.Next(_graphics.GraphicsDevice.Viewport.Width), random.Next(_graphics.GraphicsDevice.Viewport.Height));
        float enemySpeed = 3.0f; // Adjust as needed
        Enemy enemy = EnemyFactory.CreateEnemy(Content, EnemyType.RegularA, spawnPosition, enemySpeed);
        enemy.AddBullet(bulletType);
        Add(enemy);
    }
    public void SpawnEnemyB(EnemyBulletType bulletType)
    {
        Vector2 spawnPosition = new Vector2(random.Next(_graphics.GraphicsDevice.Viewport.Width), random.Next(_graphics.GraphicsDevice.Viewport.Height));
        float enemySpeed = 5.0f; // Adjust as needed
        Enemy enemy = EnemyFactory.CreateEnemy(Content, EnemyType.RegularB, spawnPosition, enemySpeed);
        enemy.AddBullet(bulletType);
        Add(enemy);
    }
    public void SpawnEnemyM(EnemyBulletType bulletType)
    {
        Vector2 spawnPosition = new Vector2(random.Next(_graphics.GraphicsDevice.Viewport.Width), random.Next(_graphics.GraphicsDevice.Viewport.Height));
        float enemySpeed = 3.0f; // Adjust as needed
        Enemy enemy = EnemyFactory.CreateEnemy(Content, EnemyType.MidBoss, spawnPosition, enemySpeed);
        enemy.AddBullet(bulletType);
        Add(enemy);
    }
    public void SpawnEnemyF(EnemyBulletType bulletType)
    {
        Vector2 spawnPosition = new Vector2(random.Next(_graphics.GraphicsDevice.Viewport.Width), random.Next(_graphics.GraphicsDevice.Viewport.Height));
        float enemySpeed = 3.0f; // Adjust as needed
        Enemy enemy = EnemyFactory.CreateEnemy(Content, EnemyType.FinalBoss, spawnPosition, enemySpeed);
        enemy.AddBullet(bulletType);
        Add(enemy);
    }



}
