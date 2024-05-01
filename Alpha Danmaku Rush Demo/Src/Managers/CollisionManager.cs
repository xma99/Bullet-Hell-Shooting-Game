using Alpha_Danmaku_Rush_Demo.Src.Entities.Bullet;
using Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies;
using System.Collections.Generic;
using System.Linq;
using Alpha_Danmaku_Rush_Demo.Src.Entities.Player;
using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Media;

namespace Alpha_Danmaku_Rush_Demo.Src.Managers;

public class CollisionManager
{
    private IPlayer player;
    private List<Bullet> bullets;
    private EnemyManager enemies;
    private ScoreManager score;
    private SoundManager sound;

    public CollisionManager(IPlayer player, EnemyManager enemies, ScoreManager score, SoundManager sound)
    {
        this.player = player;
        this.enemies = enemies;
        this.score = score;
        this.sound = sound;
    }

    public void Update()
    {
        CheckEnemyBulletPlayerCollisions();
        CheckEnemyPlayerCollisions();
        CheckPlayerBulletEnemyCollisions();
        // SetBomb();
    }

    // private void SetBomb()
    // {
    //     if (direction.IsKeyDown(Keys.B))
    //     {
    //         sound.PlaySound("bomb");
    //         foreach (var enemy in enemies.enemies.Where(enemy => enemy.IsActive))
    //         {
    //             enemy.IsActive = false;
    //             score.AddScoreForEnemyKill();
    //         }
    //     }
    // }

    private void CheckEnemyBulletPlayerCollisions()
    {
        if (!player.IsInvincible)
        {
            foreach (var enemy in enemies.enemies.Where(enemy => enemy.IsActive))
            {
                foreach (var bullet in enemy.bulletList.Where(bullet => bullet.IsActive && bullet.BoundingBox.Intersects(player.BoundingBox)))
                {
                    sound.PlaySound("playerHit");
                    player.Health -= bullet.Damage;
                    bullet.IsActive = false;

                    // Check if the player's health is still greater than 0
                    if (player.Health > 0)
                    {
                        // Call the Respawn method of the player when health reaches zero
                        player.Respawn();
                    }
                }
            }
        }
    }

    private void CheckEnemyPlayerCollisions()
    {
        // Check if the player is currently invincible
        if (!player.IsInvincible)
        {
            // Iterate through all active enemies
            foreach (var enemy in enemies.enemies.Where(enemy => enemy.BoundingBox.Intersects(player.BoundingBox) && enemy.IsActive))
            {
                sound.PlaySound("playerHit");
                // Inflict damage to the player
                player.Health -= 1;
                // Disable the enemy
                enemy.IsActive = false;

                // Check if the player's health is still greater than 0
                if (player.Health > 0)
                {
                    // Call the Respawn method of the player when health reaches zero
                    player.Respawn();
                }
            }
        }
    }

    private void CheckPlayerBulletEnemyCollisions()
    {
        Bullet playerBullet = player.GetBullet();
        if (playerBullet != null)
        {
            foreach (var enemy in enemies.enemies.Where(enemy => enemy.IsActive))
            {
                if (playerBullet.BoundingBox.Intersects(enemy.BoundingBox))
                {
                    sound.PlaySound("playerHit");
                    enemy.IsActive = false;
                    playerBullet.IsActive = false;

                    score.AddScoreForEnemyKill();
                    break;
                }
            }
        }
    }
}