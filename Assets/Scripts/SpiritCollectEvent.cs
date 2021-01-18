using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritCollectEvent : MonoBehaviour
{
    [SerializeField] private ItemCollector _itemCollector;

    // Start is called before the first frame update
    void Start()
    {
        _itemCollector = FindObjectOfType<ItemCollector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other is EdgeCollider2D || !other.gameObject.CompareTag("Player")) return;
        MusicController.Instance.PlaySound(MusicController.SoundEffects.Score, .5f);
        _itemCollector.SpiritsAmt++;
        Destroy(gameObject);
    }
}
