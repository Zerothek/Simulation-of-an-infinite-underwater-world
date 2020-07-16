using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boids/Behaviour/Master")]
public class MasterBehaviour : BoidsBehaviour
{
    // Vector of behaviours
    public BoidsBehaviour[] behaviours;

    // Weights associated with each behaviour
    public float[] weights;

    // Weight of horizontal allignement
    public float horizontalAllignementWeight = 0.5f;

    // Weight of target allignement
    public float targetWeight = 10f;

    // Maximum height that the boids are allowed to ascend to
    public float maxHeight = 30f;
    // Weight of height limit acceleration
    public float heightLimitWeight = 0.5f;

    // Maximum distance that the boids are allowed to stray away from the submarine
    public float maxDistanceFromSub = 4000;

    // Combines all the behaviours according to their weights
    public override Vector3 calculateAcceleration(BoidsAgent agent, List<Transform> surroundingAgents, List<Transform> surroundingObstacles, Boids boids)
    {

        // Check if we have a weight asociated for each behaviour
        if (weights.Length != behaviours.Length)
        {
            Debug.LogError("Weights and Behaviours not the same length!" + name, this);
            return Vector3.zero;
        }

        // Declare and initialize the acceleration
        Vector3 acceleration = Vector3.zero;

        // Combine all the behaviours according to their weights
        for (int i = 0; i < behaviours.Length; i++)
        {

            // Calculate current behaviour acceleration
            Vector3 behaviourAcceleration = behaviours[i].calculateAcceleration(agent, surroundingAgents, surroundingObstacles, boids);

            if (behaviourAcceleration != Vector3.zero)
            {

                // Limit the acceleration according to it's weight
                if (behaviourAcceleration.sqrMagnitude > weights[i] * weights[i])
                {
                    behaviourAcceleration = weights[i] * behaviourAcceleration.normalized;
                }

                // Steer towards the current acceleration
                acceleration += SteerTowards(behaviourAcceleration, agent, boids) * weights[i];
            }
        }

        // Push the boid to swim hotizontally
        Vector3 horizontal = new Vector3(agent.transform.forward.x, 0, agent.transform.forward.z).normalized;
        acceleration += SteerTowards(horizontal, agent, boids) * horizontalAllignementWeight;

        // Steer the boids towards the submarine position if they strayed too far
        if ((agent.transform.position - boids.target.position).sqrMagnitude > maxDistanceFromSub)
        {
            Vector3 directionToCenter = (boids.target.position - agent.transform.position).normalized;
            acceleration += SteerTowards(directionToCenter, agent, boids) * targetWeight;
        }

        // Push the boids down if they swim too far up
        if (agent.transform.position.y > maxHeight)
        {
            acceleration += SteerTowards(Vector3.down, agent, boids) * heightLimitWeight;
        }

        return acceleration;
    }

    // Steer the boid towards a certain direction
    private Vector3 SteerTowards(Vector3 vector, BoidsAgent agent, Boids boids)
    {
        Vector3 v = vector.normalized * boids.maximumSpeed - agent.Velocity;
        return Vector3.ClampMagnitude(v, boids.maximumSteeringForce);
    }


    public void adjustCohesionWeight(float value)
    {
        weights[0] = value;
    }

    public void adjustAllignementWeight(float value)
    {
        weights[1] = value;
    }

    public void adjustSeparationWeight(float value)
    {
        weights[2] = value;
    }

    public void adjustObstacleWeight(float value)
    {
        weights[3] = value;
    }

    public void adjustTargetTrackingWeight(float value)
    {
        targetWeight = value;
    }

    public void adjustHorizontalAllignementWeight(float value)
    {
        horizontalAllignementWeight = value;
    }

    public void adjustHeightLimitationWeight(float value)
    {
        heightLimitWeight = value;
    }

    public void adjustMaxDistance(float value)
    {
        maxDistanceFromSub = value;
    }

    public void adjustMaxHeight(float value)
    {
        maxHeight = value;
    }

}
