using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Event")]
    public class EventSO : ScriptableObject
    {
        public void Subscribe(Action action)
        {
            eventHandler += action;
        }

        public void Raise()
        {
            eventHandler?.Invoke();
        }
        
        private event Action eventHandler;
    }
}