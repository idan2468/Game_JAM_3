using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableNextHookEvent : MonoBehaviour
{
    [SerializeField] private Transform nextHook;
    [SerializeField] private bool wasTriggered;
    [SerializeField] private HooksInTheSky _hooksInTheSky;
    [SerializeField] private List<SpriteRenderer> hookSprites;

    // Start is called before the first frame update
    void Start()
    {
        wasTriggered = false;
        _hooksInTheSky = Utility.FindParentPuzzleGameObject(gameObject).GetComponent<HooksInTheSky>();
        ExtractSprites();
    }


    private void ExtractSprites()
    {
        hookSprites = new List<SpriteRenderer>();
        var spriteRenderer = nextHook.GetComponentsInChildren<SpriteRenderer>();
        hookSprites.AddRange(spriteRenderer);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (wasTriggered || !other.CompareTag("Player")) return;
        _hooksInTheSky.FadeHook(hookSprites, nextHook,
            gameObject.transform.parent.GetComponentInChildren<HookController>());
        wasTriggered = true;
    }
}