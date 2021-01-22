using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointEnterEvent : MonoBehaviour
{
    private Action eventToTrigger;
    [SerializeField] private bool _reset = true;
    public bool Reset
    {
        get { return _reset; }
        set { _reset = value; }
    }

    public Action EventToTrigger
    {
        set => eventToTrigger = value;
    }

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
        if (other is EdgeCollider2D || !other.gameObject.CompareTag("Player")) return;
        if (_reset) eventToTrigger();
    }
}
