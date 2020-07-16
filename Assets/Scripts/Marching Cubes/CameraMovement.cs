using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;

    // back and up offset
    public float offset_x = 6;
    public float offset_y = 1;

    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 1000;

    float pitch = 0, previous_pitch;
    float yaw = 0, previous_yaw;

    float elapsed_time = 0;
    float time_limit = 2f;

    InstantiatorTest instantiator;

    private void Start()
    {
        instantiator = GetComponent<InstantiatorTest>();
    }

    private void Update()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {

        pitch = -50 * Time.deltaTime * Input.GetAxis("Mouse Y");
        yaw = 50 * Time.deltaTime * Input.GetAxis("Mouse X");

        if (yaw == previous_yaw && pitch == previous_pitch)
        {
            elapsed_time += Time.deltaTime;
        } else
        {
            elapsed_time = 0;
        }

        if (elapsed_time > time_limit)
        {

            // Slowly bring camera back to default position if time_limit seconds passed
            Vector3 desiredPosition = target.position - target.forward * offset_x + target.up * offset_y;

            transform.forward = Vector3.SmoothDamp(transform.forward, target.forward, ref velocity, smoothTime * Time.deltaTime);
            this.transform.position = target.transform.position - this.transform.forward * offset_x + this.transform.up * offset_y;

        }
        else
        {

            // Rotate camera using mouse inputs
            this.transform.Rotate(new Vector3(0, 1, 0), yaw, Space.World);
            this.transform.Rotate(new Vector3(1, 0, 0), pitch);

            // Limit the camera rotation
            if (this.transform.rotation.eulerAngles.x > 40 && this.transform.rotation.eulerAngles.x < 320)
            {
                if (this.transform.rotation.eulerAngles.x < 180)
                {
                    this.transform.eulerAngles = new Vector3(40, this.transform.rotation.eulerAngles.y, 0);
                }
                else
                {
                    this.transform.eulerAngles = new Vector3(320, this.transform.rotation.eulerAngles.y, 0);
                }
            }

            this.transform.position = target.transform.position - this.transform.forward * offset_x + this.transform.up * offset_y;

            previous_yaw = yaw;
            previous_pitch = pitch;
        }
    }
}
