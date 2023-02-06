using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Recipe/Burning")]
    public class BurningRecipeSO : ScriptableObject
    {
        public KitchenObjectSO input;
        public KitchenObjectSO output;
        public float burningTimerMax;
    }
}
