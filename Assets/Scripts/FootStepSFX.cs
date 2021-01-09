using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSFX : MonoBehaviour
{
    // Start is called before the first frame update
    private string[] steps = {"Step1", "Step2", "Step3"};
    [SerializeField] private float stepSound = 0.2f;

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
}