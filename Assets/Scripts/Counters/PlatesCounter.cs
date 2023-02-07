using System;
using ScriptableObjects;
using UnityEngine;

namespace Counters
{
    public class PlatesCounter : BaseCounter
    {
        public event EventHandler OnPlateSpawned;
        public event EventHandler OnPlateRemoved;

        [SerializeField] private KitchenObjectSO platesKitchenObjectSO;
        [SerializeField] private float spawnPlateTimerMax = 4f;
        [SerializeField] private int platesSpawnMax = 4;

        private float spawnPlateTimer;
        private int platesSpawned;
        private void Update()
        {
            spawnPlateTimer += Time.deltaTime;
            if (spawnPlateTimer > spawnPlateTimerMax)
            {
                if (platesSpawned < platesSpawnMax)
                {
                    platesSpawned++;
                    
                    OnPlateSpawned?.Invoke(this, EventArgs.Empty);
                }
            }    
        }

        public override void Interact(Player player)
        {
            if (!player.HasKitchenObject() && platesSpawned > 0)
            {
                platesSpawned--;

                KitchenObject.SpawnKitchenObject(platesKitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
