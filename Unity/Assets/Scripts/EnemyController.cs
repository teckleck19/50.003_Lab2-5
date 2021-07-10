using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameConstants gameConstants;
    private float originalX;
    private float maxOffset;
    private float enemyPatroltime ;
    private int moveRight; 
    private Vector2 velocity;

    private Vector2 currentPosition;
    public Vector2 currentDirection;
    private Rigidbody2D enemyBody;
    private SpriteRenderer enemySprite;
    public float speed;

    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();
        moveRight = gameConstants.moveRight;
        enemyPatroltime =gameConstants.enemyPatroltime;
        maxOffset = gameConstants.maxOffset;
        // get the starting position
        /* originalX = transform.position.x;
        ComputeVelocity(); */
        currentDirection = new Vector2(1,0);
    }
    void ComputeVelocity(){
        velocity = new Vector2((moveRight)*maxOffset / enemyPatroltime, 0);
    }
    void MoveGomba(){
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }
    
    void Update() {
        /* if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
        {// move gomba
            MoveGomba();
        }
        else{
            // change direction
            moveRight *= -1;
            if (enemySprite.flipX) enemySprite.flipX = false; else enemySprite.flipX = true;
            ComputeVelocity();
            MoveGomba();
        } */
        currentPosition = transform.position;
        Vector2 nextPosition = currentPosition + speed * currentDirection.normalized * Time.fixedDeltaTime;
        enemyBody.MovePosition(nextPosition);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        /* if (col.gameObject.CompareTag("Player") ) {
          stop = true;
        } */
        
        if (col.gameObject.CompareTag("Pipe")||col.gameObject.CompareTag("Limit")||col.gameObject.CompareTag("PipeHead")) {
          currentDirection.x = currentDirection.x * -1.0f;
          if (enemySprite.flipX) enemySprite.flipX = false; else enemySprite.flipX = true;
        }       
    }
}
