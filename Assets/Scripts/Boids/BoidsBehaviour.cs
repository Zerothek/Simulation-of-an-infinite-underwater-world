using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoidsBehaviour : ScriptableObject
{
    public abstract Vector3 calculateAcceleration(BoidsAgent agent, List<Transform> surroundingAgents, List<Transform> surroundingObstacles, Boids boids);
}
