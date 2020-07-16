using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMoving : MonoBehaviour
{
    public Transform submarine;
    public float speed = 15f;
    public float maxDistance = 150f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.transform.forward * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, submarine.position) > maxDistance) 
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
