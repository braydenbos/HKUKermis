using UnityEngine;

namespace Games
{
    public class BasketBallHoop : MonoBehaviour
    {
        [SerializeField] private int points;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Target")) return;
            PointsSystem.Instance.ChangePoints(points);
            Destroy(other.gameObject);
        }
    }
}
