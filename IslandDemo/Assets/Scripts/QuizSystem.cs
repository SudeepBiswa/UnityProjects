using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizSystem : MonoBehaviour
{
    public int answersPerQuestion;

    public string[] questions;
    public String[] answers;
    public String[] wronganswers;

    public GameObject wall;
    public GameObject sphere;

    public float spacing; // Desired space between spheres

    private float wallWidth;
    private float wallHeight;
    private float sphereDiameter;

    private bool active;
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        active = false;
        Renderer wallRenderer = wall.GetComponent<Renderer>();
        wallWidth = wallRenderer.bounds.size.x;
        wallHeight = wallRenderer.bounds.size.y;

        // Get the diameter of the sphere
        Renderer sphereRenderer = sphere.GetComponent<Renderer>();
        sphereDiameter = sphereRenderer.bounds.size.x; // Assuming sphere is uniform in size
       
    }

    // Update is called once per frame
    void Update()
    {
        if (wall.activeSelf)
        {
            
                if (active)
                {
                    // Custom logic when active
                }
                else
                {
                    print("sphere: " + answersPerQuestion * sphereDiameter);
                    print("sapcing" + (answersPerQuestion - 1) * spacing);
                     float totalRequiredWidth = (answersPerQuestion * sphereDiameter) + ((answersPerQuestion - 1) * spacing);
                     print(totalRequiredWidth);
                     print("total: " + totalRequiredWidth.ToString());
                     print("wall: " + wallWidth.ToString());
            
                     if (totalRequiredWidth <= wallWidth)
                     {
                        for (int i = 0; i < answersPerQuestion; i++)
                        {
                            float xPos = (i * (sphereDiameter + spacing)) - (totalRequiredWidth / 2) + (sphereDiameter / 2);
                            Vector3 spawnPosition = new Vector3(wall.transform.position.x + xPos, wall.transform.position.y - (float)0.5, wall.transform.position.z - (float)0.2);
                            Instantiate(sphere, spawnPosition, Quaternion.identity, transform.Find("Spheres"));
                        }
                        active = true;
                     }
                    else
                    {
                        Debug.LogError("The spheres and spacing exceed the width of the wall.");
                    }
                }
                       
        }
    }
}
