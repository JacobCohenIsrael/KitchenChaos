using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Audio/ClipList")]
    public class AudioClipListSO : ScriptableObject
    {
        public AudioClip[] clips;
    }
}