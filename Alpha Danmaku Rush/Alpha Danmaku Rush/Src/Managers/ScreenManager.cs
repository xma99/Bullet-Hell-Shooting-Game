namespace Alpha_Danmaku_Rush.Src.Managers;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

public class ScreenManager
{
    private static ScreenManager instance;
    private Stack<GameScreen> screens = new Stack<GameScreen>();

    public static ScreenManager Instance
    {
        get
        {
            if (instance == null)
                instance = new ScreenManager();

            return instance;
        }
    }

    // Constructor
    private ScreenManager()
    {
    }

    // Load content for the screen
    public void LoadContent(ContentManager content)
    {
        foreach (var screen in screens)
        {
            screen.LoadContent(content);
        }
    }

    // Update the current screen
    public void Update(GameTime gameTime)
    {
        if (screens.Count > 0)
            screens.Peek().Update(gameTime);
    }

    // Draw the current screen
    public void Draw(SpriteBatch spriteBatch)
    {
        if (screens.Count > 0)
            screens.Peek().Draw(spriteBatch);
    }

    // Add a new screen
    public void AddScreen(GameScreen screen)
    {
        screens.Push(screen);
    }

    // Remove the current screen
    public void RemoveScreen()
    {
        if (screens.Count > 0)
        {
            var screen = screens.Pop();
            screen.UnloadContent();
        }
    }
}

// Abstract class representing a general game screen
public abstract class GameScreen
{
    public abstract void LoadContent(ContentManager content);
    public abstract void Update(GameTime gameTime);
    public abstract void Draw(SpriteBatch spriteBatch);
    public abstract void UnloadContent();
}
