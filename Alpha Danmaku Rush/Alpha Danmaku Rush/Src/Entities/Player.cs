namespace Alpha_Danmaku_Rush.Src.Entities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

public class Player
{
    public Texture2D Texture { get; private set; } // 玩家纹理
    public Vector2 Position { get; set; } // 玩家位置
    private Vector2 _velocity; // 玩家移动速度
    private const float Speed = 2f; // 玩家移动速度常数
    public Vector2 Size { get; set; } // 玩家大小

    public Player()
    {
        Position = Vector2.Zero;
        _velocity = Vector2.Zero;
        Size = new Vector2(64, 64);
    }

    // 加载纹理
    public void LoadContent(Texture2D texture)
    {
        Texture = texture;
    }

    // 更新玩家状态
    public void Update(GameTime gameTime)
    {
        _velocity = Vector2.Zero;

        // 玩家控制
        if (Keyboard.GetState().IsKeyDown(Keys.Up))
            _velocity.Y = -Speed;
        if (Keyboard.GetState().IsKeyDown(Keys.Down))
            _velocity.Y = Speed;
        if (Keyboard.GetState().IsKeyDown(Keys.Left))
            _velocity.X = -Speed;
        if (Keyboard.GetState().IsKeyDown(Keys.Right))
            _velocity.X = Speed;

        Position += _velocity;
    }

    // 绘制玩家
    public void Draw(SpriteBatch spriteBatch)
    {
        if (Texture != null)
        {
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            spriteBatch.Draw(Texture, destinationRectangle, Color.White);
        }
    }
}

