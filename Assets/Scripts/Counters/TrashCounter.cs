namespace Counters
{
    public class TrashCounter : BaseCounter
    {
        public override void Interact(IKitchenObjectParent holder)
        {
            if (holder.HasKitchenObject())
            {
                holder.GetKitchenObject().DestroySelf();
            }
        }
    }
}
