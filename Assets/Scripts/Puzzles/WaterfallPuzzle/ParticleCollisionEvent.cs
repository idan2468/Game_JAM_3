using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

public class ParticleCollisionEvent : MonoBehaviour
{
    private Tween animation;
    private bool showenSpirit;
    [Header("Params")]
    [SerializeField] private Transform checkpointPuzzle;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask moveableObjectLayer;
    [SerializeField] private SpiritAnimation spirit;



    // Start is called before the first frame update
    void Start()
    {
        showenSpirit = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartSpiritFadeInAnimation()
    {
        spirit.gameObject.SetActive(true);
        spirit.TriggerFadeInAnimation();
        showenSpirit = true;
    }
    private void OnParticleCollision(GameObject other)
    {
        if(IsInLayerMask(other,playerLayer))
        {
            other.transform.position = checkpointPuzzle.position;
        }

        if (IsInLayerMask(other, moveableObjectLayer)  && !showenSpirit)
        {
            StartSpiritFadeInAnimation();
        }
    }
    private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << obj.layer)) > 0);
    }
}
