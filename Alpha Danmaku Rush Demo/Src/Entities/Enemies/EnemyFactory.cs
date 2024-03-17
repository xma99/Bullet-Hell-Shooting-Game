using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies;

public static class EnemyFactory
{
    public static Enemy CreateEnemy(EnemyType type, Texture2D sprite, Vector2 startPosition, float movementSpeed, Texture2D bulletSprite)
    {
        return type switch
        {
            EnemyType.RegularA => new RegularAEnemy(sprite, startPosition, movementSpeed, bulletSprite),
            EnemyType.RegularB => new RegularBEnemy(sprite, startPosition, movementSpeed, bulletSprite),
            EnemyType.MidBoss => new MidBossEnemy(sprite, startPosition, movementSpeed, bulletSprite),
            EnemyType.FinalBoss => new FinalBossEnemy(sprite, startPosition, movementSpeed, bulletSprite),
            _ => throw new ArgumentOutOfRangeException(nameof(type), $"Not implemented type: {type}"),
        };
    }
}
