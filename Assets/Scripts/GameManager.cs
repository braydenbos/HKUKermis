using System.Collections;
using System.Collections.Generic;
using Ferr;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class GameManager : Singleton<GameManager>
{
    private CameraMovement _mainCamera;

    protected override void Awake()
    {
        base.Awake();
        if (Camera.main != null) _mainCamera = Camera.main.GetComponent<CameraMovement>();
    }

    public void EnterGame(GameBooth booth)
    {
        PlayerController.Instance.SetCanInteract(true);
        booth.OnEnterGame(_mainCamera);
    }

    public void ExitGame()
    {
        PlayerController.Instance.SetCanInteract(false);
        _mainCamera.lookAtPlayer();
    }
}
