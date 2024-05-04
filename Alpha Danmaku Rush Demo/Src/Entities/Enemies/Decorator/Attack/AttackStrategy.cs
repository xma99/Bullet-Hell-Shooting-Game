using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha_Danmaku_Rush_Demo.Src.Managers.Level;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies.Decorator.Attack
{
    public interface AttackStrategy
    {
        
        void attackstrategy(List<Bullet.Bullet> bullets, Vector2 playerPosition, GameTime gameTime,SpriteBatch spriteBatch=null);
    }
}
