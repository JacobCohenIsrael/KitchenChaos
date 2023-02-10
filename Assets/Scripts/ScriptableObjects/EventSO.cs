using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Event")]
    public class EventSO : ScriptableObject
    {
        public void Subscribe(Action<Transform> action)
        {
            eventHandler += action;
        }

        public void Unsubscribe(Action<Transform> action)
        {
            eventHandler -= action;
        }

        public void Raise(Transform transform)
        {
            eventHandler?.Invoke(transform);
        }
        
        private event Action<Transform> eventHandler;
    }
}