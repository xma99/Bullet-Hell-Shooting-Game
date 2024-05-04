using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using Alpha_Danmaku_Rush_Demo.Src.Entities.Bullet;
using Alpha_Danmaku_Rush_Demo.Src.Utils;
using Alpha_Danmaku_Rush_Demo.Src.Managers.Level;
using Microsoft.Xna.Framework.Content;
using Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies.Decorator.Attack;
using System.Text.RegularExpressions;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies
{
    public class Enemy : IEnemy
    {
        private GameObject gameObject;
        private float speed;
        private EnemyType enemyType;
        private ContentManager content;
        public EnemyBulletType enemyBulletType;
        //public Vector2 position;
        
        private SpriteBatch BulletSprite;
        private TimeSpan AttackTimer;
        private Queue<Bullet.Bullet> _bullets=new Queue<Bullet.Bullet>();

        public Queue<Bullet.Bullet> bulletList { 
            get { return this._bullets; }
            set { _bullets= value; }
        }

        public Vector2 Position
        {
            get => gameObject.Position;
            set => gameObject.Position = value;
        }

        public Texture2D Sprite => gameObject.Sprite;

        public bool IsActive
        {
            get => gameObject.IsActive;
            set => gameObject.IsActive = value;
        }

        public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, Sprite.Width, Sprite.Height);

        public Enemy(ContentManager content, Vector2 startPosition, float movementSpeed, EnemyType enemyType,EnemyBulletType bulletType,SpriteBatch spriteBatch)
        {
            this.content = content;
            this.enemyType = enemyType;
            string texturePath = GetTexturePath(enemyType);
            this.gameObject = new GameObject(content.Load<Texture2D>(texturePath), startPosition);
            this.speed = movementSpeed;
            this.enemyBulletType = bulletType;
            BulletSprite = spriteBatch;
            AttackTimer=TimeSpan.Zero;
        }

        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            Move(gameTime, playerPosition);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //setBulletSprite(spriteBatch);
            if (IsActive)
            {
                spriteBatch.Draw(Sprite, Position, Color.White);
            }
        }

        public void Attack(GameTime gameTime, Vector2 playerPosition,EnemyBulletType bulletType=null)
        {

            // Test. In this block as well as in the Attackallocator class HARD CODE to create an ENEMY object to see if it can be successfully drawn to enable movement.
            // Trying to rewrite Bullet.
            //Maybe a pointer call at some level is wrong
            foreach (var bullet in _bullets)
            {
                bullet.Draw(BulletSprite);
                bullet.Update(gameTime);
            }
            AttackTimer+= gameTime.ElapsedGameTime;
            TimeSpan interval = new TimeSpan(200);
            RegularAAllocator RegularPattern = new RegularAAllocator(this, interval);
            // Implement attack logic
            if (AttackTimer > TimeSpan.FromMilliseconds(2000)) ;
            {
                AttackTimer = TimeSpan.Zero;
                if (enemyType == EnemyType.RegularA || enemyType == EnemyType.RegularB)
                {
                    AttackCaller attackCaller = new AttackCaller(RegularPattern);
                } 
            }
            //RegularPattern.updateAttack(gameTime);
            foreach(var Bullet in RegularPattern.FiredBullets)
            {
                Bullet.Update(gameTime);
            }            
        }
        public void loadAmmo()
        {
            int amount = enemyBulletType.Amount;
            for(int i = 0; i < amount; i++)
            {
                float x = Position.X;
                float y = Position.Y;
                Vector2 something = new Vector2(x, y);
                
                Bullet.Bullet bullet = BulletFactory.CreateBullet(content,something,Vector2.Zero,enemyBulletType);
                bullet.IsActive = true;
                _bullets.Enqueue(bullet);
            }
        }

        public void setBulletSprite(SpriteBatch spriteBatch)
        {
            this.BulletSprite = spriteBatch;
        }

        private void Move(GameTime gameTime, Vector2 playerPosition)
        {
            Vector2 direction = Vector2.Normalize(playerPosition - Position);
            Position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private string GetTexturePath(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.RegularA:
                    return "a";
                case EnemyType.RegularB:
                    return "b";
                case EnemyType.MidBoss:
                    return "midBoss";
                case EnemyType.FinalBoss:
                    return "finalBoss";
                default:
                    // Default to "a" texture if unknown type is specified
                    return "a";
            }
        }
    }
}
