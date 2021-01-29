using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyHighEffect : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float forceY = 10f;
    [SerializeField] private Rigidbody2D rbPlayer;
    [SerializeField] private float dragOverTime = 2f;
    [SerializeField] private float orgForceY;
    [SerializeField] private float startSpeedYOnEnter = 0f;
    [Header("Debugging")]
    [SerializeField] private bool flyingUp;

    // Start is called before the first frame update
    void Start()
    {
        flyingUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (rbPlayer == null)
        {
            rbPlayer = other.gameObject.transform.parent.GetComponent<Rigidbody2D>();
        }
        orgForceY = forceY;
        rbPlayer.velocity = new Vector2(rbPlayer.velocity.x,startSpeedYOnEnter);
        flyingUp = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!flyingUp) return;
        rbPlayer.AddForce(new Vector2(0, forceY));
        forceY -= dragOverTime * Time.fixedDeltaTime;
        if (forceY < 0)
        {
            forceY = orgForceY;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        flyingUp = false;
        forceY = orgForceY;
    }
}
