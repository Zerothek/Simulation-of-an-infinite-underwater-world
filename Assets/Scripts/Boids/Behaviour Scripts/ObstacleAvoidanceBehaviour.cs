using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boids/Behaviour/ObstacleAvoidance")]
public class ObstacleAvoidanceBehaviour : BoidsBehaviour
{

    // Number of points on the fibonacci sphere
    static int number_of_points = 100;
    
    // Calculate a Fibonaccci Sphere with number_of_points points
    List<Vector3> fiboSphere = fibonacci_sphere(number_of_points);

    // Calculates the Obstacle Avoidance acceleration
    public override Vector3 calculateAcceleration(BoidsAgent agent, List<Transform> surroundingAgents, List<Transform> surroundingObstacles, Boids boids)
    {

        // Initialize layer mask for obstacles
        int layerMask = 1 << boids.BoidLayer;
        layerMask = ~layerMask;

        // If there are no obstacles in front of the boid we have nothing to avoid
        if (!IsHeadingForCollision(agent, boids))
        { 
            return Vector3.zero;
        }

        // Declare and initialize the obstacle avoidance steering force
        Vector3 steeringForce = new Vector3(0, 0, 0);
        
        // Varible initializations
        float maxDistance = -Mathf.Infinity;
        Vector3 best_direction = agent.transform.TransformDirection(fiboSphere[number_of_points - 1]);

        for (int i = number_of_points - 1; i >= 0; i--)
        {

            RaycastHit hit;

            // Make the direction from the fibbonacci sphere relative to the agent's forward
            Vector3 dir = agent.transform.TransformDirection(fiboSphere[i]);

            //Cast a ray in the dir direction and check if it collides with something
            if (Physics.SphereCast(agent.transform.position + agent.transform.up, 4f,
                dir, out hit, 4 * boids.searchRadius, layerMask))
            {

                // Remember the direction that takes us the furthest away from an obstacle
                if (hit.distance > maxDistance)
                {

                    maxDistance = hit.distance;
                    best_direction = dir;

                }

            } else
            {

                return dir;

            }
        }

        return best_direction;
    }

    // Detects if agent is heading for a collision
    bool IsHeadingForCollision(BoidsAgent agent, Boids boids)
    {

        // Initialize layer mask for obstacles
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        // Cast a ray in front of the agent to see if it detects a collision
        RaycastHit hit;
        if (Physics.SphereCast(agent.transform.position + agent.transform.up, 4f,
            agent.transform.forward, out hit, 2 * boids.searchRadius, layerMask))
        {
            return true;
        }

        return false;
    }

    // Calculates a list of evenly spaced out number_of_points points on a unit sphere
    static List<Vector3> fibonacci_sphere(int number_of_points)
    {
        List<Vector3> points = new List<Vector3>();
        float phi = (Mathf.Sqrt(5.0f) + 1.0f) / 2.0f; //golden ratio
        float golden_angle = (2.0f - phi) * (2.0f * Mathf.PI);

        for (float i = 1; i <= number_of_points; i++)
        {

            float latitude = Mathf.Asin(-1.0f + 2.0f * i / number_of_points);
            float longitude = golden_angle * i;

            float x = Mathf.Cos(longitude) * Mathf.Cos(latitude);
            float y = Mathf.Sin(longitude) * Mathf.Cos(latitude);
            float z = Mathf.Sin(latitude);

            points.Add(new Vector3(x, y, z));

        }

        return points;
    }
}
