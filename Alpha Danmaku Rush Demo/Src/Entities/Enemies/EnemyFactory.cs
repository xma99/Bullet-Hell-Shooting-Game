using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies;

public static class EnemyFactory
{
    public static Enemy CreateEnemy(EnemyType type, Texture2D sprite, Vector2 startPosition, float movementSpeed, Texture2D bulletSprite)
    {
        switch (type)
        {
            case EnemyType.RegularA:
                return new RegularAEnemy(sprite, startPosition, movementSpeed, bulletSprite);
            case EnemyType.RegularB:
                return new RegularBEnemy(sprite, startPosition, movementSpeed, bulletSprite);
            case EnemyType.MidBoss:
                return new MidBossEnemy(sprite, startPosition, movementSpeed, bulletSprite);
            case EnemyType.FinalBoss:
                return new FinalBossEnemy(sprite, startPosition, movementSpeed, bulletSprite);
            default:
                throw new ArgumentOutOfRangeException(nameof(type), $"Not implemented type: {type}");
        }
    }
}
