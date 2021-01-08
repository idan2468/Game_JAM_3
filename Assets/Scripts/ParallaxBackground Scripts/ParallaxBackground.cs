using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
public class ParallaxBackground : MonoBehaviour
{
    public float length, startPos;

    [SerializeField] private GameObject cam;

    [SerializeField] private float parallaxEffect = 1f;
    private SpriteRenderer _spriteRenderer;
    private void Start()
    {
        startPos = transform.position.x;
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        length = _spriteRenderer.bounds.size.x;
    }

    private void LateUpdate()
    {
        var currPos = cam.transform.position;
        var temp = currPos.x * (1 - parallaxEffect);
        var dist = currPos.x * parallaxEffect;
        transform.position = new Vector3(startPos + dist, currPos.y, currPos.z);
        if (temp < startPos - length)
        {
            startPos -= length;
        }
        else if (temp > startPos + length)
        {
            startPos += length;
        }
    }

    // public ParallaxCamera parallaxCamera;
    // List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();
    //
    // void Start()
    // {
    //     if (parallaxCamera == null)
    //         parallaxCamera = Camera.main.GetComponent<ParallaxCamera>();
    //     if (parallaxCamera != null)
    //         parallaxCamera.onCameraTranslate += Move;
    //     SetLayers();
    // }
    //
    // void SetLayers()
    // {
    //     parallaxLayers.Clear();
    //     for (int i = 0; i < transform.childCount; i++)
    //     {
    //         ParallaxLayer layer = transform.GetChild(i).GetComponent<ParallaxLayer>();
    //
    //         if (layer != null)
    //         {
    //             // layer.name = "Layer-" + i;
    //             parallaxLayers.Add(layer);
    //         }
    //     }
    // }
    // void Move(float delta)
    // {
    //     foreach (ParallaxLayer layer in parallaxLayers)
    //     {
    //         layer.Move(delta);
    //     }
    // }
    // private void OnDrawGizmos()
    // {
    //     if (_spriteRenderer == null)
    //     {
    //         _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    //     }
    //     // Gizmos.DrawWireCube(_spriteRenderer.bounds.center,_spriteRenderer.bounds.size);
    // }
}
