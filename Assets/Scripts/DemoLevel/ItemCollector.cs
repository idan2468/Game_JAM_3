using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private int _whiteSpiritAmt = 0;
    [SerializeField] private int _blueSpiritAmt = 0;

    public int WhiteSpiritAmt
    {
        get { return _whiteSpiritAmt; }
        set { _whiteSpiritAmt = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     // Collect spirits
    //     if (other.tag.Equals("Spirit"))
    //     {
    //         _whiteSpiritAmt++;
    //         other.GetComponent<SpiritAnimation>().Kill();
    //     }
    //
    //     // Open gates
    // }
}