using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarFill : MonoBehaviour
{
    [SerializeField] private RectTransform barFill;
    [SerializeField] private float up;
    [SerializeField] private float moveTime;
    private float _barFillAmount;
    
    [SerializeField] private float rotation;
    [SerializeField] private float rotationTime;

    
    private void Awake()
    {
        LeanTween.rotateZ(gameObject, rotation, rotationTime).setLoopPingPong();
        _barFillAmount = gameObject.GetComponent<RectTransform>().rect.width * 2;
    }

    public void BarStatus(bool isActive)
    {
        var detestation = isActive ? 0 : up;
        LeanTween.moveLocalY(gameObject, detestation, moveTime).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() => Fill(0));
    }

    public void Fill(float percent)
    {
        barFill.sizeDelta = new Vector2(_barFillAmount * percent,1);
    }
}
