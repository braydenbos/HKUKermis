using System;
using System.Collections.Generic;
using Ferr;

public class PointsSystem : Singleton<PointsSystem>
{
    private int _points;
    private float _pointMultiplier = 1;
    public void SetMultiplier(float pointMultiplier) => _pointMultiplier *= pointMultiplier;
    
    public void ChangePoints(int points)
    {
        var newPoints = points > 0 ? (int)Math.Round(points * _pointMultiplier):points;
        _points += newPoints;
        if (_points < 0) _points = 0;
        foreach (var events in _onPointsChanged)
            events?.Invoke(newPoints, _points);
        
    } 
    public int GetPoints() => _points;
    
    private readonly List<Action<int,int>> _onPointsChanged = new List<Action<int,int>>();
    
    public void AddPointsChangedListener(Action<int,int> listener) => _onPointsChanged.Add(listener);
    
    public void RemovePointsChangedListener(Action<int,int> listener) => _onPointsChanged.Remove(listener);
}
