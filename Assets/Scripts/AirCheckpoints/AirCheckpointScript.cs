﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCheckpointScript : MonoBehaviour
{
    [SerializeField] private AirFallScript _airFallColliderScript;

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

        // If player collided, set as last checkpoint
        _airFallColliderScript.LastCheckpoint = transform.position;
    }
}