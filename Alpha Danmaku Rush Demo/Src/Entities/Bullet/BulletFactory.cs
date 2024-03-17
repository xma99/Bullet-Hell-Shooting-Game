using System;
using Alpha_Danmaku_Rush_Demo.Src.Managers.Level;
using Alpha_Danmaku_Rush_Demo.Src.Utils;
using Microsoft.Xna.Framework;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Bullet;

public static class BulletFactory
{
    // A method to create bullets based on type, speed, and other attributes
    public static Bullet CreateBullet(Vector2 position, Vector2 velocity, EnemyBulletType type)
    {
        return type.Type switch
        {
            "A" => new BulletA(position, AdjustVelocity(velocity, type.Speed), ColorHelper.FromName(type.Color)), // Assume speed 10 for BulletA
            "B" => new BulletB(position, AdjustVelocity(velocity, type.Speed), ColorHelper.FromName(type.Color)), // Assume speed 5 for BulletB
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