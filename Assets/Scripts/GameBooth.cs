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
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out var player))
            player.SetBooth(this);
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out var player) && player.GetBooth() == this)
            player.SetBooth(null);
    }

    public void OnEnterGame(CameraMovement mainCamera)
    {
        mainCamera.LookAtGame(transform, OnInBooth);
    }

    private void OnInBooth()
    {
        game.gameObject.SetActive(true);
        game.StartGame();
        MouseController.Instance.OnGameEnter(mouseIcon);
    }
}
