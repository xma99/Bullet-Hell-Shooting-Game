using Alpha_Danmaku_Rush_Demo.Src.Entities.Bullet;
using Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies;
using System.Collections.Generic;
using System.Linq;
using Alpha_Danmaku_Rush_Demo.Src.Entities.Player;

namespace Alpha_Danmaku_Rush_Demo.Src.Managers;

public class CollisionManager
{
    private IPlayer player;
    private List<Bullet> bullets;
    private EnemyManager enemies;
    private ScoreManager score;

    public CollisionManager(IPlayer player, EnemyManager enemies, ScoreManager score)
    {
        this.player = player;
        this.enemies = enemies;
        this.score = score;
    }

    public void Update()
    {
        CheckEnemyBulletPlayerCollisions();
        CheckEnemyPlayerCollisions();
    }

    private void CheckEnemyBulletPlayerCollisions()
    {
        foreach (var bullet in enemies.enemies.SelectMany(enemy => enemy.bulletList.Where(bullet => bullet.BoundingBox.Intersects(player.BoundingBox) && bullet.IsActive)))
        {
            player.Health -= bullet.Damage;
            bullet.IsActive = false;
        }
    }


    private void CheckEnemyPlayerCollisions()
    {
        foreach (var enemy in enemies.enemies.Where(enemy =>
                     enemy.BoundingBox.Intersects(player.BoundingBox) && enemy.IsActive))
        {
            player.Health -= 1;
            enemy.Deactivate();
        }
    }
}