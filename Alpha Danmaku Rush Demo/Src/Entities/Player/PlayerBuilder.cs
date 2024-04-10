using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Player;

public class PlayerBuilder
{
    private Texture2D _sprite;
    private Vector2 _position;
    private List<Func<GameObject, GameObject>> _decorators = new List<Func<GameObject, GameObject>>();

    public PlayerBuilder SetSprite(Texture2D sprite)
    {
        _sprite = sprite;
        return this;
    }

    public PlayerBuilder SetPosition(Vector2 position)
    {
        _position = position;
        return this;
    }

    public PlayerBuilder WithMovement(float speed)
    {
        _decorators.Add(player => new PlayerMovementDecorator(player, speed));
        return this;
    }

    public PlayerBuilder WithExtraHealth(int extraHealth)
    {
        _decorators.Add(player => new PlayerHealthDecorator(player, extraHealth));
        return this;
    }

    public GameObject Build()
    {
        GameObject player = new Player(_sprite, _position);
        foreach (var decorator in _decorators)
        {
            player = decorator(player);
        }
        return player;
    }
}
