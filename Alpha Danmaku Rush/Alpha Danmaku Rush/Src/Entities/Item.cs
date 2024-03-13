namespace Alpha_Danmaku_Rush.Src.Entities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Item
{
    public Texture2D Texture { get; private set; } // The texture for the item
    public Vector2 Position { get; private set; } // The position of the item in the game world
    public Rectangle Hitbox => new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);

    public bool IsActive { get; set; } // Determines if the item is active and can be interacted with

    // Item specific properties
    public string ItemType { get; private set; } // Type of the item (e.g., "PowerUp", "Score", "Health", etc.)
    public float EffectValue { get; private set; } // The effect value (e.g., amount of health restored, score value, etc.)

    public Item(Texture2D texture, Vector2 position, string itemType, float effectValue)
    {
        Texture = texture;
        Position = position;
        ItemType = itemType;
        EffectValue = effectValue;
        IsActive = true;
    }

    public void Update(GameTime gameTime)
    {
        // Update item logic here (e.g., movement, collision detection)

        // Example: You could make the item float up and down or spin
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (IsActive)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }

    // Collision detection with the player or other entities can be handled here
    public void OnPlayerCollision(Player player)
    {
        // Activate item effect based on its type
        switch (ItemType)
        {
            case "PowerUp":
                player.PowerUp(EffectValue);
                break;
            case "Score":
                player.AddScore((int)EffectValue);
                break;
            case "Health":
                player.RestoreHealth(EffectValue);
                break;
                // Add other cases as needed
        }

        // Deactivate the item after being collected
        IsActive = false;
    }
}

