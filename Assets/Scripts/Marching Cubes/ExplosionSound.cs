using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSound : MonoBehaviour
{
    private AudioSource explosionAudio;
    private Transform submarine;

    private float minVolume = 0.25f;
    private float maxVolume = 1f;
    private float maxDistance = 100f;

    // Start is called before the first frame update
    void Start()
    {

        // Get reference to the submarine
        submarine = GameObject.FindWithTag("Player").transform;

        // Get reference to the explosion audio source
        explosionAudio = GetComponent<AudioSource>();

        // Set the explosion sound volume based on how far it is from the submarine
        float distanceFromSubmarine = Vector3.Distance(submarine.position, transform.position);

        if (distanceFromSubmarine > maxDistance)
        {
            explosionAudio.volume = minVolume;
        } else
        {
            explosionAudio.volume = minVolume + (1 - distanceFromSubmarine / maxDistance) * (maxVolume - minVolume);
        }
    }
}
