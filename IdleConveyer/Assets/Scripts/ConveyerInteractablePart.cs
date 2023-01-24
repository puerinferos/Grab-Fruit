using UnityEngine;

public class ConveyerInteractablePart : MonoBehaviour
{
    private void OnCollisionStay(Collision collisionInfo)
    {
        var fruit = collisionInfo.transform.GetComponent<Fruit>();
        
        if (fruit != null)
            fruit.Move(-transform.right);
    }
}