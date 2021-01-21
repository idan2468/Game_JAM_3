using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpotsPuzzle : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Debugging")] [SerializeField] private GameObject _spotsContainer;
    [SerializeField] private GameObject _spirit;
    [SerializeField] private List<GameObject> spots;
    [SerializeField] private SpiritAnimation _spiritAnimation;
    [SerializeField] private Sequence _animation;
    [SerializeField] private Coroutine triggerWithDelay;
    [Header("Params")] [SerializeField] private LayerMask layersAllowedToEnterSpots;
    [SerializeField] private float getDownTime = 1f;
    [SerializeField] private float getDownHeight = 3.5f;
    [SerializeField] private float delayTime = 1f;
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

        eventToTrigger = GetSpotDownAnimation;
        _spiritAnimation = _spirit.GetComponent<SpiritAnimation>();
        _spirit.SetActive(false);
    }
    
    private void GetSpotDownAnimation()
    {
        _spiritAnimation.enabled = false;
        _animation = DOTween.Sequence();
        _animation.AppendCallback(() => _spirit.SetActive(true));
        _animation.Append(_spirit.transform.DOMoveY(_spirit.transform.position.y - getDownHeight, getDownTime));
        _animation.OnComplete(() =>
        {
            if (_spiritAnimation != null)
            {
                _spiritAnimation.enabled = true;
            }

            _spotsContainer.SetActive(false);
        });
        _animation.SetEase(Ease.InOutSine);
        _animation.Play();
    }

    private IEnumerator TriggerWithDelay()
    {
        yield return new WaitForSeconds(delayTime);
        if (spots.Count == 0)
        {
            eventToTrigger();
            // GetSpotDownAnimation();
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
}