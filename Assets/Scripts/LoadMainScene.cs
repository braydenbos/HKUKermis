using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadMainScene : MonoBehaviour
{
    [SerializeField] private Button playButton, loadButton;
    [SerializeField] private BarFill barFill;
    [SerializeField] private float barMoveTime;
    private bool _hasLoadButton;

    private void Awake()
    {
        playButton.onClick.AddListener(OnStartPlay);
    }

    private void OnStartPlay()
    {
        LeanTween.moveLocalY(playButton.gameObject, -1000, barMoveTime).setEase(LeanTweenType.easeInBack);
        barFill.BarStatus(true);
        StartCoroutine(LoadScene());
    }

    private void LoadInScene(AsyncOperation asyncOperation) => asyncOperation.allowSceneActivation = true;

    private IEnumerator LoadScene()
    {
        yield return null;

        //Begin to load the Scene you specify
        var asyncOperation = SceneManager.LoadSceneAsync("MainMap");
        loadButton.onClick.AddListener(() => LoadInScene(asyncOperation));
        //Don't let the Scene activate until you allow it to
        if (asyncOperation == null) yield break;
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            barFill.Fill(asyncOperation.progress);

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f && !_hasLoadButton)
            {
                _hasLoadButton = true;
                LeanTween.moveLocalY(loadButton.gameObject, -100, barMoveTime).setEase(LeanTweenType.easeOutBack);
            }
            
            yield return null;
        }
    }
}
