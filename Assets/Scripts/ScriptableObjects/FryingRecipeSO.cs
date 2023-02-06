using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Recipe/Frying")]
    public class FryingRecipeSO : ScriptableObject
    {
        public KitchenObjectSO input;
        public KitchenObjectSO output;
        public float fryingTimerMax;
    }
}
