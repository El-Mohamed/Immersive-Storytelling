﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public GameObject[] waypoints;
    int current = 0;
    float rotSpeed;
    public float speed;
    float WPradius = (float)0.1;
    Animator animator;
    int lastcurrent;

    public float damping;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Vector3.Distance(waypoints[current].transform.position, transform.position) < WPradius)
        {
            //Debug.Log(current);
            animator.SetFloat("Vertical", 0);
            current++;
            if (current >= waypoints.Length)
            {
                current = waypoints.Length;
            }
        }
        else
        {
            var rotation = Quaternion.LookRotation(waypoints[current].transform.position - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);

            //SmoothLook(waypoints[current].transform.position);
            //transform.LookAt(waypoints[current].transform);
            animator.SetFloat("Vertical", (float)0.2);
        }
        if (current == waypoints.Length)
        {
            current -= 1;
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);

    }

    void SmoothLook(Vector3 newDirection)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(newDirection), Time.deltaTime);
    }
}
