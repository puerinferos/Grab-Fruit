using System;
using System.Collections;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Conveyer : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    [SerializeField] private float finishPositionX;
    public Vector3 SpawnPosition => startPos.position;

    private void Start()
    {
        GlobalEvents.OnTargetReached += OnTargetReached;
    }

    private void OnDestroy()
    {
        GlobalEvents.OnTargetReached -= OnTargetReached;
    }

    private void OnTargetReached()
    {
        transform.DOMoveX(finishPositionX, .7f).SetEase(Ease.InBack);
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        var fruit = collisionInfo.transform.GetComponent<Fruit>();
        if (fruit != null)
            fruit.Move(-transform.right);
    }
}