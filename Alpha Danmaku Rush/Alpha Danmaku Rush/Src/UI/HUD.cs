namespace Alpha_Danmaku_Rush.Src.UI;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

public class HUD
{
    private SpriteFont font;
    private int score;
    private int lives;

    public HUD()
    {
        score = 0;
        lives = 3; // Starting lives, for example
    }

    public void LoadContent(ContentManager content)
    {
        // Load the font
        font = content.Load<SpriteFont>("path_to_your_font");
    }

    public void Update(int score, int lives)
    {
        this.score = score;
        this.lives = lives;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(font, $"Score: {score}", new Vector2(10, 10), Color.White);
        spriteBatch.DrawString(font, $"Lives: {lives}", new Vector2(10, 30), Color.White);

        // You can also draw graphics for lives, power-ups, etc. here
    }
}



/*
 *public class YourGame : Game
   {
   // ... other fields ...
   
   private HUD hud;
   
   protected override void LoadContent()
   {
   // ... other loading code ...
   
   hud = new HUD();
   hud.LoadContent(Content);
   }
   
   protected override void Update(GameTime gameTime)
   {
   // ... other update code ...
   
   // Update the HUD (pass in the actual score and lives)
   hud.Update(actualScore, actualLives);
   }
   
   protected override void Draw(GameTime gameTime)
   {
   GraphicsDevice.Clear(Color.Black);
   
   spriteBatch.Begin();
   
   // ... other draw code ...
   
   hud.Draw(spriteBatch);
   
   spriteBatch.End();
   
   base.Draw(gameTime);
   }
   }
   
 *
 *
 *
 */