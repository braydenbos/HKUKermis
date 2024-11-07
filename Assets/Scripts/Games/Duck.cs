using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Games
{
    public class Duck : GameBase
    {
        [SerializeField] private Ducky target;
        [SerializeField] private Transform startPoint,endPoint;
        
        [SerializeField] private float waitTime;
        [SerializeField] private float duckMoveTime;
        
        [SerializeField] private MinMax<int> score;
        private readonly List<Ducky> _duckies = new List<Ducky>();
        
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip duckClip;
    
        private void Awake() => OnEnterGame += () => StartCoroutine(GameLoop());

        private IEnumerator GameLoop()
        {
            PlayerController.Instance.OnShoot += ShootTarget;
            while (IsActive)
            {
                var item = Instantiate(target,startPoint);
                item.transform.localRotation = Quaternion.Euler(Vector3.zero);
                _duckies.Add(item);
                item.SetScore(Random.Range(score.min, score.max));
                LeanTween.move(item.gameObject,endPoint.position,duckMoveTime).setOnComplete(() => RemoveDucky(item));
                yield return new WaitForSeconds(waitTime);
            }

            EndGame();
        }

        public void EndGame()
        {
            PlayerController.Instance.OnShoot -= ShootTarget;

            foreach (var ducky in _duckies.ToList())
            {
                _duckies.Remove(ducky);
                Destroy(ducky.gameObject);   
            }
        }
        private void RemoveDucky(Ducky ducky) => _duckies.Remove(ducky);
    
        private void ShootTarget()
        {
            if (Camera.main == null) return;
            var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit)) return;
            if (!hit.collider.CompareTag("Target")|| !hit.collider.TryGetComponent(out Ducky ducky)) return;
            ducky.OnHit(this);
            audioSource.PlayOneShot(duckClip);
            PlayerController.Instance.OnShoot -= ShootTarget;
        }

    }
}
