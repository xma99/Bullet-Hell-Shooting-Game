using System;
using Alpha_Danmaku_Rush_Demo.Src.Managers.MainMenu;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;
using System.Reflection;

namespace Alpha_Danmaku_Rush_Demo.Src.Managers;

public class ScreenManager
{
    private enum ScreenState
    {
        MainMenu,
        GameLevel,
        Settings,
        GameOver
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

    private Texture2D _gameOverTexture; // Texture for game over screen
    private const float GameOverDisplayTime = 3f; // Duration to display GameOver screen in seconds
    private float gameOverTimer = 0f;
    private bool isGameOverDisplayed = false;

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

        _gameOverTexture = _content.Load<Texture2D>("GameOver"); // Load game over texture
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
                if (_levelManager.IsGameOver()) // Check if game over
                {
                    _currentState = ScreenState.GameOver;
                    // Pause the game or take any necessary actions
                }
                else
                {
                    _levelManager.Update(gameTime);
                }
                break;
            case ScreenState.Settings:
                // Handle settings updates
                break;
            case ScreenState.GameOver:
                // Handle game over screen updates
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (_levelManager.IsGameOver() && !isGameOverDisplayed)
        {
            isGameOverDisplayed = true;
        }

        if (isGameOverDisplayed) // Update the game over timer if the GameOver screen is displayed
        {
            gameOverTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (gameOverTimer >= GameOverDisplayTime)
            {
                Environment.Exit(0); // quit the application
            }
        }
    }

    public void Draw()
    {
        switch (_currentState)
        {
            case ScreenState.MainMenu:
                _mainMenuManager.Draw(_spriteBatch);
                break;
            case ScreenState.GameLevel:
                if (!_isGameStarted)
                {
                    //debug code for vs
                    string exePath = Assembly.GetExecutingAssembly().Location;
                    string exeDirectory = Path.GetDirectoryName(exePath);

                    // Move up directories to the solution root from the executable location
                    string solutionRoot = Directory.GetParent(exeDirectory).Parent.Parent.FullName;
                    string filePath = Path.Combine(solutionRoot, "Save/Levels/level" + _currentLevel + ".json");


                    _levelManager.LoadLevel(filePath, _currentLevel);
                    _isGameStarted = true;
                }
                _levelManager.Draw();
               
                break;
            case ScreenState.Settings:
                // Draw settings screen
                break;
            case ScreenState.GameOver:
                // Draw game over screen
                Texture2D gameOverTexture = _content.Load<Texture2D>("GameOver");
                Vector2 gameOverPosition = new Vector2((_graphics.GraphicsDevice.Viewport.Width - gameOverTexture.Width) / 2, (_graphics.GraphicsDevice.Viewport.Height - gameOverTexture.Height) / 2);
                float scale = 1f;
                _spriteBatch.Draw(gameOverTexture, gameOverPosition, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void LoadLevel(string levelPath, int levelNumber)
    {
        _currentLevel = levelNumber;
        _levelManager.LoadLevel(levelPath, levelNumber);
        _currentState = ScreenState.GameLevel;
    }
}