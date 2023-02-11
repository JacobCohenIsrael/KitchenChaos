using System;
using System.Numerics;
using Player;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    [SerializeField] private AudioClipListSO deliverySuccessAudioClipListSo;
    [SerializeField] private AudioClipListSO deliveryFailAudioClipListSo;
    [SerializeField] private AudioClipListSO choppingAudioClipListSo;
    [SerializeField] private AudioClipListSO objectPickupAudioClipListSo;
    [SerializeField] private AudioClipListSO objectDropAudioClipListSo;
    [SerializeField] private AudioClipListSO objectTrashedAudioClipListSo;
    [SerializeField] private AudioClipListSO footstepAudioClipListSo;
    [SerializeField] private AudioClipListSO warningAudioClipListSo;
    
    [SerializeField] private DeliveryCounter deliveryCounter;
    [SerializeField] private PlayerInteractions playerInteractions;
    
    [SerializeField] private EventSO chopEvent;
    [SerializeField] private EventSO trashEvent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        DeliveryManager.Instance.OnDeliverySuccess += OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFail += OnDeliveryFail;
        BaseCounter.OnKitchenObjectPlaced += OnKitchenObjectPlaced;
        playerInteractions.OnKitchenItemPicked += PlayerInteractionsOnOnKitchenItemPicked;
        trashEvent.Subscribe(TrashCounterOnOnObjectTrashed);
        chopEvent.Subscribe(OnAnyCut);
    }

    private void OnDestroy()
    {
        DeliveryManager.Instance.OnDeliverySuccess -= OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFail -= OnDeliveryFail;
        BaseCounter.OnKitchenObjectPlaced -= OnKitchenObjectPlaced;
        playerInteractions.OnKitchenItemPicked -= PlayerInteractionsOnOnKitchenItemPicked;
        chopEvent.Unsubscribe(OnAnyCut);
        trashEvent.Unsubscribe(TrashCounterOnOnObjectTrashed);

    }

    public void PlayWarningSound(Transform originTransform)
    {
        PlaySound(warningAudioClipListSo, originTransform.position);
    }

    public void PlayCountdown()
    {
        PlaySound(warningAudioClipListSo, Camera.main.transform.position, 1f);
    }

    public void PlayFootstepSound(Transform originTransform, float volume)
    {
        PlaySound(footstepAudioClipListSo, originTransform.position, volume);
    }

    private void TrashCounterOnOnObjectTrashed(Transform originTransform)
    {
        PlaySound(objectTrashedAudioClipListSo, originTransform.position);
    }

    private void OnKitchenObjectPlaced(object sender, Transform originTransform)
    {
        PlaySound(objectDropAudioClipListSo, originTransform.position);
    }

    private void PlayerInteractionsOnOnKitchenItemPicked(object sender, EventArgs e)
    {
        PlaySound(objectPickupAudioClipListSo, playerInteractions.transform.position);
    }

    private void OnAnyCut(Transform originTransform)
    {
        PlaySound(choppingAudioClipListSo, originTransform.position);
    }

    private void OnDeliverySuccess(object sender, EventArgs e)
    {
        PlaySound(deliverySuccessAudioClipListSo, deliveryCounter.transform.position);
    }

    private void OnDeliveryFail(object sender, EventArgs e)
    {
        PlaySound(deliveryFailAudioClipListSo, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClipListSO audioClipListSo, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClipListSo.clips[Random.Range(0, audioClipListSo.clips.Length)], position, volume);
    }
    
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
}
