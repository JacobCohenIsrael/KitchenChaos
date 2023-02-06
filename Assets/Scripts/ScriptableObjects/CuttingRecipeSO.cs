using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Recipe/Cutting")]
    public class CuttingRecipeSO : ScriptableObject
    {
        public KitchenObjectSO input;
        public KitchenObjectSO output;
        public int cuttingProgressMax;
    }
}
