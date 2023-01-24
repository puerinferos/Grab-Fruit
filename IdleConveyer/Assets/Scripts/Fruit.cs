using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private const float AccelarationSpeed = 2;
    private const float DeSpawnDelay = 3;
    [SerializeField] private FruitType type;

    private Rigidbody rb;
    public FruitType Type => type;

    private void Awake() =>
        rb = GetComponent<Rigidbody>();

    public void Grab(Transform handParent)
    {
        transform.SetParent(handParent);

        transform.DOLocalMove(Vector3.forward * .15f, .2f);
        transform.DORotate(Vector3.left * 90, .2f);

        rb.isKinematic = true;
    }

    public void DeSpawnWithDelay()
    {
        StartCoroutine(DeSpawnWithDelayCor());
    }

    private IEnumerator DeSpawnWithDelayCor()
    {
        yield return new WaitForSeconds(DeSpawnDelay);

        transform.DOScale(Vector3.zero, 1).SetEase(Ease.Linear).OnComplete(() =>
        {
            GlobalEvents.OnFruitNeedToDeSpawn?.Invoke(this);
        });
    }

    public void Move(Vector3 direction) =>
        rb.AddForce(direction * AccelarationSpeed, ForceMode.Acceleration);

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.layer == 3)
            DeSpawnWithDelay();
    }
}