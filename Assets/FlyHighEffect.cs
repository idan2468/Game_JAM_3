using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyHighEffect : MonoBehaviour
{
    [SerializeField] private float forceY = 10f;
    [SerializeField] private Rigidbody2D rbPlayer;
    [SerializeField] private float dragOverTime = 2f;
    private float orgForceY;
    private bool flyingUp;

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
        orgForceY = forceY;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other is EdgeCollider2D) return;
        rbPlayer.AddForce(new Vector2(0,forceY));
        forceY -= dragOverTime * Time.fixedDeltaTime;
        if (forceY < 0)
        {
            forceY = orgForceY;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other is EdgeCollider2D) return;
        forceY = orgForceY;
    }
}
