using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BallMove : MonoBehaviour
{
    public float force = 1.0f;
    public Text counterOne;
    public Text counterTwo;
    public Text startCounter;
    private Rigidbody rigid;
    public int[] counter = new int[] {0, 0};

    public GameObject outOne, outTwo;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        //Initial ball throw coroutine
        StartCoroutine("ThrowBall");
        //Ball Speed Increase Coroutine
        StartCoroutine("SpeedIncrease");
    }

    bool randomBool() //Generates a random boolean
    {
        return (Random.Range(0, 2) == 1);
    }

    private void OnCollisionEnter(Collision other)
    {
        //IMPORTANT
        //This prevents the ball from slowing down with bounces
        Vector3 v = rigid.velocity;
        v = v.normalized;
        v *= force;
        rigid.velocity = v;
        
        //Checks for score counter updates
        int id = other.gameObject.GetInstanceID();
        if (id == outOne.GetInstanceID())
        {
            counterTwo.text = (++counter[1]).ToString();
            counterUpdate();
        }
        else if (id == outTwo.GetInstanceID())
        {
            counterOne.text = (++counter[0]).ToString();
            counterUpdate();
        }
    }

    private void counterUpdate()//show counter in console on every score update
    {
        Debug.Log("Player 1:" + counter[0] + " Player 2(IA):" + counter[1]);
    }

    IEnumerator SpeedIncrease()//Increases Ball speed over the time
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            force += 0.1f;
        }
    }

    IEnumerator ThrowBall()
    {
        for (int a = 5; a != 0; a--)
        {
            startCounter.text = a.ToString();
            yield return new WaitForSeconds(1);
        }
        //Score counter initial value
        counterOne.text = counter[0].ToString();
        counterTwo.text = counter[0].ToString();
        
        //Random launch
        if (randomBool())
        {
            transform.Rotate(0, 180, 0);
        }
        //Rotates random 45
        transform.Rotate(Vector3.forward, Random.Range(-45, 45), Space.Self);
        //Start impulse
        rigid.AddForce(transform.right * force, ForceMode.VelocityChange);
        
    }
}