using Alpha_Danmaku_Rush_Demo.Src.UI;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Alpha_Danmaku_Rush_Demo.Src.Managers;

public class UIManager
{
    private List<HealthIcon> _healthIcons = new List<HealthIcon>();
    private ContentManager _content;
    private GraphicsDeviceManager _graphics;

    public UIManager(ContentManager content, GraphicsDeviceManager graphics)
    {
        _content = content;
        _graphics = graphics;
    }

    public void InitializeHealthIcons(int healthCount)
    {
        Texture2D heartTexture = _content.Load<Texture2D>("heart");
        float scale = 0.03f;
        int iconSpacing = 1;
        int iconWidth = (int)(heartTexture.Width * scale);
        int startPositionX = _graphics.PreferredBackBufferWidth - (iconWidth + iconSpacing) * healthCount;

        _healthIcons.Clear();
        for (int i = 0; i < healthCount; i++)
        {
            Vector2 iconPosition = new Vector2(startPositionX + (iconWidth + iconSpacing) * i, 20);
            _healthIcons.Add(new HealthIcon(heartTexture, iconPosition, scale, isActive: true));
        }
    }

    public void UpdateHealthIcons(int currentHealth)
    {
        for (int i = 0; i < _healthIcons.Count; i++)
        {
            _healthIcons[i].IsActive = i < currentHealth;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var icon in _healthIcons)
        {
            icon.Draw(spriteBatch);
        }
    }
}