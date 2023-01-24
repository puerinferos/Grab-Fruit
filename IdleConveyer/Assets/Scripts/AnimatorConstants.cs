using System;
using Unity.VisualScripting;

public static class AnimatorConstants
{
    public static readonly string Grab = "Grab";
    public static readonly string GrabFruit = "GrabbedFruit";
    public static readonly string ThrowOutOrToBasket = "FruitNeeded"; // true to basket
    public static readonly string FinishLevel = "FinishLevel";
}

public static class GlobalEvents
{
    public static Action GameStarted;
    public static Action<Fruit> OnGrabNeededItem;
    public static Action OnTargetReached;
    public static Action<Fruit> OnFruitNeedToDeSpawn;
}