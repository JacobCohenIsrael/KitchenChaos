using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private ContainerCounterVisual counterVisual;
    
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject()) return;

        KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
        counterVisual.PlayOpenClose();
    }

}
