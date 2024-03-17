using System;
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
    private SettingsManager _settingsManager;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private ContentManager _content;

    private bool _isGameStarted;
    private int _currentLevel;

    public ScreenManager(ContentManager content, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
    {
        _content = content;
        _graphics = graphics;
        _spriteBatch = spriteBatch;

        _mainMenuManager = new MainMenuManager(content);
        _levelManager = new LevelManager(content, graphics, spriteBatch);
        _currentState = ScreenState.MainMenu; // Start game in main menu


        // Debugging
        _currentState = ScreenState.GameLevel;  // debug to start game in level
        _currentLevel = 1; // debug to start game in level 1
        _isGameStarted = false; // debug to start game in level 1

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
            default:
                throw new ArgumentOutOfRangeException();
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
                if (!_isGameStarted)
                {
                    _levelManager.LoadLevel("/Save/Levels/level"+ _currentLevel + ".json", _currentLevel);
                    _isGameStarted = true;
                }
                _levelManager.Draw();
                break;
            case ScreenState.Settings:
                // Draw settings screen
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        _spriteBatch.End();
    }

    public void LoadLevel(string levelPath, int levelNumber)
    {
        _currentLevel = levelNumber;
        _levelManager.LoadLevel(levelPath, levelNumber);
        _currentState = ScreenState.GameLevel;
    }
}