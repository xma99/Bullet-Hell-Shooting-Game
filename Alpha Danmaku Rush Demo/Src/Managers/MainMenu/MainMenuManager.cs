using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;

namespace Alpha_Danmaku_Rush_Demo.Src.Managers.MainMenu;

public class MainMenuManager
{
    private Texture2D menuImg;
    private Texture2D startButton;
    private Texture2D endButton;
    private Rectangle startButtonSet;
    private Rectangle endButtonSet;
    private bool gameStartCheck;
    private bool startClickCheck;
    private MouseState previousMouseState;

    public MainMenuManager(ContentManager content)
    {
        menuImg = content.Load<Texture2D>("menu");
        startButton = content.Load<Texture2D>("start");
        endButton = content.Load<Texture2D>("end");
        gameStartCheck = true;
        startClickCheck = false;
        startButtonSet = new Rectangle(400, 500, startButton.Width, startButton.Height);
        endButtonSet = new Rectangle(400, 550, endButton.Width, endButton.Height);
        previousMouseState = Mouse.GetState();
    }

    public void Update(GameTime gameTime)
    {
        MouseState currentMouseState = Mouse.GetState();

        if (gameStartCheck && currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
        {
            // Check that the mouse click is on the “Start” button
            if (startButtonSet.Contains(currentMouseState.Position))
            {
                startClickCheck = true;
                gameStartCheck = false;
            }
            // Check that the mouse click is on the “Exit” button 
            else if (endButtonSet.Contains(currentMouseState.Position))
            {
                Environment.Exit(0);
            }
        }

        previousMouseState = currentMouseState;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (gameStartCheck)
        {
            spriteBatch.Draw(menuImg, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(startButton, startButtonSet, Color.White);
            spriteBatch.Draw(endButton, endButtonSet, Color.White);
        }
    }

    public bool IsClick()
    {
        return startClickCheck;
    }

    public bool IsStart()
    {
        return gameStartCheck;
    }

    internal void Update(object gameTime)
    {
        throw new NotImplementedException();
    }
}