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
            // CR: no need to update timer if plates are at max
            if (platesSpawned >= platesSpawnMax || !GameManager.Instance.IsGamePlaying()) return;
            spawnPlateTimer += Time.deltaTime;
            if (spawnPlateTimer > spawnPlateTimerMax)
            {
                platesSpawned++;
                spawnPlateTimer = 0f;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }    
        }

        public override void Interact(IKitchenObjectParent holder)
        {
            if (!holder.HasKitchenObject() && platesSpawned > 0)
            {
                platesSpawned--;

                KitchenObject.SpawnKitchenObject(platesKitchenObjectSO, holder);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
