using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverAnim : MonoBehaviour
{
    public float hoverHeight = 0.2f; // The height of the hover
    public float hoverSpeed = 2.0f;  // The speed of the hover

    // Initial position of the object
    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Record the original position of the object
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the new position based on a sine wave
        float newY = originalPosition.y + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = new Vector3(originalPosition.x, newY, originalPosition.z);
    }
}
