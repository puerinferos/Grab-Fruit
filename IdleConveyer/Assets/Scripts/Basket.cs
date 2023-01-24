using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Basket : MonoBehaviour
{
    [SerializeField] private float duration = .5f;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float sphereRadius;

    private void Start()
    {
        GlobalEvents.OnGrabNeededItem += OnItemNeededGrabbed;
    }

    private void OnDestroy()
    {
        GlobalEvents.OnGrabNeededItem -= OnItemNeededGrabbed;
    }

    private void OnItemNeededGrabbed(Fruit fruit)
    {
        fruit.GetComponent<Rigidbody>().isKinematic = true;

        fruit.transform.DOScale(.5f, duration).OnComplete(() =>
        {
            fruit.transform.DOMove(spawnPoint.position + Random.insideUnitSphere * sphereRadius,duration);
            fruit.transform.SetParent(spawnPoint);
        });
    }
    private void OnDrawGizmos()
    {
        if (spawnPoint != null)
            Gizmos.DrawWireSphere(spawnPoint.transform.position,sphereRadius);
    }
}
