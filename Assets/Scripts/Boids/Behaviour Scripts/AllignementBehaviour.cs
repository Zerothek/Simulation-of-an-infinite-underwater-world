using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boids/Behaviour/Allignement")]

// Calculates the separation acceleration
public class AllignementBehaviour : BoidsBehaviour
{

    public override Vector3 calculateAcceleration(BoidsAgent agent, List<Transform> surroundingAgents, List<Transform> surroundingObstacles, Boids boids)
    {

        // If the agent is not surrounded by any other agents continue accelerating in your current direction
        if (surroundingAgents.Count == 0)
        {
            return agent.transform.forward;
        }

        //Calculate the average allignement of the surrounding agents
        Vector3 averageHeading = Vector3.zero;

        foreach (Transform surroundingAgent in surroundingAgents)
        {
            //averageHeading += surroundingAgent.forward;
            averageHeading += surroundingAgent.GetComponent<BoidsAgent>().Velocity;
        }

        if (surroundingAgents.Count > 0)
        {
            averageHeading /= surroundingAgents.Count;
        }

        return averageHeading.normalized;
    }

}
