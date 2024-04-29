using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies.Decorator.Attack
{
    public class AttackAction
    {
        StrategyManager _strategyManager;

        public AttackAction(StrategyManager strategyManager)
        {
            _strategyManager = strategyManager;
        }
        public void performAttack(GameTime gameTime)
        {
            _strategyManager.attackstrategy(gameTime);
        }
    }
}
