using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boids/Behaviour/Cohesion")]
public class CohesionBehaviour : BoidsBehaviour
{
    Vector3 currentVelocity;
    public float smoothTime = 0.5f;

    // Calculates the cohesion acceleration
    public override Vector3 calculateAcceleration(BoidsAgent agent, List<Transform> surroundingAgents, List<Transform> surroundingObstacles, Boids boids)
    {

        // If the agent is not surrounded by any other agents there is no cohesion acceleration
        if (surroundingAgents.Count == 0)
        {
            return Vector3.zero;
        }

        // Calculates the average position of the surrounding agents
        Vector3 averagePosition = Vector3.zero;
        foreach (Transform surroundingAgent in surroundingAgents)
        {
            averagePosition += surroundingAgent.position;
        }

        averagePosition /= surroundingAgents.Count;

        // Calculates the vector that points from the agent to the surrounding agents average position
        Vector3 cohesionAcceleration = averagePosition - agent.transform.position;
        
        return cohesionAcceleration.normalized;

    }
}
