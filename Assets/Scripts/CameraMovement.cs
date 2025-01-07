using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private float distance;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveToGameTime;
    [SerializeField] private Vector3 offset;
    private float _rotationX;
    private Vector2 _startPosition;
    private Coroutine _moving;

    private void Awake()
    {
        Cursor.visible = false;
        _rotationX = transform.eulerAngles.x;
        _startPosition = new Vector2(transform.position.y,transform.position.z);
        player.OnWalk += OnWalkEvent;
    }

    private void OnWalkEvent()
    {
        if (Mathf.Abs(player.transform.position.x - transform.position.x) > distance)
            _moving = StartCoroutine(GoToPlayer());
    }

    private IEnumerator GoToPlayer()
    {
        player.OnWalk -= OnWalkEvent;
        while (Mathf.Abs(player.transform.position.x - transform.position.x) > distance)
        {
            var destination = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime * (Mathf.Abs(player.transform.position.x - transform.position.x)-distance));
            yield return null;
        }
        player.OnWalk += OnWalkEvent;
    }

    public void LookAtGame(Transform target, Action oncomplete)
    {
        LeanTween.rotateX(gameObject, 8, moveToGameTime).setEase(LeanTweenType.easeInOutExpo);
        LeanTween.move(gameObject, target.position + offset, moveToGameTime).setEase(LeanTweenType.easeInOutExpo).
            setOnComplete(oncomplete.Invoke);
        
        if (_moving == null) return;
        StopCoroutine(_moving);
        player.OnWalk += OnWalkEvent;

    }

    public void LookAtPlayer(Action oncomplete)
    {
        LeanTween.rotateX(gameObject, _rotationX, moveToGameTime).setEase(LeanTweenType.easeInOutExpo);
        LeanTween.moveY(gameObject, _startPosition.x, moveToGameTime).setEase(LeanTweenType.easeInOutExpo);
        LeanTween.moveZ(gameObject, _startPosition.y, moveToGameTime).setEase(LeanTweenType.easeInOutExpo).
            setOnComplete(oncomplete.Invoke);
    }
}