using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies;

public static class EnemyFactory
{
    public static Enemy CreateEnemy(ContentManager content, EnemyType type, Vector2 startPosition, float movementSpeed)
    {
        return type switch
        {
            EnemyType.RegularA => new RegularAEnemy(content, startPosition, movementSpeed),
            EnemyType.RegularB => new RegularBEnemy(content, startPosition, movementSpeed),
            EnemyType.MidBoss => new MidBossEnemy(content, startPosition, movementSpeed),
            EnemyType.FinalBoss => new FinalBossEnemy(content, startPosition, movementSpeed),
            _ => throw new ArgumentOutOfRangeException(nameof(type), $"Not implemented type: {type}"),
        };
    }
}
