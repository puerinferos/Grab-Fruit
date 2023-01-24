using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Slider loadingBar;
    [SerializeField] private Button tapToPlayBtn;
    
    [SerializeField] private TMP_Text targetTxt;
    [SerializeField] private GameObject finishScreen;

    private int initialFruitsNeeded;
    private int fruitsNeededToCollect;

    private void Start()
    {
        GlobalEvents.OnGrabNeededItem += OnGrabNeededItem;
        GlobalEvents.OnTargetReached += ShowFinishScreen;
        
        tapToPlayBtn.onClick.AddListener(StartGame);
    }

    private void OnDestroy()
    {
        GlobalEvents.OnGrabNeededItem -= OnGrabNeededItem;
        GlobalEvents.OnTargetReached -= ShowFinishScreen;
    }

    private void StartGame()
    {
        GlobalEvents.GameStarted?.Invoke();
        
        tapToPlayBtn.gameObject.SetActive(false);
        targetTxt.gameObject.SetActive(true);
        loadingBar.gameObject.SetActive(true);
    }

    private void ShowFinishScreen()
    {
        targetTxt.gameObject.SetActive(false);
        loadingBar.gameObject.SetActive(false);
        
        finishScreen.SetActive(true);
    }

    public void NextLevel()
    {
        Debug.Log($"need to load next level");
        SceneManager.LoadScene(0);
    }

    public void Initialize(int countNeeded,FruitType fruitTypeNeeded)
    {
        fruitsNeededToCollect = countNeeded;
        initialFruitsNeeded = countNeeded;
        
        loadingBar.maxValue = countNeeded;
        targetTxt.text = $"Collect {countNeeded} {fruitTypeNeeded}s.";
    }

    private void OnGrabNeededItem(Fruit fruit)
    {
        --fruitsNeededToCollect;
        loadingBar.DOValue(initialFruitsNeeded-fruitsNeededToCollect, .3f).SetEase(Ease.OutBack);
    }
}
