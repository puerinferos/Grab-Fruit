using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerScoreCounter : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> pointCounters;
    [SerializeField] private Vector2 minMaxPointerPositionX;
    [SerializeField] private float duration;
    [SerializeField] private float distance;
    
    private int index;
    private Vector3 startPosition;

    private void Start()
    {
        GlobalEvents.OnGrabNeededItem += ShowCounter;
    }

    private void OnDestroy()
    {
        GlobalEvents.OnGrabNeededItem -= ShowCounter;
    }

    private void ShowCounter(Fruit fruit)
    {
        var startRandomPosition = Vector3.right * Random.Range(minMaxPointerPositionX.x, minMaxPointerPositionX.y);
        var neededEndPosition = pointCounters[index].transform.localPosition.y + distance;
        
        pointCounters[index].alpha = 1;
        
        pointCounters[index].transform.localPosition = startPosition + startRandomPosition;
        pointCounters[index].transform.DOLocalMoveY(neededEndPosition, duration);
        pointCounters[index].DOFade(0, duration);

        index = index >= pointCounters.Count-1 ? 0 : index + 1;
    }
}
