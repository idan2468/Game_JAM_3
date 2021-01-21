using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeChildEvent : MonoBehaviour
{
    [SerializeField] private LayerMask objectsOnTopOfStone;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (Utility.IsInLayerMask(other.gameObject, objectsOnTopOfStone))
        {
            other.gameObject.transform.parent = gameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (Utility.IsInLayerMask(other.gameObject, objectsOnTopOfStone))
        {
            other.gameObject.transform.parent = other.gameObject.CompareTag("Player")
                ? null
                : Utility.FindParentPuzzleGameObject(gameObject).transform;
        }
    }
}