using UnityEngine;

public class NotePad : MonoBehaviour
{
    private bool  _isActive, _canMove = true;
    [SerializeField] private float distance, moveTime;
    [SerializeField] private GameObject key;
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)||!_canMove) return;
        _isActive = !_isActive;
        _canMove = false;
        var destination = _isActive ? 0 : distance;
        LeanTween.moveLocalX(gameObject, destination, moveTime)
            .setEase(LeanTweenType.easeInOutExpo)
            .setOnComplete(() =>
            {
                _canMove = true;
                key.SetActive(false);
            });
    }
}
