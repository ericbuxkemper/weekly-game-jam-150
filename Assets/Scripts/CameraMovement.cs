using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform objectToFollow;
    private Queue<Vector3> _targetPositions = new Queue<Vector3>();
    
    void Update()
    {
        transform.position = new Vector3(objectToFollow.transform.position.x, objectToFollow.transform.position.y, -10);
    }
}
