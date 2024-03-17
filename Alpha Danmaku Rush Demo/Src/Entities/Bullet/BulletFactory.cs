using System;
using Alpha_Danmaku_Rush_Demo.Src.Managers.Level;
using Alpha_Danmaku_Rush_Demo.Src.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Bullet;

public static class BulletFactory
{
    // A method to create bullets based on type, speed, and other attributes
    public static Bullet CreateBullet(ContentManager content, Vector2 position, Vector2 velocity, EnemyBulletType type)
    {
        return type.Type switch
        {
            "A" => new BulletA(content.Load<Texture2D>("bullettest1"), position, AdjustVelocity(velocity, type.Speed), ColorHelper.FromName(type.Color)),
            "B" => new BulletB(content.Load<Texture2D>("bullettest1"), position, AdjustVelocity(velocity, type.Speed), ColorHelper.FromName(type.Color)),
            _ => throw new ArgumentException($"Unsupported bullet type: {type}")
        };
    }

    // Adjusts the velocity vector based on the desired speed
    private static Vector2 AdjustVelocity(Vector2 originalVelocity, int speed)
    {
        // Normalize the velocity vector to get direction, then scale by speed
        if (originalVelocity != Vector2.Zero)
        {
            return Vector2.Normalize(originalVelocity) * speed;
        }
        return originalVelocity;
    }
}