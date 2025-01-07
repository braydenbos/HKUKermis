using System;
using Ferr;
using TMPro;
using UnityEngine;

namespace Games
{
    public class Indicator : Singleton<Indicator>
    {
        [SerializeField] private float signTime;
        [SerializeField] private float topPlace;
        [SerializeField] private float rotation;
        [SerializeField] private float rotationTime;
        [SerializeField] private TMP_Text text,textShadow;

        protected override void Awake()
        {
            base.Awake();
            LeanTween.rotateZ(gameObject, rotation, rotationTime).setLoopPingPong();
        }
        

        public void ShowSign(string newText)
        {
            text.text = newText;
            textShadow.text = newText;
            LeanTween.moveLocalY(gameObject, 0, signTime).setEase(LeanTweenType.easeInOutExpo);
        }

        public void HideSign() => LeanTween.moveLocalY(gameObject, topPlace, signTime).setEase(LeanTweenType.easeInOutExpo);
    }
}
