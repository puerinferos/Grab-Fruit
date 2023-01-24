using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private RightHandController rightHandController;

    private Animator animator;
    private Camera camera;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        camera = Camera.main;
        GlobalEvents.OnTargetReached += OnTargetReached;
    }

    private void OnDestroy()
    {
        GlobalEvents.OnTargetReached -= OnTargetReached;
    }

    private void OnTargetReached()
    {
        SetHandNotInteractable();
        animator.SetBool(AnimatorConstants.FinishLevel,true);

        enabled = false;
    }

    public void Initialize(FruitType neededType)
    {
        rightHandController.Initialize(neededType,animator);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out var hit))
            {
                if(EventSystem.current.IsPointerOverGameObject())
                    return;

                if (hit.transform.GetComponent<ConveyerInteractablePart>() || hit.transform.GetComponent<Fruit>())
                    TryToGrab(hit.point);
            }
        }
    }

    private void TryToGrab(Vector3 hitPoint)
    {
        targetTransform.position = new Vector3(hitPoint.x,.8f,hitPoint.z);
        animator.SetTrigger(AnimatorConstants.Grab);
    }

    public void SetHandInteractable() => 
        rightHandController.ChangeInteractable(true);
    
    public void SetHandNotInteractable() => 
        rightHandController.ChangeInteractable(false);

    public void LooseFruitFromHand() =>
        rightHandController.LooseFruit();   
}