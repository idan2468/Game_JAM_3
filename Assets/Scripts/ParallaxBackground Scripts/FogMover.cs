using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogMover : MonoBehaviour
{
    private SpriteRenderer _renderer;
    [Header("Params")]
    [SerializeField] private float offset = 2f;
    [SerializeField] private float _distToMove;
    [Header("Debugging")]
    [SerializeField] private Bounds bounds;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _distToMove = _renderer.bounds.size.x / 4;
    }

    // Update is called once per frame
    void Update()
    {
        bounds = OrthographicBounds();
        if (bounds.min.x < _renderer.bounds.min.x + offset)
        {
            gameObject.transform.position += Vector3.left * _distToMove;
        }
        if (bounds.max.x  > _renderer.bounds.max.x - offset)
        {
            gameObject.transform.position += Vector3.right * _distToMove;
        }
        if (bounds.min.y < _renderer.bounds.min.y + offset)
        {
            gameObject.transform.position += Vector3.down * _distToMove;
        }
        if (bounds.max.y  > _renderer.bounds.max.y - offset)
        {
            gameObject.transform.position += Vector3.up * _distToMove;
        }
    }
    
    public static Bounds OrthographicBounds()
    {
        float cameraHeight = Camera.main.orthographicSize * 2;
        Bounds bounds = new Bounds(
            Camera.main.transform.position,
            new Vector3(cameraHeight * Camera.main.aspect, cameraHeight, 0));
        return bounds;
    }
}
