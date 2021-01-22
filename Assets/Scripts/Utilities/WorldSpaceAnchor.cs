using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class WorldSpaceAnchor : MonoBehaviour
{
    [SerializeField] private float xOffset = 1f;
    [SerializeField] private float yOffset = 1f;
    [SerializeField] private Corner cornerAnchor;

    private enum Corner
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    // Start is called before the first frame update
    void Start()
    {
    }
    

    // Update is called once per frame
    void Update()
    {
        var bounds = Utility.OrthographicBounds();
        var newPos = Vector3.zero;
        switch (cornerAnchor)
        {
            case Corner.TopLeft:
                newPos = new Vector3(bounds.min.x + xOffset, bounds.max.y - yOffset, transform.position.z);
                break;
            case Corner.TopRight:
                newPos = new Vector3(bounds.max.x - xOffset, bounds.max.y - yOffset, transform.position.z);
                break;
            case Corner.BottomLeft:
                newPos = new Vector3(bounds.min.x + xOffset, bounds.min.y + yOffset, transform.position.z);
                break;
            case Corner.BottomRight:
                newPos = new Vector3(bounds.max.x - xOffset, bounds.min.y + yOffset, transform.position.z);
                break;
        }

        transform.position = newPos;
    }
}
