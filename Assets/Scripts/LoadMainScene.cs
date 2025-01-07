using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadMainScene : MonoBehaviour
{
    [SerializeField] private GameObject playShake, loadShake;
    [SerializeField] private float rotateAmount, rotateTime;
    [SerializeField] private Button playButton, loadButton;
    [SerializeField] private BarFill barFill;
    [SerializeField] private float barMoveTime;
    private bool _hasLoadButton, _hasPlayButton = true;

    private void Awake()
    {
        playButton.onClick.AddListener(OnStartPlay);
        LeanTween.rotateZ(playShake, rotateAmount, rotateTime).setEase(LeanTweenType.easeInOutQuad).setLoopPingPong();
        LeanTween.rotateZ(loadShake, rotateAmount, rotateTime).setEase(LeanTweenType.easeInOutQuad).setLoopPingPong();
    }

    private void OnStartPlay()
    {
        LeanTween.moveLocalY(playShake, -1000, barMoveTime).setEase(LeanTweenType.easeInBack).setOnComplete(() => _hasPlayButton = false);
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
            if (asyncOperation.progress >= 0.9f && !_hasLoadButton && !_hasPlayButton)
            {
                _hasLoadButton = true;
                LeanTween.moveLocalY(loadShake, -100, barMoveTime).setEase(LeanTweenType.easeOutBack);
            }
            
            yield return null;
        }
    }
}
