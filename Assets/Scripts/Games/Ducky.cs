using System.Collections;
using TMPro;
using UnityEngine;

namespace Games
{
    public class Ducky : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMesh;
        [SerializeField] private Color correctColor, incorrectColor;
        [SerializeField] private float moveTime;
        [SerializeField] private float scoreDisplayTime;
        [SerializeField] private Vector3 cameraOffset;
        [SerializeField] private Vector3 duckAtCameraRotation;
        private int _score;
        private bool _isHit;

        public void SetScore(int newScore)
        {
            _score = newScore;
            var positive = _score > -1;
            textMesh.text = positive ? $"+{_score}" : $"{_score}";
            textMesh.color = positive ? correctColor : incorrectColor;
        }
    
        public void OnHit(Duck other)
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
            LeanTween.cancel(gameObject);
            if (Camera.main != null) LeanTween.move(gameObject, Camera.main.transform.position+cameraOffset, moveTime).
                setEase(LeanTweenType.easeInOutExpo);
            LeanTween.rotate(gameObject, duckAtCameraRotation, moveTime * 1.5f)
                .setOnComplete(() => StartCoroutine(AddPoints(other)));
        }

        private IEnumerator AddPoints(Duck other)
        {
            var time = 0f;
            while (time < scoreDisplayTime)
            {
                time += Time.deltaTime;
                yield return null;
            }
            Destroy(gameObject);
            PointsSystem.Instance.ChangePoints(_score);
            other.SetTime(0);
            other.EndGame();
        }
    }
}
