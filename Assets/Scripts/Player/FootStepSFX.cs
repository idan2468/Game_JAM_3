using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSFX : MonoBehaviour
{
    // Start is called before the first frame update
    private string[] steps = {"Step1", "Step2", "Step3"};
    private string[] drags = { "Drag1", "Drag2", "Drag3" };
    [SerializeField] private float stepSound = 0.2f;
    [SerializeField] private float dragSound = 1f;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayStep(int stepNum = -1)
    {
        var step = steps[Random.Range(0, steps.Length)];
        if (stepNum >= 1 && stepNum <= steps.Length)
        {
            step = steps[stepNum - 1];
        }

        MusicController.Instance.PlaySound(step, stepSound);
    }

    public void PlayDrag(int stepNum = -1)
    {
        var drag = drags[Random.Range(0, drags.Length)];
        if (stepNum >= 1 && stepNum <= drags.Length)
        {
            drag = drags[stepNum - 1];
        }

        MusicController.Instance.PlaySound(drag, dragSound);
    }
}