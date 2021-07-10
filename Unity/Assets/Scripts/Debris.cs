using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    public GameConstants gameConstants;
    private  Rigidbody2D rigidBody;
    private  Vector3 scaler;
    private int debrisForce;
    private int debrisTorque;
    private int debrisTimeStep;

    // Start is called before the first frame update
    void  Start()
    {
        // we want the object to have a scale of 0 (disappear) after 30 frames. 
        scaler  =  transform.localScale  / (float) 30 ;
        rigidBody  =  GetComponent<Rigidbody2D>();
        debrisForce = gameConstants.breakDebrisForce;
        debrisTorque = gameConstants.breakDebrisTorque;
        debrisTimeStep = gameConstants.breakTimeStep;
        StartCoroutine("ScaleOut");
    }

    IEnumerator  ScaleOut(){

        Vector2 direction =  new  Vector2(Random.Range(-1.0f, 1.0f), 1);
        rigidBody.AddForce(direction.normalized  *  debrisForce, ForceMode2D.Impulse);
        rigidBody.AddTorque(debrisTorque, ForceMode2D.Impulse);
        // wait for next frame
        yield  return  null;

        // render for 0.5 second
        for (int step =  0; step  < debrisTimeStep; step++)
        {
            this.transform.localScale  =  this.transform.localScale  -  scaler;
            // wait for next frame
            yield  return  null;
        }

        Destroy(gameObject);

    }
}
