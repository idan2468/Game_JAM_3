using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpotsPuzzle : MonoBehaviour
{
    // Start is called before the first frame update
    //todo: Change names of spots and spotsCopy to something more accurate
    [Header("Debugging")] 
    [SerializeField] private List<GameObject> spots;
    [SerializeField] private Coroutine triggerWithDelay;
    [SerializeField] private int _spotSequenceIndex = 0;
    [SerializeField] private List<GameObject> _spotsInSequence; // Save activated spot for possible reset
    [SerializeField] private List<SpotLightEffect> _spotScripts;

    [Header("Params")] [SerializeField] private LayerMask layersAllowedToEnterSpots;
    [SerializeField] private float delayTime = 1f;
    [SerializeField] private GameObject _spotsContainer;
    [SerializeField] private bool bySequence = false;
    private Action eventToTrigger;
    private Action eventResetPuzzle;
    [SerializeField] private float puzzleSuccessVolume = 1f;

    public Action EventToTrigger
    {
        set => eventToTrigger = value;
    }

    public Action EventResetPuzzle
    {
        set => eventResetPuzzle = value;
    }

    public bool IsBySequence => bySequence; 

    void Start()
    {
        foreach (Transform child in _spotsContainer.transform)
        {
            spots.Add(child.gameObject);
            _spotScripts.Add(child.GetComponent<SpotLightEffect>());
            if (bySequence) _spotsInSequence.Add(child.gameObject);
        }
    }

    private IEnumerator TriggerWithDelay()
    {
        yield return new WaitForSeconds(delayTime);
        if (spots.Count == 0)
        {
            MusicController.Instance.PlaySound(MusicController.SoundEffects.SuccessPuzzle,puzzleSuccessVolume);
            eventToTrigger();
        }
    }
    public void SpotActivated(GameObject spot, GameObject other)
    {
        if (Utility.IsInLayerMask(other.gameObject, layersAllowedToEnterSpots))
        {
            if (bySequence)
            {
                // If wrong spot in sequence reset
                if (_spotsInSequence.IndexOf(spot) > _spotSequenceIndex)
                {
                    for (int i = 0; i < _spotsInSequence.Count; ++i)
                    {
                        SpotDeactivated(_spotsInSequence[i], other);
                        _spotScripts[i].FadeInLight();
                    }
                    _spotSequenceIndex = 0;

                    eventResetPuzzle();
                    return;
                }
                else if (_spotsInSequence.IndexOf(spot) == _spotSequenceIndex)
                {
                    _spotScripts[_spotSequenceIndex].FadeOutLight();
                    _spotSequenceIndex++;
                }
            }

            spots.Remove(spot);
            if (spots.Count == 0)
            {
                triggerWithDelay = StartCoroutine(TriggerWithDelay());
            }
        }
    }

    public void SpotDeactivated(GameObject spot, GameObject other)
    {
        if(triggerWithDelay != null)
        {
            StopCoroutine(triggerWithDelay);
        }
        if (Utility.IsInLayerMask(other.gameObject, layersAllowedToEnterSpots))
        {
            if (!spots.Contains(spot))
            {
                spots.Add(spot);
            }
        }
    }

    public void TurnOffPuzzle()
    {
        _spotsContainer.SetActive(false);
    }
}