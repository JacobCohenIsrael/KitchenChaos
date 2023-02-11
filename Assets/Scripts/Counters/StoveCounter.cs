using System;
using System.Linq;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Counters
{
    public class StoveCounter : BaseCounter
    {
        public event EventHandler<State> OnStateChanged;
        public event EventHandler<float> OnProgressChanged;
        public enum State
        {
            Idle,
            Frying,
            Fried,
            Burned
        }
        [FormerlySerializedAs("fryingRecipeSOList")] [SerializeField] private FryingRecipeSO[] fryingRecipeSoList;
        [FormerlySerializedAs("burningRecipeSOList")] [SerializeField] private BurningRecipeSO[] burningRecipeSoList;
        [SerializeField] private StoveCounterVisual stoveCounterVisual;
        [SerializeField] private ProgressBarUI progressBarUI;

        private State state;
        private float fryingTimer;
        private float burningTimer;
        private FryingRecipeSO fryingRecipeSo;
        private BurningRecipeSO burningRecipeSo;

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
                    SetProgress(fryingTimer / fryingRecipeSo.fryingTimerMax);
                    if (fryingTimer > fryingRecipeSo.fryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(fryingRecipeSo.output, this);
                        burningRecipeSo = GetBurningRecipe(GetKitchenObject().GetKitchenObjectSO());
                        SetState(State.Fried);
                        burningTimer = 0f;
                        SetProgress(0f);

                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    SetProgress(burningTimer / burningRecipeSo.burningTimerMax);

                    if (burningTimer > burningRecipeSo.burningTimerMax)
                    {
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(burningRecipeSo.output, this);

                        SetState(State.Burned);
                        stoveCounterVisual.Off();
                        SetProgress(0f);
                    }
                    break;
                case State.Burned:
                    break;
            }
        }

        public bool IsFried()
        {
            return state == State.Fried;
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
                            SetProgress(0f);
                        }
                    }
                }
                else
                {
                    GetKitchenObject().SetKitchenObjectParent(holder);

                    SetState(State.Idle);
                    stoveCounterVisual.Off();
                    SetProgress(0f);
                }
            }
            else
            {
                if (holder.HasKitchenObject())
                {
                    if (HasRecipeResult(holder.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        holder.GetKitchenObject().SetKitchenObjectParent(this);
                        fryingRecipeSo = GetFryingRecipe(GetKitchenObject().GetKitchenObjectSO());
                        SetState(State.Frying);
                        fryingTimer = 0f;
                        SetProgress(0f);
                        stoveCounterVisual.On();
                    }
                }
            }
        }

        private void SetProgress(float progress)
        {
            progressBarUI.SetProgress(progress);
            OnProgressChanged?.Invoke(this, progress);
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
            return (from fryingRecipeSo in fryingRecipeSoList where fryingRecipeSo.input == inputKitchenObjectSo select fryingRecipeSo).FirstOrDefault();
        }
        
        private BurningRecipeSO GetBurningRecipe(KitchenObjectSO inputKitchenObjectSo)
        {
            return (from burningRecipeSo in burningRecipeSoList where burningRecipeSo.input == inputKitchenObjectSo select burningRecipeSo).FirstOrDefault();
        }
    }
}
