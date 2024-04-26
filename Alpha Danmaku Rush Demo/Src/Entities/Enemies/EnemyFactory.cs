using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Alpha_Danmaku_Rush_Demo.Src.Managers.Level;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies;

public static class EnemyFactory
{
    public static IEnemy CreateEnemy(ContentManager content, EnemyType type, Vector2 startPosition, float movementSpeed,EnemyBulletType bulletType)
    {
        Texture2D sprite;
        EnemyBuilder builder = new EnemyBuilder(content, startPosition, movementSpeed, type, bulletType)
            .SetPosition(startPosition);

        // Apply type-specific configurations
        switch (type)
        {
            case EnemyType.RegularA:
                sprite = content.Load<Texture2D>("a");
                builder.SetSprite(sprite);
                //builder.WithAggressiveAttack();  // Example: RegularA has an aggressive attack
                break;
            case EnemyType.RegularB:
                sprite = content.Load<Texture2D>("b");
                builder.SetSprite(sprite);
                // Possibly no additional decorators for RegularB
               // builder.WithAggressiveAttack();
                break;
            case EnemyType.MidBoss:
                sprite = content.Load<Texture2D>("midBoss");
                builder.SetSprite(sprite);
                builder.WithMovement(1.5f * movementSpeed);  // MidBoss has increased speed
                break;
            case EnemyType.FinalBoss:
                sprite = content.Load<Texture2D>("finalBoss");
                builder.SetSprite(sprite);
                builder.WithMovement(2.0f * movementSpeed);   // FinalBoss has significantly increased speed
                //builder.WithAggressiveAttack();             // and also an aggressive attack
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), $"Not implemented type: {type}");
        }

        return builder.Build();
    }
}
