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
    [Header("Params")] [SerializeField] private LayerMask layersAllowedToEnterSpots;
    [SerializeField] private float getDownTime = 1f;
    [SerializeField] private float getDownHeight = 3.5f;
    [SerializeField] private float delayTime = 1f;

    void Start()
    {
        foreach (Transform child in _spotsContainer.transform)
        {
            spots.Add(child.gameObject);
        }

        _spiritAnimation = _spirit.GetComponent<SpiritAnimation>();
        _spirit.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator SpotActivateWithDelay(GameObject spot, GameObject other)
    {
        yield return new WaitForSeconds(delayTime);
        if (IsInLayerMask(other.gameObject))
        {
            spots.Remove(spot);
            if (spots.Count == 0)
            {
                _spiritAnimation.enabled = false;
                _animation = DOTween.Sequence();
                _animation.AppendInterval(delayTime);
                _animation.AppendCallback(() => _spirit.SetActive(true));
                _animation.Append(_spirit.transform.DOMoveY(_spirit.transform.position.y - getDownHeight, getDownTime));
                _animation.OnComplete(() => { _spiritAnimation.enabled = true; });
                _animation.Play();
                _spotsContainer.SetActive(false);
            }
        }
    }
    public void SpotActivated(GameObject spot, GameObject other)
    {
        if (IsInLayerMask(other.gameObject))
        {
            spots.Remove(spot);
            if (spots.Count == 0)
            {
                _spiritAnimation.enabled = false;
                _animation = DOTween.Sequence();
                _animation.AppendInterval(delayTime);
                _animation.AppendCallback(() =>
                {
                    if (spots.Count != 0)
                    {
                        _animation.Kill();
                    }
                });
                _animation.AppendCallback(() => _spirit.SetActive(true));
                _animation.Append(_spirit.transform.DOMoveY(_spirit.transform.position.y - getDownHeight, getDownTime));
                _animation.OnComplete(() =>
                {
                    _spiritAnimation.enabled = true;
                    _spotsContainer.SetActive(false);
                });
                _animation.SetEase(Ease.InOutSine);
                _animation.Play();
            }
        }
    }

    public void SpotDeactivated(GameObject spot, GameObject other)
    {
        if (IsInLayerMask(other.gameObject))
        {
            if (!spots.Contains(spot))
            {
                spots.Add(spot);
            }
        }
    }
    
    private bool IsInLayerMask(GameObject obj)
    {
        return ((layersAllowedToEnterSpots.value & (1 << obj.layer)) > 0);
    }
}