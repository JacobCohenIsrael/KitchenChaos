using System;
using System.Linq;
using ScriptableObjects;
using UnityEngine;

namespace Counters
{
    public class StoveCounter : BaseCounter
    {
        private enum State
        {
            Idle,
            Frying,
            Fried,
            Burned
        }
        [SerializeField] private FryingRecipeSO[] fryingRecipeSOList;
        [SerializeField] private BurningRecipeSO[] burningRecipeSOList;
        [SerializeField] private StoveCounterVisual stoveCounterVisual;
        [SerializeField] private ProgressBarUI progressBarUI;

        private State state;
        private float fryingTimer;
        private float burningTimer;
        private FryingRecipeSO fryingRecipeSO;
        private BurningRecipeSO burningRecipeSO;

        private void Start()
        {
            state = State.Idle;
            stoveCounterVisual.Off();
        }

        private void Update()
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    progressBarUI.SetProgress(fryingTimer / fryingRecipeSO.fryingTimerMax);
                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        burningRecipeSO = GetBurningRecipe(GetKitchenObject().GetKitchenObjectSO());
                        state = State.Fried;
                        burningTimer = 0f;
                        progressBarUI.SetProgress(0f);

                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    progressBarUI.SetProgress(burningTimer / burningRecipeSO.burningTimerMax);

                    if (burningTimer > burningRecipeSO.burningTimerMax)
                    {
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                        state = State.Burned;
                        stoveCounterVisual.Off();
                        progressBarUI.SetProgress(0f);
                    }
                    break;
                case State.Burned:
                    break;
            }
        }

        public override void Interact(IKitchenObjectParent holder)
        {
            if (HasKitchenObject())
            {
                if (holder.HasKitchenObject())
                {
                    if (holder.GetKitchenObject().TryGetPlate(out var plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        {
                            GetKitchenObject().DestroySelf();
                            
                            state = State.Idle;
                            stoveCounterVisual.Off();
                            progressBarUI.SetProgress(0f);
                        }
                    }
                }
                else
                {
                    GetKitchenObject().SetKitchenObjectParent(holder);

                    state = State.Idle;
                    stoveCounterVisual.Off();
                    progressBarUI.SetProgress(0f);
                }
            }
            else
            {
                if (holder.HasKitchenObject())
                {
                    if (HasRecipeResult(holder.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        holder.GetKitchenObject().SetKitchenObjectParent(this);
                        fryingRecipeSO = GetFryingRecipe(GetKitchenObject().GetKitchenObjectSO());
                        state = State.Frying;
                        fryingTimer = 0f;
                        progressBarUI.SetProgress(0f);
                        stoveCounterVisual.On();
                    }
                }
            }
        }
        
        private bool HasRecipeResult(KitchenObjectSO inputKitchenObjectSO)
        {
            return GetFryingRecipe(inputKitchenObjectSO) != null;
        }

        private KitchenObjectSO GetRecipeResult(KitchenObjectSO inputKitchenObjectSO)
        {
            var fryingRecipe = GetFryingRecipe(inputKitchenObjectSO);
            return fryingRecipe != null ? fryingRecipe.output : null;
        }

        private FryingRecipeSO GetFryingRecipe(KitchenObjectSO inputKitchenObjectSO)
        {
            return (from fryingRecipeSO in fryingRecipeSOList where fryingRecipeSO.input == inputKitchenObjectSO select fryingRecipeSO).FirstOrDefault();

        }
        
        private BurningRecipeSO GetBurningRecipe(KitchenObjectSO inputKitchenObjectSO)
        {
            return (from burningRecipeSO in burningRecipeSOList where burningRecipeSO.input == inputKitchenObjectSO select burningRecipeSO).FirstOrDefault();

        }
    }
}
