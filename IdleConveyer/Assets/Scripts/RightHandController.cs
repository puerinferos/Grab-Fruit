using DG.Tweening;
using UnityEngine;

public class RightHandController : MonoBehaviour
{
    [SerializeField] private Fruit currentFruit;
    private int fruitCount;

    private FruitType neededFruitType;
    private Animator playerAnimator;

    private bool isInteractable;

    public void ChangeInteractable(bool interactable) =>
        isInteractable = interactable;

    private void OnTriggerEnter(Collider other)
    {
        var fruit = other.GetComponent<Fruit>();
        if (fruit == null || fruit == currentFruit || fruitCount > 0 || !isInteractable)
            return;
        playerAnimator.SetBool(AnimatorConstants.GrabFruit,true);
        playerAnimator.SetBool(AnimatorConstants.ThrowOutOrToBasket, fruit.Type == neededFruitType);

        currentFruit = fruit;
        fruit.Grab(transform);
        fruitCount++;
    }

    public void Initialize(FruitType neededFruitType, Animator playerAnimator)
    {
        this.neededFruitType = neededFruitType;
        this.playerAnimator = playerAnimator;
    }

    public void LooseFruit()
    {
        if (currentFruit == null)
            return;
        currentFruit.transform.SetParent(null);
        currentFruit.GetComponent<Rigidbody>().isKinematic = false;

        if (currentFruit.Type == neededFruitType)
        {
            GlobalEvents.OnGrabNeededItem?.Invoke(currentFruit);
        }
        else
        {
            currentFruit.GetComponent<Rigidbody>().AddForce(Vector3.forward * 5, ForceMode.Impulse);
            currentFruit.DeSpawnWithDelay();
        }

        currentFruit = null;
        fruitCount--;
        
        playerAnimator.SetBool(AnimatorConstants.GrabFruit,false);
        playerAnimator.SetBool(AnimatorConstants.ThrowOutOrToBasket,false);

    }
}