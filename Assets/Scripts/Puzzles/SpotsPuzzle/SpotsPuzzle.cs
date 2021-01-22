using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpotsPuzzle : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Debugging")] 
    [SerializeField] private List<GameObject> spots;
    [SerializeField] private Coroutine triggerWithDelay;
    [Header("Params")] [SerializeField] private LayerMask layersAllowedToEnterSpots;
    [SerializeField] private float delayTime = 1f;
    [SerializeField] private GameObject _spotsContainer;
    private Action eventToTrigger;
    public Action EventToTrigger
    {
        set => eventToTrigger = value;
    }

    void Start()
    {
        foreach (Transform child in _spotsContainer.transform)
        {
            spots.Add(child.gameObject);
        }
    }

    private IEnumerator TriggerWithDelay()
    {
        yield return new WaitForSeconds(delayTime);
        if (spots.Count == 0)
        {
            eventToTrigger();
        }
    }
    public void SpotActivated(GameObject spot, GameObject other)
    {
        if (Utility.IsInLayerMask(other.gameObject, layersAllowedToEnterSpots))
        {
            spots.Remove(spot);
            if (spots.Count == 0)
            {
                triggerWithDelay = StartCoroutine(TriggerWithDelay());
            }
        }
    }

    public void SpotDeactivated(GameObject spot, GameObject other)
    {
        StopCoroutine(triggerWithDelay);
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