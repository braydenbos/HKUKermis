using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PointText : MonoBehaviour
{
    [Serializable]
    private struct Text
    {
        public Color color;
        public TMP_FontAsset font;
    }
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float jumpHeight, jumpTime;
    [SerializeField] private float fadeTime;
    [SerializeField] private Transform parentOnTop;
    [SerializeField] private Text negativeText, normalText;
    private Action _onFinished;

    public void AddOnFinished(Action onFinished) => _onFinished = onFinished;
    public void SetText(string newText,bool positive,Transform parent)
    {
        text.color = positive ? normalText.color : negativeText.color;
        text.font = positive ? normalText.font : negativeText.font;
        text.text = newText;
        parentOnTop = parent;
    }

    public void StartJump()
    {
        LeanTween.moveLocalY(text.gameObject, jumpHeight, jumpTime/2).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
        {
            LeanTween.moveLocalY(text.gameObject, 0, jumpTime/2).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() => StartCoroutine(FadeOut()));
            transform.SetParent(parentOnTop);
        });
    }

    private IEnumerator FadeOut()
    {
        var time = 0f;
        while (time<fadeTime)
        {
            time += Time.deltaTime;
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1 - time / fadeTime);
            yield return null;
        }
        Destroy(gameObject);
        _onFinished?.Invoke();
    }
}
