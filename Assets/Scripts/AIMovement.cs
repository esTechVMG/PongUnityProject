using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public float MoveSpeed = 1.0f;
    // Start is called before the first frame update
    public GameObject ball;
    private int x;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        if (ball.transform.position.y>pos.y)
        {
            x = 1;
        }
        else
        {
            x = -1;
        }
        pos+= transform.right*MoveSpeed*Time.deltaTime*x;
        //Clamping
        pos.y = Mathf.Clamp(pos.y,-3.9f,3.9f);
        transform.position = pos;
    }
    IEnumerator SpeedIncrease()//Increases Ball speed over the time
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            MoveSpeed += 0.075f;
        }
    }
    
}
