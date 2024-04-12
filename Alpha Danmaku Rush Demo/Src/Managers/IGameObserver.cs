using Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies;

namespace Alpha_Danmaku_Rush_Demo.Src.Managers;

public interface IGameObserver
{
    void OnEnemyKilled(IEnemy enemy);
    void OnHealthChanged(int currentHealth);
}
