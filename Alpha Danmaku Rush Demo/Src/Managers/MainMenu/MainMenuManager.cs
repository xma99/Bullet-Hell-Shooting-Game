using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Alpha_Danmaku_Rush_Demo.Src.Managers.MainMenu;

public class MainMenuManager
{
    private SpriteFont _menuFont;
    private int _selectedOptionIndex = 0;
    private readonly string[] _menuOptions =
    {
            "Continue Game",
            "New Game",
            "Level Selection",
            "Adjust Volume",
            "Exit"
        };

    public MainMenuManager(ContentManager content)
    {
        _menuFont = content.Load<SpriteFont>("Font"); // Ensure you have a Font.spritefont
    }

    public void Update(GameTime gameTime, KeyboardState keyboardState, KeyboardState previousKeyboardState)
    {
        if (keyboardState.IsKeyDown(Keys.Down) && previousKeyboardState.IsKeyUp(Keys.Down))
        {
            _selectedOptionIndex = (_selectedOptionIndex + 1) % _menuOptions.Length;
        }
        else if (keyboardState.IsKeyDown(Keys.Up) && previousKeyboardState.IsKeyUp(Keys.Up))
        {
            _selectedOptionIndex--;
            if (_selectedOptionIndex < 0) _selectedOptionIndex = _menuOptions.Length - 1;
        }

        // Handle option selection (e.g., Enter key)
        if (keyboardState.IsKeyDown(Keys.Enter) && previousKeyboardState.IsKeyUp(Keys.Enter))
        {
            SelectOption((MainMenuOption)_selectedOptionIndex);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();

        Vector2 position = new Vector2(100, 100); // Starting position of the menu
        for (int i = 0; i < _menuOptions.Length; i++)
        {
            Color color = i == _selectedOptionIndex ? Color.Yellow : Color.White;
            spriteBatch.DrawString(_menuFont, _menuOptions[i], position, color);
            position.Y += 50; // Move down for the next option
        }

        spriteBatch.End();
    }

    private void SelectOption(MainMenuOption option)
    {
        switch (option)
        {
            case MainMenuOption.ContinueGame:
                // Implement the logic to continue the game
                break;
            case MainMenuOption.NewGame:
                // Implement the logic to start a new game
                break;
            case MainMenuOption.LevelSelection:
                // Implement the logic to show level selection
                break;
            case MainMenuOption.AdjustVolume:
                // Implement the logic to adjust volume
                break;
            case MainMenuOption.Exit:
                // Implement the logic to exit the game
                break;
        }
    }
}