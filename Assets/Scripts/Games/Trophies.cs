using System;
using System.Collections;
using UnityEngine;

namespace Games
{
    public class Trophies : GameBase
    {
        public bool _hasGun = true;
        private CameraMovement _mainCamera;
        [SerializeField] private GameBooth[] allBooths;
        [SerializeField] private ExitIndicator exitIndicator;

        private void Awake()
        {
            if (Camera.main != null) 
                _mainCamera = Camera.main.GetComponent<CameraMovement>();
        }

        public override void StartGame() => StartCoroutine(InShop());
        private void ShootTarget()
        {
            if (Camera.main == null) return;
            var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit)) return;
            if (!hit.collider.CompareTag("Target") || !hit.collider.gameObject.TryGetComponent(out Items targetHit)) return;
            targetHit.OnClick(this);
        }

        private IEnumerator InShop()
        {
            IsActive = true;
            PlayerController.Instance.OnShoot += ShootTarget;
            exitIndicator.EnterShop();
            
            while(!Input.GetKeyDown(KeyCode.E) && _hasGun)
            {
                yield return null;
            }
            
            exitIndicator.ExitShop();

            PlayerController.Instance.OnShoot -= ShootTarget;
            
            IsActive = false;
            GameManager.Instance.ExitGame();
            MouseController.Instance.OnGameExit();
            TimeMultiplier = 1;
            if (_hasGun) yield break;
            foreach (var booth in allBooths)
                Destroy(booth);
        }
    }
}
