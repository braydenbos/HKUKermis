using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Games
{
    public class Shovel : GameBase
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private float lineSpeed;
        [SerializeField] private Vector3 lineOffset,targetLineOffset;
        
        [SerializeField] private float waitTime;
        
        [SerializeField] private TextMeshProUGUI distanceText;
        [SerializeField] private int distanceModifier;
        
        [SerializeField] private MinMax<Vector2> targetRange;
        [SerializeField] private GameObject target;

        [SerializeField] private int maxPoints = 300;
        
        [SerializeField] private AudioSource audioSource;
        private void Awake()
        {
            OnEnterGame += GameStart;
            OnExitGame += GameEnd;
        }

        private void GameStart()
        {
            PlayerController.Instance.OnShoot += ShootTarget;
            target.transform.localPosition = new Vector3(
                Random.Range(targetRange.min.x, targetRange.max.x), 
                0.5f,
                Random.Range(targetRange.min.y, targetRange.max.y));
        }

        private void GameEnd() => PlayerController.Instance.OnShoot -= ShootTarget;


        private void ComparePoints(Vector3 other)
        {
            var distance = (int)Math.Round(Vector3.Distance(target.transform.position, other) * distanceModifier);
            StartCoroutine(DrawLine(other, distance));
        }

        private IEnumerator DrawLine(Vector3 other, int deduction)
        {
            lineRenderer.gameObject.SetActive(true);
            lineRenderer.SetPosition(0,other+lineOffset);
            lineRenderer.SetPosition(1,other+lineOffset);
            var distance = 0f;
            while (distance < 1)
            {
                distance += Time.deltaTime * lineSpeed;
                
                lineRenderer.SetPosition(1,Vector3.Lerp(other+lineOffset,target.transform.position+targetLineOffset,Mathf.Clamp01(distance)));
                yield return null;
            }
            audioSource.Stop();
            
            target.SetActive(true);
            
            PointsSystem.Instance.ChangePoints(maxPoints - deduction);

            distanceText.gameObject.SetActive(true);
            distanceText.transform.position = Vector3.Lerp(other,target.transform.position,0.5f);
            distanceText.transform.localPosition = new Vector3(distanceText.transform.localPosition.x,distanceText.transform.localPosition.y,-0.1f);
            distanceText.text = deduction.ToString();
            
            yield return new WaitForSeconds(waitTime);

            SetTime(0);
            target.SetActive(false);
            distanceText.gameObject.SetActive(false);
            lineRenderer.gameObject.SetActive(false);
        }
        private void ShootTarget()
        {
            audioSource.Play();
            if (Camera.main == null) return;
            var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit)) return;
            if (!hit.collider.CompareTag("Target")) return;
            ComparePoints(hit.point);
            PlayerController.Instance.OnShoot -= ShootTarget;

        }

    }
}
