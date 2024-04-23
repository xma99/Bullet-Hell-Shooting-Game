using Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies.Decorator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Content;
using Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies.Decorator.Attack;
using Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies.Decorator.Move;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies;

public class EnemyBuilder
{
    private Texture2D _sprite;
    private Vector2 _position;
    private ContentManager _content;
    private Vector2 _startPosition;
    private EnemyType _enemyType;
    private float _movementSpeed;

    private List<Func<IEnemy, IEnemy>> _decorators = new List<Func<IEnemy, IEnemy>>();

    public EnemyBuilder(ContentManager content, Vector2 startPosition, float movementSpeed, EnemyType enemyType)
    {
        _content = content;
        _startPosition = startPosition;
        _movementSpeed = movementSpeed;
        _enemyType = enemyType;
    }

    public EnemyBuilder SetSprite(Texture2D sprite)
    {
        _sprite = sprite;
        return this;
    }

    public EnemyBuilder SetPosition(Vector2 position)
    {
        _position = position;
        return this;
    }

    public EnemyBuilder WithMovement(float speed)
    {
        _decorators.Add(enemy => new SpeedBoostDecorator(enemy));
        return this;
    }

    public EnemyBuilder WithAggressiveAttack()
    {
        _decorators.Add(enemy => new AggressiveAttackDecorator(enemy));
        return this;
    }

    public IEnemy Build()
    {
        IEnemy enemy = new Enemy(_content, _startPosition, _movementSpeed, _enemyType);
        foreach (var decorator in _decorators)
        {
            enemy = decorator(enemy);
        }
        return enemy;
    }
}
