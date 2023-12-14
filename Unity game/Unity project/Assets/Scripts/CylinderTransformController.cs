using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;


public class CylinderTransformController : MonoBehaviour
{
    public float minScaleY = 0.5f;  // cutomized min value
    public float transitionDuration = 2.0f;  // trainsition time
    public float returnSpeed = 5.0f;  // velocity to original scale
    public Rigidbody ballRigidbody;  // rigidbody of ball

    public float pushForce;


    private bool isScaling = false;
    private bool isReturning = false;
    private float scaleStartTime;
    private Vector3 initialScale;

    public float fabric1;
    public float fabric2;
    public float fabric3;
    public float fabric4;
    SerialPort stream = new SerialPort("/dev/cu.usbmodem1421", 9600); // customize based on the serail port


    private void Start()
    {
        initialScale = transform.localScale;
        stream.Open(); // Open the Serial Stream
    }

    private void Update()
    {
        if (stream.IsOpen)
        {
            string value = stream.ReadLine();
            ParseAndAssignValues(value);
        }
        // if (fabric1 < 700)
        // {
        //     isScaling = true;
        // }

        // if (fabric1 > 700)
        // {
        //     isScaling = false;
        //     isReturning = true;
        // }

        // compress Phase 1
        if (fabric1 >= 650 && fabric1 < 700) // Setting thresholds on a case-by-case basis
        {
            isReturning = true;
            // Calculate scaling interpolation
            float scaleProgress = (700 - fabric1) / 50 / 4;
            pushForce = scaleProgress * 2; // Record push's largest value
            float newScaleY = Mathf.Lerp(initialScale.y, minScaleY, scaleProgress);
            Vector3 newScale = new Vector3(initialScale.x, newScaleY, initialScale.z);
            transform.localScale = newScale;
        }

        // compress Phase 2
        if (fabric1 < 650 && fabric2 >= 500 && fabric2 < 550)
        {
            isReturning = true;
            // Calculate scaling interpolation
            float scaleProgress = (550 - fabric2) / 50 / 4 + 0.25f; // 1/4 has already been pressed
            pushForce = scaleProgress * 2; // Record push's largest value
            float newScaleY = Mathf.Lerp(initialScale.y, minScaleY, scaleProgress);
            Vector3 newScale = new Vector3(initialScale.x, newScaleY, initialScale.z);
            transform.localScale = newScale;
        }

        // compress Phase 3
        if (fabric1 < 650 && fabric2 < 500 && fabric3 >= 720 && fabric3 < 770)
        {
            isReturning = true;
            // Calculate scaling interpolation
            float scaleProgress = (770 - fabric3) / 50 / 4 + 0.5f; // 1/2 has already been pressed
            pushForce = scaleProgress * 2; // Record push's largest value
            float newScaleY = Mathf.Lerp(initialScale.y, minScaleY, scaleProgress);
            Vector3 newScale = new Vector3(initialScale.x, newScaleY, initialScale.z);
            transform.localScale = newScale;
        }

        // compress Phase 4
        if (fabric1 < 650 && fabric2 < 500 && fabric3 < 720 && fabric4 >= 580 && fabric4 < 630)
        {
            isReturning = true;
            // Calculate scaling interpolation
            float scaleProgress = (630 - fabric4) / 50 / 4 + 0.75f; // 3/4 has already been pressed
            pushForce = scaleProgress * 2; // Record push's largest value
            float newScaleY = Mathf.Lerp(initialScale.y, minScaleY, scaleProgress);
            Vector3 newScale = new Vector3(initialScale.x, newScaleY, initialScale.z);
            transform.localScale = newScale;
        }


        if (fabric1 > 700 & isReturning)
        {
            // Calculate the speed to return to the original scale
            float returnStep = returnSpeed * Time.deltaTime;
            transform.localScale = Vector3.MoveTowards(transform.localScale, initialScale, returnStep);

            // back to original scale
            if (transform.localScale == initialScale)
            {
                isReturning = false;
                ballRigidbody.AddForce(Vector3.up * pushForce, ForceMode.Impulse);
            }
        }
    }

    void ParseAndAssignValues(string input)
    {
        string[] values = input.Split(' ');

        if (values.Length == 4)
        {
            fabric1 = float.Parse(values[0]);
            fabric2 = float.Parse(values[1]);
            fabric3 = float.Parse(values[2]);
            fabric4 = float.Parse(values[3]);
        }
        else
        {
            Debug.LogWarning("Invalid input format");
        }
    }

}