using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEnterEvent : MonoBehaviour
{
    [SerializeField] private Transform checkpointPos;
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
            other.transform.parent.position = checkpointPos.position;
        }
    }
}
