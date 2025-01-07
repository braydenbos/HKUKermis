using System;
using Games;
using TMPro;
using UnityEngine;

public class NotePad : MonoBehaviour
{
    private bool  _isActive, _canMove = true;
    [SerializeField] private float distance, moveTime;
    [SerializeField] private GameObject key,arrow;
    [SerializeField] private float arrowTime, arrowDistance;
    [SerializeField] private TMP_Text text;
    [SerializeField] private FinalGun gun;

    private void Awake()
    {
        gun.OnCollect += () => text.text = $"<s>{text.text}";
        LeanTween.moveLocalX(arrow,arrowDistance-20,arrowTime).setEase(LeanTweenType.easeInOutExpo).setLoopPingPong();
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.F)||!_canMove) return;
        _isActive = !_isActive;
        _canMove = false;
        var destination = _isActive ? 0 : distance;
        LeanTween.moveLocalX(gameObject, destination, moveTime)
            .setEase(LeanTweenType.easeInOutExpo)
            .setOnComplete(() => _canMove = true);
        LeanTween.moveLocalX(key, 1000, moveTime)
            .setEase(LeanTweenType.easeInOutExpo);
    }
}
