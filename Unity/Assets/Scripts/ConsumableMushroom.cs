using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableMushroom : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Vector2 currentPosition;
    public Vector2 currentDirection;
    private Rigidbody2D mushroomBody;
    private bool stop;

    void Start()
    {
        mushroomBody = GetComponent<Rigidbody2D>();
        stop = false;
        currentDirection = new Vector2(1,0);

        mushroomBody.AddForce(Vector2.up  *  20, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop){
            currentPosition = transform.position;
            Vector2 nextPosition = currentPosition + speed * currentDirection.normalized * Time.fixedDeltaTime;
            mushroomBody.MovePosition(nextPosition);
        }else{
            gameObject.transform.localScale=new Vector3(0,0,0);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") ) {
          stop = true;
        }
        
        if (col.gameObject.CompareTag("Pipe")||col.gameObject.CompareTag("Limit")) {
          currentDirection.x = currentDirection.x * -1.0f;
        }       
    }
    void  OnBecameInvisible(){
        //Destroy(gameObject);	
    }
}


