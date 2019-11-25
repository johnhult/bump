using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 200;
    private bool gotGyro;
    private Rigidbody rb;
    private Quaternion gyroAngle;
    private Vector3 movement;
    private Vector3 gyroForce;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Input.gyro.enabled = true;
        //if (SystemInfo.supportsGyroscope)
        //{
        //    Input.gyro.enabled = true;
        //    gotGyro = true;
        //}
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(50, 50, 200, 60), gyroForce.ToString());
    }

    private float MaxAcceleration(float value) {
        float newValue;
        if (value > 0.2) {
            newValue = 0.2f;
        }
        else if (value < -0.2) {
            newValue = -0.2f;
        }
        else {
            newValue = value;
        }
        return newValue;
    }

    // Physics update
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position + Vector3.up, movement, Color.cyan);
        //if (gotGyro)
        //{
        //    //gyroAngle = Input.gyro.attitude;
        //    //gyroForce = gyroAngle.eulerAngles;
        //    gyroForce = Input.acceleration;
        //    gyroForce = new Vector3(gyroForce.x, gyroForce.y, gyroForce.z);
        //    //gyroForce = Quaternion.Euler(45, 0, 0) * gyroForce;
        //    //Vector3 removeYAngle = new Vector3(normalizeAngles.x, 0.0f, normalizeAngles.z);
        //    movement = gyroForce;

        //    //Debug.Log(gyroForce);
        //}
        //else
        //{
        //    float xSpeed = Input.GetAxis("Horizontal");
        //    float ySpeed = Input.GetAxis("Vertical");
        //    movement = new Vector3(xSpeed, 0.0f, ySpeed);

        //    //Debug.Log(movement);
        //}
        //Mobile Devices
        #if UNITY_IOS || UNITY_ANDROID || UNITY_WSA_10_0
            movement = new Vector3(-Input.acceleration.y, 0.0f, Input.acceleration.x);
        //Desktop 
        #else
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            movement = new Vector3(moveVertical, 0f, -moveHorizontal);
        #endif

        rb.AddTorque(movement*10);
    }
}
