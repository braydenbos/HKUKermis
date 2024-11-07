using UnityEngine;

namespace Games
{
    public class Target : MonoBehaviour
    {
        [SerializeField] private float time;
        [SerializeField] private int points;

        public void MultiplyPoints(float multiply) => points = Mathf.RoundToInt(multiply * points);
        public void Jump(float height, float width)
        {
            LeanTween.moveLocalX(gameObject, width, time);

            LeanTween.moveLocalY(gameObject, height, time / 2).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
                LeanTween.moveLocalY(gameObject, 0, time / 2).setEase(LeanTweenType.easeInExpo)
                    .setOnComplete(() =>
                    {
                        if (gameObject != null) Destroy(gameObject);
                    }));
        }
    
        public void OnHit()
        {
            Destroy(gameObject);
            PointsSystem.Instance.ChangePoints(points);
        }
    }
}
