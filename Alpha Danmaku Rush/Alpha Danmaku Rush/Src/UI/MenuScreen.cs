using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha_Danmaku_Rush.Src.UI
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class MenuScreen
    {
        private List<string> menuItems;
        private int selectedIndex;
        private SpriteFont font;
        private Vector2 position;
        private Color selectedColor = Color.Red;
        private Color normalColor = Color.White;

        public MenuScreen(SpriteFont font)
        {
            this.font = font;
            menuItems = new List<string>() { "Play Game", "Settings", "Exit" };
            selectedIndex = 0;
            position = new Vector2(100, 100); // 初始菜单项位置
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && selectedIndex < menuItems.Count - 1)
            {
                selectedIndex++;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up) && selectedIndex > 0)
            {
                selectedIndex--;
            }

            // 这里可以添加选择菜单项时的逻辑，例如按Enter键
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 tempPosition = position;

            for (int i = 0; i < menuItems.Count; i++)
            {
                Color color = (i == selectedIndex) ? selectedColor : normalColor;
                spriteBatch.DrawString(font, menuItems[i], tempPosition, color);
                tempPosition.Y += font.LineSpacing + 5; // 下一个菜单项的位置
            }
        }
    }
}
