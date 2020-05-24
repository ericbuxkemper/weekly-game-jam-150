using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform objectToFollow;
    private Queue<Vector3> _targetPositions = new Queue<Vector3>();
    
    void Update()
    {
        // transform.position = new Vector3(objectToFollow.transform.position.x, objectToFollow.transform.position.y, -10);
        if (_targetPositions.Count > 0 && Vector3.Distance(_targetPositions.Peek(), transform.position) > .1)
            _targetPositions.Enqueue(objectToFollow.transform.position);

        if (_targetPositions.Count < 10) return;
        transform.position = _targetPositions.Dequeue();
        // var distanceSum = Vector3.zero;
        // foreach (var futurePosition in _targetPositions) distanceSum += futurePosition;

        // var avgDistance = distanceSum / _targetPositions.Count;
        // transform.position = Vector3.Lerp(new Vector3(avgDistance.x, avgDistance.y, -10)
        // _targetPositions.Dequeue();

    }
}
