namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Player;

public class PlayerHealthDecorator : GameObject
{
    private GameObject wrappedPlayer;
    private int extraLifeTime = 3;
    public PlayerHealthDecorator(GameObject player, int extraLifeTime) : base(player.Sprite, player.Position)
    {
        this.wrappedPlayer = player;    
        this.extraLifeTime = extraLifeTime;
    }
}