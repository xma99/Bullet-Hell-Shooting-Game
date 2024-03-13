namespace Alpha_Danmaku_Rush.Src.Managers;

using Alpha_Danmaku_Rush.Src.Entities;
using Alpha_Danmaku_Rush.Src.Entities.Enemy;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

public class CollisionManager
{
    private Player player;
    private List<Bullet> bullets;
    private List<Enemy> enemies;

    public CollisionManager(Player player, List<Enemy> enemies)
    {
        this.player = player;
        this.enemies = enemies;
    }


    public void Update()
    {
        CheckBulletEnemyCollisions();
        CheckEnemyPlayerCollisions();
    }

    private void CheckBulletEnemyCollisions()
    {
        // 使用 player.Bullets 来获取子弹列表
        for (int i = player.Bullets.Count - 1; i >= 0; i--)
        {
            for (int j = enemies.Count - 1; j >= 0; j--)
            {
                if (player.Bullets[i].IsActive && enemies[j].IsActive &&
                    player.Bullets[i].BoundingBox.Intersects(enemies[j].BoundingBox))
                {
                    // 子弹和敌人相撞
                    player.Bullets[i].IsActive = false; // 使子弹消失
                    enemies[j].IsActive = false; // 使敌人消失
                }
            }
        }
    }

    private void CheckEnemyPlayerCollisions()
    {
        foreach (var enemy in enemies)
        {
            if (enemy.IsActive && enemy.BoundingBox.Intersects(player.BoundingBox))
            {
                // 敌人和玩家相撞
                enemy.IsActive = false; // 使敌人消失
            }
        }
    }
}
