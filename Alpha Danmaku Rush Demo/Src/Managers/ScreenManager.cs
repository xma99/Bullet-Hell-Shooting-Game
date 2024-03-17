using Alpha_Danmaku_Rush_Demo.Src.Managers.MainMenu;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Alpha_Danmaku_Rush_Demo.Src.Managers;

public class ScreenManager
{
    private enum ScreenState
    {
        MainMenu,
        GameLevel,
        Settings
    }

    private ScreenState _currentState;
    private MainMenuManager _mainMenuManager;
    private LevelManager _levelManager;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private ContentManager _content;

    public ScreenManager(ContentManager content, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
    {
        _content = content;
        _graphics = graphics;
        _spriteBatch = spriteBatch;

        _mainMenuManager = new MainMenuManager(content);
        _levelManager = new LevelManager(content, graphics, spriteBatch);

        _currentState = ScreenState.MainMenu; // Start game in main menu
    }

    public void Update(GameTime gameTime)
    {
        switch (_currentState)
        {
            case ScreenState.MainMenu:
                // Here, you'd handle main menu updates and option selections
                // E.g., if New Game is selected, set _currentState to GameLevel
                break;
            case ScreenState.GameLevel:
                _levelManager.Update(gameTime);
                break;
            case ScreenState.Settings:
                // Handle settings updates
                break;
        }
    }

    public void Draw()
    {
        _spriteBatch.Begin();

        switch (_currentState)
        {
            case ScreenState.MainMenu:
                _mainMenuManager.Draw(_spriteBatch);
                break;
            case ScreenState.GameLevel:
                _levelManager.Draw();
                break;
            case ScreenState.Settings:
                // Draw settings screen
                break;
        }

        _spriteBatch.End();
    }

    public void LoadLevel(string levelPath)
    {
        _levelManager.LoadLevel(levelPath);
        _currentState = ScreenState.GameLevel;
    }
}