using System;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyObjectTrashed;

    public static void ResetStaticData()
    {
        OnAnyObjectTrashed = null;
    }
    
    public override void Interact(IKitchenObjectParent player)
    {
        if (player.HasKitchenObject()) {
            player.GetKitchenObject().DestroySelf();
            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}