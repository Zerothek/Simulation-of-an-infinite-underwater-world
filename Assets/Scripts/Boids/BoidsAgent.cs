using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BoidsAgent : MonoBehaviour
{
    Vector3 velocity;
    public Vector3 Velocity { get { return velocity; } }

    Vector3 acceleration;
    public Vector3 Acceleration { get { return acceleration; } }

    Collider agentCollider;
    public Collider AgentCollider { get { return agentCollider; } }

    // Start is called before the first frame update
    void Start()
    {

        // Reference to the agent's collider
        agentCollider = GetComponent<Collider>();

        // Set initial acceleration
        SetAcceleration(transform.forward);

        // Set initial velocity
        SetVelocity(5 * transform.forward);

    }

    public void moveBoid(float minimumSpeed, float maximumSpeed)
    {

        // Calculate the new velocity based on the current acceleration
        velocity += acceleration * Time.deltaTime;

        // Set the speed to be between [minimumSpeed, maximumSpeed]
        float speed = velocity.magnitude;
        Vector3 normalized_velocity = velocity / speed;
        speed = Mathf.Clamp(speed, minimumSpeed, maximumSpeed);
        velocity = normalized_velocity * speed;

        // Set the new orientation of the boid
        transform.forward = normalized_velocity;

        // Calculate the new position of the boid
        transform.position += velocity * Time.deltaTime;
    }

    public void SetVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    public void SetAcceleration(Vector3 acceleration)
    {
        this.acceleration = acceleration;
    }

    private void OnDrawGizmos()
    {

        //Gizmos.DrawWireSphere(transform.position, 12f);
    }
}
