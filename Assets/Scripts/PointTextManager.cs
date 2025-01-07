using System;
using UnityEngine;
using Color = UnityEngine.Color;

public class PointTextManager : MonoBehaviour
{
    [SerializeField] private PointText pointText; 
    [SerializeField] private Transform parentOnTop;
    private Action<int,int> _pointAction;
    private void Awake() => PointsSystem.Instance.AddPointsChangedListener(OnPointsChanged);
    
    private void OnPointsChanged(int points, int totalPoints)
    {
        var text = Instantiate(pointText, transform);
        var isPositive = points > -1;
        var textT = isPositive ? $"+{points}" : $"{points}";
        text.SetText(textT,isPositive,parentOnTop);
        text.StartJump();
        text.AddOnFinished(() => AddScore(points,totalPoints));
    }
    private void AddScore(int add,int max) => _pointAction.Invoke(add,max);
    public void AddListener(Action<int,int> action) => _pointAction = action;
}
