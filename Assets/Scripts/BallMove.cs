using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BallMove : MonoBehaviour
{
    public float startForce = 5.0f,force;
    public GameObject outOne, outTwo,playerOne,playerTwo;
    public Text counterOne, counterTwo,startCounter;
    private Rigidbody rigid;
    private Vector3 startposition;
    private int[] counter = new int[] {0, 0};
    
    

    // Start is called before the first frame update
    void Start()
    {
        counterOne.text = counter[0].ToString();
        counterTwo.text = counter[0].ToString();
        startposition = transform.position;
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
            StartCoroutine("ThrowBall");
        }
        else if (id == outTwo.GetInstanceID())
        {
            counterOne.text = (++counter[0]).ToString();
            counterUpdate();
            StartCoroutine("ThrowBall");
        }
        else if(id == playerOne.GetInstanceID()|id==playerTwo.GetInstanceID())
        {
            float e = Vector3.Angle(other.transform.position-transform.position,other.transform.right);
            Debug.Log(e-90);
            /*if (e < 15)
            {
                resetBall();
                if (id == playerTwo.GetInstanceID())
                {
                    transform.Rotate(0, 0, 180);
                    rotateRandomDegrees(e*3.5f);
                }
            }*/
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
            yield return new WaitForSeconds(1);
            force += 0.1f;
        }
    }

    IEnumerator ThrowBall()
    {
        //Score counter text value update
        
        //Reset Ball Movement, rotation, position and  movement force
        resetBall();
        rigid.velocity = Vector3.zero;
        transform.position=startposition;
        //5 seconds counter
        startCounter.enabled = true;
        for (int a = 5; a != 0; a--)
        {
            startCounter.text = a.ToString();
            yield return new WaitForSeconds(1);
        }

        startCounter.enabled = false;
        

        //Random launch
        if (randomBool())
        {
            transform.Rotate(0, 0, 180);
        }
        //Rotates random 45
        rotateRandomDegrees(22.5f);
        //Initial force
        force = startForce;
        //Start impulse
        rigid.AddForce(transform.right * force, ForceMode.VelocityChange);
        
    }

    private void rotateRandomDegrees(float degrees)
    {
        transform.Rotate(Vector3.forward, Random.Range(-degrees, degrees), Space.Self);
    }
    private void resetBall()
    {
        rigid.angularVelocity = Vector3.zero;
        transform.rotation=new Quaternion(0,0,0,0);
    }
}