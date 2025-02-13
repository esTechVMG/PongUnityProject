﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float force = 1.0f;
    private void Update()
    {
        var pos = transform.position;
        float x=Input.GetAxisRaw("Vertical");
        if (x == 1|x==-1)
        {
            pos+= transform.right * (force * Time.deltaTime * x);
        }
        //Clamp the position
        pos.y = Mathf.Clamp(pos.y,-3.7f,3.7f);
        transform.position = pos;
    }
}

    /* OLD MOVEMENT CODE BASED ON PHYSICS
     //This code works better with rotation clamping additions
     //in order to avoid physics-calculations-related bugs
    private Rigidbody rb;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        //Control movement
        float x=Input.GetAxisRaw("Vertical");
        if (x==1){
            rb.AddForce(Vector3.up*force);
        }else if(x==-1){
            rb.AddForce(Vector3.down*force);
        }
    }*/
