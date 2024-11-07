using System.Collections;
using System.Collections.Generic;
using Ferr;
using UnityEngine;
using UnityEngine.UI;

public class MouseController : Singleton<MouseController>
{
    [SerializeField] private Transform mouse;
    private Image _mouseImage;
    private bool _inGame = false;
    private Vector3 _startPos;

    protected override void Awake()
    {
        _startPos = mouse.position;
        base.Awake();
        _mouseImage = mouse.GetComponent<Image>();
    }

    public void OnGameEnter(Sprite icon)
    {
        _inGame = true;
        _mouseImage.sprite = icon;
        StartCoroutine(InGame());
    }

    public void OnGameExit()
    {
        _inGame = false;
        _mouseImage.sprite = null;
    }

    private IEnumerator InGame()
    {
        while (_inGame)
        {
            mouse.position = Input.mousePosition;
            yield return null;
        }
        mouse.position = _startPos;
    }
}
