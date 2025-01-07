using System;
using System.Collections;
using System.Collections.Generic;
using Games;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameBooth : MonoBehaviour
{
    [SerializeField] private GameBase game;
    [SerializeField] private Sprite mouseIcon;
    [SerializeField] private string signText = "PLAY";

    public string Text => signText;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent<PlayerController>(out var player)) return;
            player.SetBooth(this);
            Indicator.Instance.ShowSign(signText);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.TryGetComponent<PlayerController>(out var player) || !player.GetBooth() == this) return;
        player.SetBooth(null);
        Indicator.Instance.HideSign();
    }

    public void OnEnterGame(CameraMovement mainCamera)
    {
        Indicator.Instance.HideSign();
        mainCamera.LookAtGame(transform, OnInBooth);
    }

    private void OnInBooth()
    {
        if (Camera.main != null) Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("Player"));
        game.gameObject.SetActive(true);
        game.StartGame();
        MouseController.Instance.OnGameEnter(mouseIcon);
    }
}
