using System;
using TMPro;
using UnityEngine;

namespace Games
{
    public class Items : MonoBehaviour
    {
        [SerializeField] private float speedMultiplier = 1;
        [SerializeField] private float reloadsSpeedMultiplier = 1;
        [SerializeField] private float pointMultiplier = 1;
        [SerializeField] private bool clownNoise = false;
        
        [SerializeField] protected GameObject offObject;
        [SerializeField] protected GameObject onObject;
        
        [SerializeField] protected int cost;
        [SerializeField] private TextMeshProUGUI costText;
        
        [SerializeField] protected AudioSource audioSource;

        private void Awake()
        {
            var text = cost.ToString();
            var replace = text.Replace('-', '+');
            costText.text = replace;
        }

        public virtual void OnClick(Trophies other)
        {
            if (cost > PointsSystem.Instance.GetPoints()) return;
            audioSource.Play();
            
            PointsSystem.Instance.ChangePoints(-cost);
            
            offObject.SetActive(false);
            onObject.SetActive(true);
            
            PlayerController.Instance.Upgrade(speedMultiplier, reloadsSpeedMultiplier, clownNoise);
            PointsSystem.Instance.SetMultiplier(pointMultiplier);
        }
    }
}
