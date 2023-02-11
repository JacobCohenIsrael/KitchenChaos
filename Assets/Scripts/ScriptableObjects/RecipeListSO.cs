using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Recipe/Recipe List")]
    public class RecipeListSO : ScriptableObject
    {
        public List<RecipeSO> recipeSoList;
    }
}