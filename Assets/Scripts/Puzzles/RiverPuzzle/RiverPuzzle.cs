using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RiverPuzzle : MonoBehaviour
{
    private Tween animationMove, animationRotate;
    
    [Header("Params")] [SerializeField] private Transform endOfRiver;
    [SerializeField] private Transform raftTransform;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject spirit;
    [SerializeField] private float distOffsetToEnableSpirit;
    [Header("Raft Animation")]
    [SerializeField] private bool resetAnimation = false;
    [SerializeField] private float speedMoveX;
    [SerializeField] private float maxRotationZ;
    [SerializeField] private float rotationSpeed;
    [Header("Debugging")] [SerializeField] float endOfRiverX;
    [SerializeField] float startOfRiverX;
    [SerializeField] float riverLength;
    [SerializeField] private Vector3 endRotation;
    [SerializeField] private bool tookSpirit;

    // Start is called before the first frame update
    void Start()
    {
        tookSpirit = false;
        SetInitialValuesRaft();
        CreateAnimation();
        animationMove.Play();
    }

    private void SetInitialValuesRaft()
    {
        resetAnimation = false;
        endRotation = new Vector3(0, 0, -maxRotationZ);
        raftTransform.rotation = Quaternion.Euler(0, 0, maxRotationZ);
        endOfRiverX = endOfRiver.position.x;
        startOfRiverX = raftTransform.position.x;
        riverLength = endOfRiverX - startOfRiverX;
    }

    private void OnValidate()
    {
        if (resetAnimation)
        {
            animationMove.Kill();
            animationRotate.Kill();
            raftTransform.position = new Vector3(startOfRiverX, raftTransform.position.y, raftTransform.position.z);
            raftTransform.rotation = Quaternion.Euler(0, 0, maxRotationZ);
            CreateAnimation();
            animationMove.Play();
            Debug.Log("Playing again animation");
        }

        Utility.DisableInspectorButton(() => resetAnimation = false).Play();
    }

    private void CreateAnimation()
    {
        animationMove = raftTransform.DOMoveX(endOfRiver.position.x, riverLength / speedMoveX);
        animationMove.SetLoops(-1, LoopType.Yoyo);
        animationMove.SetEase(Ease.InOutSine);

        animationRotate = raftTransform.DORotate(endRotation, maxRotationZ * 2 / rotationSpeed);
        animationRotate.SetLoops(-1, LoopType.Yoyo);
        animationRotate.SetEase(Ease.InOutSine);
    }
    

    private void Update()
    {
        if (!tookSpirit)
        {
            if (spirit == null)
            {
                tookSpirit = true;
                return;
            }
            if (!spirit.activeSelf)
            {
                if (playerTransform.position.x >= spirit.transform.position.x - distOffsetToEnableSpirit)
                {
                    spirit.SetActive(true);
                }    
            }    
        }
        
    }
}