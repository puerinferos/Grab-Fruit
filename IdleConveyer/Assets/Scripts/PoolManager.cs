using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    private Dictionary<FruitType, FruitPool> pools = new();

    public PoolManager(List<Fruit> allFruits)
    {
        foreach (Fruit fruit in allFruits)
        {
            var pool = new FruitPool(fruit);
            pools.Add(fruit.Type,pool);
        }
    }

    public Fruit Spawn(FruitType type,Vector3 position)
    {
        Debug.Log($"poolmanager spawn");
        return pools[type].Spawn(position);
    }

    public void DeSpawn(Fruit fruit)
    {
        pools[fruit.Type].DeSpawn(fruit);
    }
}