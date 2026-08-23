using UnityEngine;

public class BaseCamp : PlayerUnit
{
    public override void Die()
    {
        base.Die();
        GameManager.Event.Broadcast(HexEvents.GameOver.ToString(), GameEventParameter.Empty);
    }
}