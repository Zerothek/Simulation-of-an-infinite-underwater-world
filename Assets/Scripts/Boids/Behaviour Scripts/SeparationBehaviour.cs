using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boids/Behaviour/Separation")]

// Calculates the separation acceleration
public class SeparationBehaviour : BoidsBehaviour
{
    public override Vector3 calculateAcceleration(BoidsAgent agent, List<Transform> surroundingAgents, List<Transform> surroundingObstacles, Boids boids)
    {

        // If the agent is not surrounded by any other agents there is no cohesion acceleration
        if (surroundingAgents.Count == 0)
        {
            return Vector3.zero;
        }

        // The average direction of avoiding surrounding agents
        Vector3 averageAvoidance = Vector3.zero;
        
        // Number of agents to avoid
        int numberOfAvoids = 0;

        foreach (Transform surroundingAgent in surroundingAgents)
        {

            // If the surrounding agent is within the avoidance radius calculate the direction of avoidance
            // (the opposite of the vector from the surrounding agent to the current agent scaled by the 
            // inverse of the distance between the two agents).
            if (Vector3.SqrMagnitude(agent.transform.position - surroundingAgent.position) < boids.SquareAvoidanceRadius) {
                averageAvoidance += agent.transform.position - surroundingAgent.position * (1 / Vector3.Distance(agent.transform.position, surroundingAgent.position));
                numberOfAvoids++;
            }

        }

        // Calculate the average of the avoidance vectors
        if (numberOfAvoids > 0)
        {
            averageAvoidance /= numberOfAvoids;
        }

        return averageAvoidance.normalized;

    }

}
