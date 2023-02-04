using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private ContainerCounterVisual counterVisual;
    
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject()) return;
        
        var kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        counterVisual.PlayOpenClose();
    }

}
