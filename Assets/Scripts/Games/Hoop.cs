using UnityEngine;

namespace Games
{
    public class Hoop : GameBase
    {
        [SerializeField] private Rigidbody ball;
        [SerializeField] private Vector3 ballForce;
        [SerializeField] private float ballLifeSpan;
        
        [SerializeField] private BasketBallHoop hoop;
        [SerializeField] private MinMax<float> place;
        [SerializeField] private float hoopMoveTime;
        
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip basketBallClip;

        [SerializeField] private MinMax<float> pitch;
        private void Awake()
        {
            OnEnterGame += StartHoop;
            OnExitGame += StopGame;
        }

        private void StartHoop()
        {
            PlayerController.Instance.OnShoot += ShootTarget;
            hoop.transform.localPosition = new Vector3(place.min, 0, 0);
            LeanTween.moveLocalX(hoop.gameObject, place.max, hoopMoveTime).setLoopPingPong(-1);
        }

        private void StopGame()
        {
            PlayerController.Instance.OnShoot -= ShootTarget;
            LeanTween.cancel(hoop.gameObject);
        }

        private void ShootTarget()
        {
            if (Camera.main == null) return;
            
            var newBall = Instantiate(ball, Camera.main.transform.position, Quaternion.Euler(Vector3.zero));
            Destroy(newBall.gameObject, ballLifeSpan);
            
            audioSource.pitch = Random.Range(pitch.min, pitch.max);
            audioSource.PlayOneShot(basketBallClip);
            
            var y = Input.mousePosition.y - Screen.height / 2;
            var x = Input.mousePosition.x - Screen.width / 2;
            
            var force = new Vector3(x * ballForce.x, y * ballForce.y, ballForce.z);
            newBall.AddForce(force);
        }
    }
}

