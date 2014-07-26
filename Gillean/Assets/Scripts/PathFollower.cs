using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFollower : MonoBehaviour
{
    public enum MovementType
    {
        MoveToward,
        Lerp
    }

    public MovementType Type = MovementType.MoveToward;
    private IEnumerator<Transform> _currentPoint;
    public Path2D SelectedPath;
    public float Speed = 1;
    public float MaxDistanceToRegisterTouch = .1f;
    public void Start()
    {
        if (SelectedPath == null)
        {
            Debug.Log("Path2D is null", gameObject);
            return;
        }
        _currentPoint = SelectedPath.GetPathPointsEnumerator();
        _currentPoint.MoveNext();

        if (_currentPoint.Current == null)
            return;

        transform.position = _currentPoint.Current.position;
    }

    public void Update()
    {
        if(_currentPoint == null || _currentPoint.Current == null)
            return;
        
        
        if (Type == MovementType.MoveToward)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentPoint.Current.position,
                Time.deltaTime * Speed);
        }
        if (Type == MovementType.Lerp)
        {
            transform.position = Vector3.Lerp(transform.position, _currentPoint.Current.position,
                Time.deltaTime * Speed);
        }

        var distanceSquared = (transform.position - _currentPoint.Current.position).sqrMagnitude;
        if (distanceSquared < MaxDistanceToRegisterTouch*MaxDistanceToRegisterTouch)
        {
            _currentPoint.MoveNext();
        }
    }

}
