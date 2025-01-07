using System;
using UnityEngine;

namespace Games
{
    public class FinalGun : Items
    {
        [SerializeField] private EndGame endGame;
        public Action OnCollect;
        public override void OnClick(Trophies other)
        {
            if (cost > PointsSystem.Instance.GetPoints()) return;
            other.hasGun = false;
            base.OnClick(other);
            PlayerController.Instance.SetCanInteract(true);
            endGame.EndGameStart();
            OnCollect?.Invoke();
        }
    }
}