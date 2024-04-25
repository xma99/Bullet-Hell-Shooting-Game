using Alpha_Danmaku_Rush_Demo.Src.Managers.Level;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies.Decorator.Attack
{
    public class AttackCaller
    {
        public AttackStrategy _attackStrategy;

        public AttackCaller(AttackStrategy attackStrategy)
        {
            _attackStrategy = attackStrategy;
        }
        public void performAttack(List<Bullet.Bullet> bullets, Vector2 playerPosition, GameTime gameTime)
        {
            _attackStrategy.attackstrategy(bullets, playerPosition, gameTime);
        }
    }
}
