using System;
using System.Collections;
using System.Collections.Generic;
using Counters;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipListSO deliverySuccessAudioClipListSo;
    [SerializeField] private AudioClipListSO deliveryFailAudioClipListSo;
    [SerializeField] private AudioClipListSO choppingAudioClipListSo;
    [SerializeField] private AudioClipListSO objectPickupAudioClipListSo;
    [SerializeField] private AudioClipListSO objectDropAudioClipListSo;
    [SerializeField] private AudioClipListSO objectTrashedAudioClipListSo;
    [SerializeField] private AudioClipListSO footstepAudioClipListSo;

    [SerializeField] private DeliveryCounter deliveryCounter;
    [SerializeField] private Player player;
    
    private void Start()
    {
        DeliveryManager.Instance.OnDeliverySuccess += OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFail += OnDeliveryFail;
        TrashCounter.OnObjectTrashed += TrashCounterOnOnObjectTrashed;
        BaseCounter.OnKitchenObjectPlaced += OnKitchenObjectPlaced;
        CuttingCounter.OnAnyCut += OnAnyCut;
        player.OnKitchenItemPicked += PlayerOnOnKitchenItemPicked;
    }

    public void PlayFootstepSound(Transform originTransform, float volume)
    {
        PlaySound(footstepAudioClipListSo, originTransform.position, volume);
    }

    private void TrashCounterOnOnObjectTrashed(object sender, Transform originTransform)
    {
        PlaySound(objectTrashedAudioClipListSo, originTransform.position);
    }

    private void OnKitchenObjectPlaced(object sender, Transform originTransform)
    {
        PlaySound(objectDropAudioClipListSo, originTransform.position);
    }

    private void PlayerOnOnKitchenItemPicked(object sender, EventArgs e)
    {
        PlaySound(objectPickupAudioClipListSo, player.transform.position);
    }

    private void OnAnyCut(object sender, Transform originTransform)
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
