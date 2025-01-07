using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [Serializable]
    private struct Text
    {
        public Color color;
        public TMP_FontAsset font;
    }
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Text thousandText, normalText;
    [SerializeField] private PointTextManager pointTextManager;
    [SerializeField] private float scoreTimer;
    private float _displayScore;

    private void Awake() => pointTextManager.AddListener(AddScore);
    private void AddScore(int points, int totalPoints) => StartCoroutine(OnPointsChanged(points));
    
    private IEnumerator OnPointsChanged(int points)
    {
        var time = 0f;
        var currentScore = 0f;
        while (time < scoreTimer && _displayScore >= 0)
        {
            time += Time.deltaTime;
            var current = Mathf.Lerp(0, points, time/scoreTimer) - currentScore;
            currentScore += current;
            _displayScore += current;
            scoreText.text = $"{_displayScore:0}";
            scoreText.color = _displayScore > 999 ? thousandText.color : normalText.color;
            scoreText.font = _displayScore > 999 ? thousandText.font : normalText.font;
            yield return null;
        }

        if (!(_displayScore < 0)) yield break;
        _displayScore = 0;
        scoreText.text = $"{_displayScore:0}";
    }
}
