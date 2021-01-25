using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirFallScript : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private Vector3 _lastCheckpoint;

    public Vector3 LastCheckpoint
    {
        set { _lastCheckpoint = value; }
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

        // Send player back to last checkpoint
        GameManager.Instance.ReturnPlayerToCheckpoint(_lastCheckpoint);
    }
}
