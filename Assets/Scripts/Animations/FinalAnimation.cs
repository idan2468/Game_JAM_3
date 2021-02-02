using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FinalAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _targetTemple;
    [SerializeField] private float _ascentSpeed = 10f;
    [SerializeField] private Ease ease = Ease.InOutSine;
    private Sequence _animation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other is EdgeCollider2D) return;
        if (other.CompareTag("Player"))
        {
            var dist = Vector3.Distance(other.transform.parent.position, _targetTemple.transform.position);
            _animation = DOTween.Sequence()
                .Append(other.transform.parent.DOMove(_targetTemple.transform.position, _ascentSpeed != 0 ? dist / _ascentSpeed : _ascentSpeed))
                .SetEase(ease);
        }
    }
}
