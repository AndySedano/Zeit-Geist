using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    float angle;

    Vector3 velocity;

    Rigidbody rigidBody;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
		rigidBody.MovePosition(velocity * Time.fixedDeltaTime + rigidBody.position);
    }

    

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
        float quickangle = (Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg) - 270f;
        if (velocity.x!=0f || velocity.z!=0f)
        {
            angle = quickangle;
        }
        transform.rotation = Quaternion.Euler(0f, angle, 0f) ;
    }
}
