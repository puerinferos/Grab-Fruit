using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FruitPool
{
    private Queue<Fruit> fruits = new();
    private readonly Fruit prefab;

    public FruitPool(Fruit prefab)
    {
        this.prefab = prefab;
    }

    public Fruit Spawn(Vector3 position)
    {
        if (fruits.Count > 0)
        {
            Fruit neededFruit = fruits.Dequeue();
            neededFruit.gameObject.SetActive(true);
            neededFruit.transform.localScale = Vector3.one;
            neededFruit.transform.position = position;
            
            return neededFruit;
        }

        return GameObject.Instantiate(prefab, position, prefab.transform.rotation);
    }
    
    public void DeSpawn(Fruit fruit)
    {
        fruit.gameObject.SetActive(false);
        fruits.Enqueue(fruit);
    }
}