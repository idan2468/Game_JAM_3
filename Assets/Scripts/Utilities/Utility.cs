using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Utility : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public static bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << obj.layer)) > 0);
    }
    
    public static Sequence DisableInspectorButton(TweenCallback cb)
    {
        return DOTween.Sequence()
            .AppendInterval(1f)
            .AppendCallback(cb);
    }
    
}
