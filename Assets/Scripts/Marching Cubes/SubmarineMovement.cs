using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineMovement : MonoBehaviour
{
    Rigidbody rigidbody;

    private void Start()
    {
        // Get reference to the Rigid Body component of the submarine
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 old_position = transform.position;

        // Terrain layermask
        int layermask = 1 << 9;

        // Move the submarine forward
        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            this.transform.position += this.transform.forward * Time.deltaTime * 8;
        }

        // Move the submarine backward
        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            this.transform.position -= this.transform.forward * Time.deltaTime * 4;
        }

        // Move the submarine left
        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            this.transform.Rotate(new Vector3(0, -40 * Time.deltaTime, 0));
        }

        // Move the submarine right
        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            this.transform.Rotate(new Vector3(0, 40 * Time.deltaTime, 0));
        }

        // Move the submarine up
        if (Input.GetKey("e"))
        {
            this.transform.position += this.transform.up * Time.deltaTime * 4;
        }

        // Move the submarine down
        if (Input.GetKey("q"))
        {
            this.transform.position -= this.transform.up * Time.deltaTime * 4;
        }


        // Obstacle Collision

        RaycastHit hit;
        if (Physics.Raycast(transform.position + transform.right + transform.up - transform.forward * 2.5f, transform.forward, 5, layermask))
        {
            transform.position = old_position;
        }

        if (Physics.Raycast(transform.position - transform.right + transform.up - transform.forward * 2.5f, transform.forward, 5, layermask))
        {
            transform.position = old_position;
        }

        if (Physics.Raycast(transform.position + transform.right - transform.up - transform.forward * 2.5f, transform.forward, 5, layermask))
        {
            transform.position = old_position;
        }

        if (Physics.Raycast(transform.position - transform.right - transform.up - transform.forward * 2.5f, transform.forward, 5, layermask))
        {
            transform.position = old_position;
        }
    }

}
