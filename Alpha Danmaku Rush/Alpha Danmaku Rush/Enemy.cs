namespace Alpha_Danmaku_Rush;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Enemy
{
    // Fields for enemy properties
    public Vector2 Position;
    private Texture2D sprite;
    private float movementSpeed;
    private bool isActive;

    // Enemy attack pattern properties
    private float attackCooldown;
    private float currentCooldown;

    public object BoundingBox { get; internal set; }

    public Enemy(Texture2D sprite, Vector2 startPosition, float movementSpeed)
    {
        this.sprite = sprite;
        this.Position = startPosition;
        this.movementSpeed = movementSpeed;
        this.isActive = true;
        this.attackCooldown = 1.0f; // Time in seconds between attacks
        this.currentCooldown = 0;
    }

    public void Update(GameTime gameTime)
    {
        if (!isActive) return;

        // Update enemy movement
        Move();

        // Update attack pattern
        UpdateAttack(gameTime);
    }

    private void Move()
    {
        // Basic movement logic here
        // For example, simple vertical movement:
        Position.Y += movementSpeed;
    }

    private void UpdateAttack(GameTime gameTime)
    {
        currentCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (currentCooldown <= 0)
        {
            // Reset cooldown
            currentCooldown = attackCooldown;

            // Execute attack pattern
            Shoot();
        }
    }

    private void Shoot()
    {
        // Implement bullet shooting logic here
        // E.g., create bullets and set their direction and speed
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!isActive) return;

        spriteBatch.Draw(sprite, Position, Color.White);
    }
}
