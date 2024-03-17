using Alpha_Danmaku_Rush_Demo.Src.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities
{
    public enum EnemyType
    {
        RegularA,
        RegularB,
        MidBoss,
        FinalBoss
    }

    public class Enemy
    {
        private Texture2D sprite;
        public Vector2 Position { get; private set; }
        private float movementSpeed;
        private bool isActive;

        private EnemyType type;

        private Vector2 midBossPosition;
        private float midBossMove = 20f;

        private Vector2 finalBossPosition;
        private float finalBossMove = 20f;

        //Attack generate part. Need to update detailed attack logic later, it now fire toward bottom of the screen
        Texture2D BulletSprite;//sprite used to draw bullet
        private Attack attack;//an attack object
        private Vector2 DefaultTarget = new Vector2(0, 1);//default bullet moving direction
        public List<Attack> attackList;
        public bool BulletCheck = true;//allow to add bullet into attacklist

        public bool IsActive => isActive;

        public Enemy(Texture2D sprite, Vector2 startPosition, float movementSpeed, EnemyType type, Texture2D bulletSprite)
        {
            this.sprite = sprite;
            Position = startPosition;
            this.movementSpeed = movementSpeed;
            isActive = true;
            this.type = type;
            BulletSprite = bulletSprite;
            // If it's midBoss
            if (type == EnemyType.MidBoss)
            {
                midBossPosition = startPosition;
            }

            // If it's finalBoss
            if (type == EnemyType.FinalBoss)
            {
                finalBossPosition = startPosition;
            }
            BulletSprite = bulletSprite;
            attackList = new List<Attack>();
            attack = new Attack(BulletSprite, Position, DefaultTarget);
            attackList.Add(attack);

        }

        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            // movementSpeed = 80f; // Set enemy speed
            if (!isActive) return;
            switch (type)
            {
                case EnemyType.MidBoss:
                    UpdateMidBoss(gameTime, playerPosition);
                    break;
                case EnemyType.FinalBoss:
                    UpdateFinalBoss(gameTime, playerPosition);
                    break;
                default:
                    UpdateRegularEnemy(gameTime, playerPosition);
                    break;
            }
            // Deactivate enemy if it goes off-screen
            if (Position.Y > GraphicsDeviceManager.DefaultBackBufferHeight)
            {
                isActive = false;
            }
        }

        private void UpdateMidBoss(GameTime gameTime, Vector2 playerPosition)
        {
            // MidBoss do left-right movement
            float delta = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds) * midBossMove;
            Position = new Vector2(midBossPosition.X + delta, Position.Y);
            DateTime curTime = DateTime.Now;
            int second = curTime.Second;
            if (attackList.Count > 0)
            {
                foreach (Attack attack in attackList)
                {
                    attack.UpdateAttack(gameTime, playerPosition);

                }
            }

            if (second % 3 == 0 && BulletCheck)
            {
                Attack newAttack = new Attack(BulletSprite, Position, DefaultTarget);

                attackList.Add(newAttack);
                BulletCheck = false;
            }
            if (second % 3 != 0 && !BulletCheck)
            {
                BulletCheck = true;
            }

        }

        private void UpdateFinalBoss(GameTime gameTime, Vector2 playerPosition)
        {
            // FinalBoss do left-right movement
            float delta = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds) * finalBossMove;
            Position = new Vector2(finalBossPosition.X + delta, Position.Y);

            DateTime curTime = DateTime.Now;
            int second = curTime.Second;
            if (attackList.Count > 0)
            {
                foreach (Attack attack in attackList.ToList())
                {
                    attack.UpdateAttack(gameTime, playerPosition);
                }
            }

            if (second % 2 == 0 && BulletCheck)
            {
                Attack newAttack = new Attack(BulletSprite, Position, DefaultTarget);
                //newAttack.UpdateAttack(gameTime, playerPosition);
                attackList.Add(newAttack);

                BulletCheck = false;
            }
            if (second % 2 != 0 && !BulletCheck)
            {
                BulletCheck = true;
            }
        }

        private void UpdateRegularEnemy(GameTime gameTime, Vector2 playerPosition)
        {
            movementSpeed = 80f;
            Move(gameTime, playerPosition);
            DateTime curTime = DateTime.Now;
            int second = curTime.Second;
            if (attackList.Count > 0)
            {
                foreach (Attack attack in attackList.ToList())
                {
                    attack.UpdateAttack(gameTime, playerPosition);
                    //if (attack.bulletPosition.Y > GraphicsDeviceManager.DefaultBackBufferHeight)
                    //{
                    //    attackList.Remove(attack); 
                    //}
                }
            }
            //attack.UpdateAttack(gameTime, playerPosition);

            if (second % 4 == 0 && BulletCheck == true)
            {
                attack = new Attack(BulletSprite, Position, DefaultTarget);
                attackList.Add(attack);
                BulletCheck = false;

            }
            if (second % 4 != 0 && !BulletCheck)
            {
                BulletCheck = true;
            }
        }
        private void Move(GameTime gameTime, Vector2 playerPosition)
        {
            // Calculate direction towards the player
            Vector2 direction = Vector2.Normalize(playerPosition - Position);
            // Move the enemy towards the player
            Vector2 newPosition = Position + direction * movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            // Ensure the enemy stays within the game screen
            newPosition.X = Math.Clamp(newPosition.X, 0, GraphicsDeviceManager.DefaultBackBufferWidth - sprite.Width);
            newPosition.Y = Math.Clamp(newPosition.Y, 0, GraphicsDeviceManager.DefaultBackBufferHeight - sprite.Height);
            // Update the position
            Position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            if (!isActive) return;
            spriteBatch.Draw(sprite, Position, Color.White);
            foreach (Attack attack in attackList)
            {
                attack.Draw(spriteBatch);
            }


        }

        public void Deactivate() // Method to deactivate the enemy
        {
            isActive = false;
        }
        //public void PerformAttack(Vector2 targetPosition)
        //{
        //    DateTime gameTime = DateTime.Now;
        //    int second = gameTime.Second;
        //    if (second%2==0)
        //    {
        //        attack.Fire(targetPosition);

        //    }
        //}
    }
}

