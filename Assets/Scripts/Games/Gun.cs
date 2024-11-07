using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Games
{
    public class Gun : GameBase
    {
        [SerializeField] private Target target;
        [SerializeField] private Transform targetParent, lineParent;
        
        [SerializeField] private MinMax<float> place;
        [SerializeField] private MinMax<float> height;
        [SerializeField] private MinMax<float> size;
        
        [SerializeField] private float waitTime;
        
        [SerializeField] private LineRenderer lineRenderer;
        
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip shootClip,targetClip,hitClip;

        private void Awake() => OnEnterGame += () => StartCoroutine(GameLoop());

        private IEnumerator GameLoop()
        {
            PlayerController.Instance.OnShoot += ShootTarget;
            while (IsActive)
            {
                audioSource.PlayOneShot(targetClip);
                var item = Instantiate(target,targetParent);
                var scale = Random.Range(size.min, size.max);
                
                item.MultiplyPoints(size.max/scale);
                item.transform.localScale = new Vector3(scale,scale,scale);
                item.transform.localPosition = new Vector3(Random.Range(place.min,place.max), 0, 0);
                
                item.Jump(Random.Range(height.min,height.max),Random.Range(place.min,place.max));
                yield return new WaitForSeconds(waitTime);
            }
            PlayerController.Instance.OnShoot -= ShootTarget;

        }

        private void ShootTarget()
        {
            var gunLine = Instantiate(lineRenderer, lineParent);
            Destroy(gunLine.gameObject,1f);
            gunLine.SetPosition(0,lineParent.position);
            audioSource.PlayOneShot(shootClip);
            
            if (Camera.main == null) return;
            var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            
            if (!Physics.Raycast(ray, out var hit)) return;
            gunLine.SetPosition(1,hit.point);
            
            if (!hit.collider.CompareTag("Target") || !hit.collider.gameObject.TryGetComponent(out Target targetHit)) return;
            audioSource.PlayOneShot(hitClip);
            targetHit.OnHit();
        }
    }
}
