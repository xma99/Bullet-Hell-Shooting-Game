using System;
using Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Alpha_Danmaku_Rush_Demo.Src.Managers.Level;
using Alpha_Danmaku_Rush_Demo.Src.Entities.Bullet;

namespace Alpha_Danmaku_Rush_Demo.Src.Managers;

public class EnemyManager
{
    public List<IEnemy> enemies = new List<IEnemy>();
    private Random random = new Random();
    private ContentManager Content;
    GraphicsDeviceManager _graphics;

    private List<IGameObserver> observers = new List<IGameObserver>();
    // Other fields remain unchanged...

    public void RegisterObserver(IGameObserver observer)
    {
        observers.Add(observer);
    }

    public void UnregisterObserver(IGameObserver observer)
    {
        observers.Remove(observer);
    }

    private void NotifyEnemyKilled(IEnemy enemy)
    {
        foreach (var observer in observers)
        {
            observer.OnEnemyKilled(enemy);
        }
    }

    public void Update(GameTime gameTime, Vector2 playerPosition)
    {
        foreach (var enemy in enemies)
        {
            enemy.Update(gameTime, playerPosition);
            //enemy.Attack(gameTime, playerPosition);
            if (!enemy.IsActive)
            {
                NotifyEnemyKilled(enemy);
            }
        }
        enemies.RemoveAll(e => !e.IsActive);
    }
    public int[] getSize()
    {
        return new int[] { _graphics.GraphicsDevice.Viewport.Width, _graphics.GraphicsDevice.Viewport.Height };
    }

    public EnemyManager(ContentManager content, GraphicsDeviceManager gdManager)
    {
        this.Content = content;

        _graphics = gdManager;
    }

    public void Add(IEnemy enemy)
    {
        enemies.Add(enemy);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var enemy in enemies)
        {
            enemy.Draw(spriteBatch);
            
        }
    }

    public void loadAmmo(EnemyBulletType type)
    {
       enemies.ForEach(enemy =>
        {
            Queue<Bullet> bullets = new Queue<Bullet>();

            for (int i = 0; i < type.Amount; i++)
            {
                Microsoft.Xna.Framework.Vector2 startPosition = new Microsoft.Xna.Framework.Vector2(enemy.Position.X, enemy.Position.Y);
                Bullet bullet = BulletFactory.CreateBullet(Content, startPosition, Microsoft.Xna.Framework.Vector2.Zero, type);
                bullets.Enqueue(bullet);
                //bullet.Update();
                
                

            }
            enemy.bulletList = bullets;


        });

    }
    public void Clear()
    {
        enemies.Clear();
    }

    public void SpawnEnemyA(EnemyBulletType bulletType,SpriteBatch spriteBatch)
    {
        Vector2 spawnPosition = new Vector2(random.Next(_graphics.GraphicsDevice.Viewport.Width), random.Next(_graphics.GraphicsDevice.Viewport.Height));
        float enemySpeed = 3.0f; // Adjust as needed
        IEnemy enemy = EnemyFactory.CreateEnemy(Content, EnemyType.RegularA, spawnPosition, enemySpeed,bulletType, spriteBatch);
        Add(enemy);
    }
    public void SpawnEnemyB(EnemyBulletType bulletType, SpriteBatch spriteBatch)
    {
        Vector2 spawnPosition = new Vector2(random.Next(_graphics.GraphicsDevice.Viewport.Width), random.Next(_graphics.GraphicsDevice.Viewport.Height));
        float enemySpeed = 5.0f; // Adjust as needed
        IEnemy enemy = EnemyFactory.CreateEnemy(Content, EnemyType.RegularB, spawnPosition, enemySpeed,bulletType, spriteBatch);
        Add(enemy);
    }
    public void SpawnEnemyM(EnemyBulletType bulletType,SpriteBatch spriteBatch)
    {
        Vector2 spawnPosition = new Vector2(0,0);
        float enemySpeed = 1.0f; // Adjust as needed
        IEnemy enemy = EnemyFactory.CreateEnemy(Content, EnemyType.MidBoss, spawnPosition, enemySpeed, bulletType, spriteBatch);
        Add(enemy);
    }
    public void SpawnEnemyF(EnemyBulletType bulletType, SpriteBatch spriteBatch)
    {
        Vector2 spawnPosition = new Vector2(random.Next(_graphics.GraphicsDevice.Viewport.Width), random.Next(_graphics.GraphicsDevice.Viewport.Height));
        float enemySpeed = 3.0f; // Adjust as needed
        IEnemy enemy = EnemyFactory.CreateEnemy(Content, EnemyType.FinalBoss, spawnPosition, enemySpeed, bulletType, spriteBatch);
        
        Add(enemy);
    }
}
