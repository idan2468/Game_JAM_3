using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatioScript : MonoBehaviour
{
    public float targetWidth = 1920f;
    public float targetHeight = 1080f;
    public float pixelsPerUnit = 100f;

    void Start()
    {
        
    }

    private void Update()
    {
        Calculate();
    }

    // TODO: MOVE FROM UPDATE TO START
    // TODO: CHECK EFFECT ON STATIC HOOK (WATER LEVEL) CAMERA
    void Calculate()
    {
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float targetAspect = targetWidth / targetHeight;
        float scaleHeight = windowAspect / targetAspect;

        var camera = GetComponent<Cinemachine.CinemachineVirtualCamera>();

        if (windowAspect < targetAspect)
        {
            camera.m_Lens.OrthographicSize = (targetHeight / (2 * pixelsPerUnit)) / scaleHeight;
        }
        else
        {
            camera.m_Lens.OrthographicSize = targetHeight / (2 * pixelsPerUnit);
        }
    }
}
