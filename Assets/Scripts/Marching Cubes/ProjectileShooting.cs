using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooting : MonoBehaviour
{
    public GameObject projectile;
    private Transform submarineTransform;

    // Start is called before the first frame update
    void Start()
    {
        // Get reference to the submarine Transform
        submarineTransform = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {

        // Check if Space has been pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Instantiate a new Projectile
            GameObject newProjectile = Instantiate(projectile,
                transform.position + transform.forward * 4,
                this.transform.rotation);

            ProjectileMoving projectileMoving = newProjectile.GetComponent<ProjectileMoving>();
            projectileMoving.submarine = submarineTransform;
        }
    }
}
