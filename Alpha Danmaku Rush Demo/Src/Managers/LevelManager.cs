using Alpha_Danmaku_Rush_Demo.Src.Entities;
using Alpha_Danmaku_Rush_Demo.Src.UI;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies;
using Alpha_Danmaku_Rush_Demo.Src.Managers.Level;
using System.Text.Json;
using System.IO;

namespace Alpha_Danmaku_Rush_Demo.Src.Managers;

public class LevelManager
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private ContentManager _content;
    private EnemyManager _enemyManager;
    private Player _player;
    private CollisionManager _collisionManager;
    private ScoreManager _scoreManager;


    private TimeSpan _spawnIntervalMin;
    private TimeSpan _spawnIntervalMax;
    private Random _random = new Random();

    // Existing fields and methods...
    private UIManager _uiManager;

    private bool _isGameStarted;
    private int _currentLevel;

    public LevelManager(ContentManager content, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
    {
        InitializePlayer();
        _content = content;
        _graphics = graphics;
        _spriteBatch = spriteBatch;
        _enemyManager = new EnemyManager(content, graphics);
        _collisionManager = new CollisionManager(_player, _enemyManager, _scoreManager);

        _uiManager.InitializeHealthIcons(_player.Health);
    }

    private void InitializePlayer()
    {
        Texture2D playerTexture = _content.Load<Texture2D>("testplayer1");
        Vector2 initialPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2 - playerTexture.Width / 2, _graphics.PreferredBackBufferHeight - playerTexture.Height);
        _player = new Player(playerTexture, initialPosition);
    }

    public void Update(GameTime gameTime)
    {
        _player.Update(gameTime, _graphics.GraphicsDevice.Viewport.Width);
        // Here you would handle the logic for updating the level state, spawning enemies, etc.
        // Example: Update health icons based on player's health
        _uiManager.UpdateHealthIcons(_player.Health);
        _collisionManager.Update();
        _scoreManager.Update(gameTime);
    }

    public void Draw()
    {
        _spriteBatch.Begin();
        // Background, player, enemies, and health icons drawing logic
        _player.Draw(_spriteBatch);
        _enemyManager.Draw(_spriteBatch);
        _uiManager.Draw(_spriteBatch); // Draw UI elements
        _spriteBatch.End();
    }

    public void LoadLevel(string filePath, int levelNumber)
    {
        // Reset the current level state
        _enemyManager.Clear();
        ResetPlayerPosition(); // Implement this method as needed

        _currentLevel = levelNumber;
        string jsonString = File.ReadAllText(filePath);
        LevelData levelData = JsonSerializer.Deserialize<LevelData>(jsonString);

        foreach (var wave in levelData.Waves)
        {
            // Assuming you have a method to parse the enemy type and create an enemy
            for (int i = 0; i < wave.EnemyAmount; i++)
            {
                // Here we'll need to translate string enemy types to our enum or class types
                EnemyType enemyType = ParseEnemyType(wave.EnemyType);

                // Placeholder for spawning logic; replace with your actual implementation
                SpawnEnemy(enemyType, wave.EnemyBulletType);
            }
        }
    }

    private void ResetPlayerPosition() =>
        _player.Position = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight - _player.Sprite.Height);

    private EnemyType ParseEnemyType(string type)
    {
        // Implement parsing logic based on your EnemyType enum or classes
        // This is a basic example, extend it according to your needs
        return type switch
        {
            "RegularA" => EnemyType.RegularA,
            "RegularB" => EnemyType.RegularB,
            "MidBoss" => EnemyType.MidBoss,
            "FinalBoss" => EnemyType.FinalBoss,
            _ => throw new ArgumentOutOfRangeException(nameof(type), $"Not expected enemy type value: {type}"),
        };
    }

    private void SpawnEnemy(EnemyType enemyType, EnemyBulletType bulletType)
    {
        // Your logic to spawn an enemy based on type and bullet configuration
        switch (enemyType)
        {
            case EnemyType.RegularA:
                _enemyManager.SpawnEnemyA(bulletType);
                break;
            case EnemyType.RegularB:
                _enemyManager.SpawnEnemyB(bulletType);
                break;
            case EnemyType.MidBoss:
                _enemyManager.SpawnEnemyM(bulletType);
                break;
            case EnemyType.FinalBoss:
                _enemyManager.SpawnEnemyF(bulletType);
                break;
            default: ;
                break;
        }
    }
}