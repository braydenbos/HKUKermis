using System.Collections;
using System.Collections.Generic;
using Ferr;
using Games;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class GameManager : Singleton<GameManager>
{
    private CameraMovement _mainCamera;
    private GameBooth _currentBooth;

    protected override void Awake()
    {
        base.Awake();
        if (Camera.main != null) _mainCamera = Camera.main.GetComponent<CameraMovement>();
    }

    public void EnterGame(GameBooth booth)
    {
        _currentBooth = booth;
        PlayerController.Instance.SetCanInteract(true);
        booth.OnEnterGame(_mainCamera);
    }

    public void ExitGame()
    {
        if (Camera.main != null) Camera.main.cullingMask |= 1 << LayerMask.NameToLayer("Player");
        PlayerController.Instance.SetCanInteract(false);
        _mainCamera.LookAtPlayer(() => Indicator.Instance.ShowSign(_currentBooth.Text));
    }
}
