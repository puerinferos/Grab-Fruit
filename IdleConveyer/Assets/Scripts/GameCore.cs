using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace DefaultNamespace
{
    public class GameCore : MonoBehaviour
    {
        [SerializeField] private UI ui;
        [SerializeField] private Conveyer conveyer;
        [SerializeField] private Player player;
        [Space] 
        [SerializeField] private Vector2Int minMaxFruitNeeded;
        [SerializeField] private List<Fruit> allFruit = new();
        [SerializeField] private Transform finishCameraPosition;

        private int neededFruitCount;
        private int currentFruitNeeded;

        private PoolManager poolManager;
        private FruitType currentNeededFruit;

        private void Start()
        {
            Initialize();
            ui.Initialize(neededFruitCount,currentNeededFruit);
            player.Initialize(currentNeededFruit);
            
            GlobalEvents.OnFruitNeedToDeSpawn += OnFruitNeedToDeSpawn;
            GlobalEvents.GameStarted += StartGame;

        }

        private void OnDestroy()
        {
            GlobalEvents.OnFruitNeedToDeSpawn -= OnFruitNeedToDeSpawn;
            GlobalEvents.GameStarted -= StartGame;
            GlobalEvents.OnGrabNeededItem -= OnFruitGrabbed;
        }

        private void StartGame() =>
            StartCoroutine(SpawnFruits());

        private void Initialize()
        {
            FruitType[] fruitTypes = (FruitType[])Enum.GetValues(typeof(FruitType));

            currentNeededFruit = fruitTypes[Random.Range(0, fruitTypes.Length)];

            neededFruitCount = Random.Range(minMaxFruitNeeded.x, minMaxFruitNeeded.y);
            currentFruitNeeded = neededFruitCount;

            poolManager = new PoolManager(allFruit);

            GlobalEvents.OnGrabNeededItem += OnFruitGrabbed;
        }

        private void OnFruitNeedToDeSpawn(Fruit fruit) =>
            poolManager.DeSpawn(fruit);

        private void OnFruitGrabbed(Fruit fruit)
        {
            currentFruitNeeded--;
            if (currentFruitNeeded <= 0)
            {
                GlobalEvents.OnTargetReached?.Invoke();
                Camera.main.transform.DOMove(finishCameraPosition.position, 1);
                StopAllCoroutines();
            }
        }

        private IEnumerator SpawnFruits()
        {
            while (true)
            {
                GenerateRandomFruit(conveyer.SpawnPosition);

                yield return new WaitForSeconds(1);
            }
        }

        public void GenerateRandomFruit(Vector3 position)
        {
            FruitType[] allTypes = (FruitType[])Enum.GetValues(typeof(FruitType));
            int randomIndex = Random.Range(0, allTypes.Length);

            FruitType randomType = allTypes[randomIndex];
            Debug.Log($"gamecore spawn random {randomType.ToString()}");
            poolManager.Spawn(randomType, position);
        }
    }
}