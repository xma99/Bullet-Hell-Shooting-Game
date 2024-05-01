using Alpha_Danmaku_Rush_Demo.Src.Entities.Bullet;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha_Danmaku_Rush_Demo.Src.Utils;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Content;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Player
{
    public class PlayerAttackDecorator : IPlayer
    {
        private IPlayer _wrappedPlayer;
        private float _attackSpeed = 1.0f;
        private bool _isAttacking = false;
        private ContentManager content;
        private Bullet.Bullet newBullet;

        public PlayerAttackDecorator(IPlayer player, float attackSpeed)
        {
            _wrappedPlayer = player;
            _attackSpeed = attackSpeed;
        }

        public Vector2 Position
        {
            get => _wrappedPlayer.Position;
            set => _wrappedPlayer.Position = value;
        }

        public Texture2D Sprite => _wrappedPlayer.Sprite;

        public Rectangle BoundingBox => _wrappedPlayer.BoundingBox;

        public int Health { get => _wrappedPlayer.Health; set => _wrappedPlayer.Health = value; }

        public bool IsInvincible => _wrappedPlayer.IsInvincible;

        public void Update(GameTime gameTime, int screenWidth)
        {
            HandleAttack(gameTime);
            _wrappedPlayer.Update(gameTime, screenWidth);
        }

        private void HandleAttack(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Space))
            {
                PerformAttack();
            }

            if (newBullet != null)
            {
                if (newBullet.IsActive == false)
                {
                    _isAttacking = false;
                }
                newBullet.Update(gameTime);
            }
            else
            {
                _isAttacking = false;
            }
        }

        private void PerformAttack()
        {
            if (_isAttacking)
            {
                return;
            }

            // Example: Trigger attack logic
            // This could be creating bullets, playing an attack animation, etc.
            _isAttacking = true;
            // If you have a method to actually "fire", call it here
            // e.g., FireBullet();
            // Reset attacking state if needed

            newBullet = new PlayerBullet(content.Load<Texture2D>("bubble"), Position, BulletFactory.AdjustVelocity(new Vector2(0, 1), -10), ColorHelper.FromName("yellow"));
            newBullet.Speed = 10;
        }

        public void SetContent(ContentManager content)
        {
            this.content = content;
        }

        public Bullet.Bullet GetBullet()
        {
            return _isAttacking ? newBullet : null;
        }


        public void Respawn()
        {
            _wrappedPlayer.Respawn();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _wrappedPlayer.Draw(spriteBatch);
            if (_isAttacking)
            {
                newBullet.Draw(spriteBatch);
            }
        }
    }
}
