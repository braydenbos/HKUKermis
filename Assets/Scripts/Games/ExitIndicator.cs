using System;
using UnityEngine;

namespace Games
{
    public class ExitIndicator : MonoBehaviour
    {
        [SerializeField] private float signTime;
        [SerializeField] private float topPlace;
        [SerializeField] private float rotation;
        [SerializeField] private float rotationTime;

        private void Awake()
        {
            LeanTween.rotateZ(gameObject, rotation, rotationTime).setLoopPingPong();
        }

        public void EnterShop() => LeanTween.moveLocalY(gameObject, 0, signTime).setEase(LeanTweenType.easeInOutExpo);
        
        public void ExitShop() => LeanTween.moveLocalY(gameObject, topPlace, signTime).setEase(LeanTweenType.easeInOutExpo);
    }
}
