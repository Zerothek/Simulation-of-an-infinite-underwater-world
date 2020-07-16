using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boids : MonoBehaviour
{
    public BoidsAgent boidPrefab;
    List<BoidsAgent> agents = new List<BoidsAgent>();
    public BoidsBehaviour behaviour;

    [Range(1, 500)]
    public int numberOfBoids = 30;
    const float densityOfSpawn = 0.08f;

    [Range(1f, 20f)]
    public float maximumSteeringForce = 3f;

    [Range(1f, 100f)]
    public float maximumSpeed = 5f;
    [Range(1f, 10f)]
    public float minimumSpeed = 2f;
    [Range(1f, 20f)]

    public float searchRadius = 4f;
    [Range(4f, 20f)]
    public float obstacleSearchRadius = 10f;
    [Range(0.001f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaximumSpeed;
    float sqaureSearchRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    // The layer of the boids
    const int boidLayer = 8;
    public int BoidLayer { get { return boidLayer; } }

    // Reference to the submarine's target
    public Transform target;

    // Vector of different materials for the boids
    public Material[] fish_colors;

    // Start is called before the first frame update
    void Start()
    {
        // Calculate the spawn location
        Vector3 spawnLocation = target.position + target.transform.forward * 60;

        // Initialize variables
        squareMaximumSpeed = maximumSpeed * maximumSpeed;
        sqaureSearchRadius = searchRadius * searchRadius;
        squareAvoidanceRadius = sqaureSearchRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < numberOfBoids; i++)
        {
            // Randomly spawn the boids in a sphere centered in spawnLocation
            BoidsAgent newAgent = Instantiate(
                boidPrefab,
                spawnLocation + Random.insideUnitSphere * numberOfBoids * densityOfSpawn,
                Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))),
                transform);

            // Set the color of the fish randomly
            if (fish_colors.Length > 0)
            {
                newAgent.GetComponent<MeshRenderer>().material = fish_colors[Random.Range(0, fish_colors.Length)];
            }

            newAgent.transform.forward = -Vector3.forward;

            // Set the agent name and add him to the agent list
            newAgent.name = "Agent" + i;
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (BoidsAgent agent in agents)
        {

            // Get surrounding Agents and Obstacles of current boid
            Pair<List<Transform>, List<Transform>> surroundings = GetSurroundingObjects(agent);
            List<Transform> surroundingAgents = surroundings.First;
            List<Transform> surroundingObstacles = surroundings.Second;

            // Caclulate the acceleration as a weighted sum of all of the behaviours accelerations
            Vector3 acceleration = behaviour.calculateAcceleration(agent, surroundingAgents, surroundingObstacles, this);
            
            // Limit the acceleration to the maximumSteeringForce
            acceleration = acceleration.normalized * Mathf.Clamp(acceleration.magnitude, 0, maximumSteeringForce);
            
            // Set the acceleration of the agent
            agent.SetAcceleration(acceleration);

        }

        // Move the boids according to the previously calculated acceleration after all the 
        // agent's accelerations have been calculated
        foreach (BoidsAgent agent in agents)
        {
            agent.moveBoid(minimumSpeed, maximumSpeed);
        }
    }

    // Return the surrounding agents and obstacles for a given agent(boid)
    Pair<List<Transform>, List<Transform>>  GetSurroundingObjects(BoidsAgent agent)
    {
        List<Transform> surroundingAgents = new List<Transform>();
        List<Transform> surroundingObstacles = new List<Transform>();
        Collider[] neighboringObjects = Physics.OverlapSphere(agent.transform.position, searchRadius);

        foreach (Collider c in neighboringObjects)
        {
            if (c != agent.AgentCollider)
            {
                if (c.gameObject.layer == boidLayer)
                {
                    surroundingAgents.Add(c.transform);
                }
                else
                {
                    surroundingObstacles.Add(c.transform);
                }
            }
        }
        return new Pair<List<Transform>, List<Transform>>(surroundingAgents, surroundingObstacles);
    }

    public class Pair<T, U>
    {
        public Pair()
        {
        }

        public Pair(T first, U second)
        {
            this.First = first;
            this.Second = second;
        }

        public T First { get; set; }
        public U Second { get; set; }
    };
}
