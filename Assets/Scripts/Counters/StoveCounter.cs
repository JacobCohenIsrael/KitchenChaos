using System;
using System.Linq;
using ScriptableObjects;
using UnityEngine;

namespace Counters
{
    public class StoveCounter : BaseCounter
    {
        public event EventHandler<State> OnStateChanged;
        public enum State
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
            SetState(State.Idle);
            stoveCounterVisual.Off();
        }

        private void SetState(State newState)
        {
            if (state != newState)
            {
                state = newState;
                OnStateChanged?.Invoke(this, state);
            }
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
                        SetState(State.Fried);
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

                        SetState(State.Burned);
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
                            
                            SetState(State.Idle);
                            stoveCounterVisual.Off();
                            progressBarUI.SetProgress(0f);
                        }
                    }
                }
                else
                {
                    GetKitchenObject().SetKitchenObjectParent(holder);

                    SetState(State.Idle);
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
                        SetState(State.Frying);
                        fryingTimer = 0f;
                        progressBarUI.SetProgress(0f);
                        stoveCounterVisual.On();
                    }
                }
            }
        }
        
        private bool HasRecipeResult(KitchenObjectSO inputKitchenObjectSo)
        {
            return GetFryingRecipe(inputKitchenObjectSo) != null;
        }

        private KitchenObjectSO GetRecipeResult(KitchenObjectSO inputKitchenObjectSo)
        {
            var fryingRecipe = GetFryingRecipe(inputKitchenObjectSo);
            return fryingRecipe != null ? fryingRecipe.output : null;
        }

        private FryingRecipeSO GetFryingRecipe(KitchenObjectSO inputKitchenObjectSo)
        {
            return (from fryingRecipeSo in fryingRecipeSOList where fryingRecipeSo.input == inputKitchenObjectSo select fryingRecipeSo).FirstOrDefault();

        }
        
        private BurningRecipeSO GetBurningRecipe(KitchenObjectSO inputKitchenObjectSo)
        {
            return (from burningRecipeSo in burningRecipeSOList where burningRecipeSo.input == inputKitchenObjectSo select burningRecipeSo).FirstOrDefault();

        }
    }
}
