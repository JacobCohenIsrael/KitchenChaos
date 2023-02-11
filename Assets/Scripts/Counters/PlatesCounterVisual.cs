using System;
using System.Collections.Generic;
using UnityEngine;

namespace Counters
{
    public class PlatesCounterVisual : MonoBehaviour
    {
        [SerializeField] private PlatesCounter platesCounter;
        [SerializeField] private Transform counterTopPoint;
        [SerializeField] private Transform plateVisualPrefab;

        private List<GameObject> platesVisualGameObjectList;

        private void Awake()
        {
            platesVisualGameObjectList = new List<GameObject>();
        }
        
        private void Start()
        {
            platesCounter.OnPlateSpawned += OnPlateSpawned;
            platesCounter.OnPlateRemoved += OnPlatesRemoved;
        }

        private void OnDestroy()
        {
            platesCounter.OnPlateSpawned -= OnPlateSpawned;
            platesCounter.OnPlateRemoved -= OnPlatesRemoved;
        }

        private void OnPlatesRemoved(object sender, EventArgs e)
        {
            var lastPlateGameObject = platesVisualGameObjectList[^1];
            platesVisualGameObjectList.Remove(lastPlateGameObject);
            Destroy(lastPlateGameObject);
        }

        private void OnPlateSpawned(object sender, EventArgs e)
        {
            var plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
            var plateOffsetY = .1f;
            plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * platesVisualGameObjectList.Count, 0);
            
            platesVisualGameObjectList.Add(plateVisualTransform.gameObject);
        }
    }
     
}
