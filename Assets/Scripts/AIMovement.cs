using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public float MoveSpeed = 1.0f;
    // Start is called before the first frame update
    public GameObject ball;
    private int x;

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        if (Mathf.Floor(ball.transform.position.y*100)>Mathf.Floor(pos.y*100))
        {
            x = 1;
        }
        else
        {
            x = -1;
        }
        pos+= transform.right*Mathf.Floor(MoveSpeed*Time.deltaTime*x*100)/100;
        //Clamping
        pos.y = Mathf.Clamp(pos.y,-3.7f,3.7f);
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
