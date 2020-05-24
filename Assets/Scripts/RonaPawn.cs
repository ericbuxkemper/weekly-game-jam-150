using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RonaPawn : MonoBehaviour
{
    public float acceleration = .001f;
    public float maxAcceleration = .01f;
    private float currentUpVelocity = 0;
    private float currentRightVelocity = 0;

    private Vector2 _startingCameraLocation;
    private Vector2 _targetCameraLocation;
    private float _elapsedTime;

    private Rigidbody2D _rigidBody = null;
    
    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) currentUpVelocity += acceleration;
        if (Input.GetKey(KeyCode.A)) currentRightVelocity -= acceleration;
        if (Input.GetKey(KeyCode.S)) currentUpVelocity -= acceleration;
        if (Input.GetKey(KeyCode.D)) currentRightVelocity += acceleration;
        
        currentUpVelocity = Mathf.Clamp(currentUpVelocity, -maxAcceleration, maxAcceleration);
        currentRightVelocity = Mathf.Clamp(currentRightVelocity, -maxAcceleration, maxAcceleration);
        
        _rigidBody.AddForce(new Vector2(currentRightVelocity, currentUpVelocity));
        
        currentUpVelocity = Mathf.Clamp(currentUpVelocity / 2, -maxAcceleration, maxAcceleration);
        currentRightVelocity = Mathf.Clamp(currentRightVelocity / 2, -maxAcceleration, maxAcceleration);
    }
}
