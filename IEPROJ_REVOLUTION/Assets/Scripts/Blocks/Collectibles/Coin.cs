public class Coin : Collectible
{
    protected override void OnCollect()
    {
        // Add coin to level here
    }

    public override void OnResetCollectible()
    {
        throw new System.NotImplementedException();
    }
}
