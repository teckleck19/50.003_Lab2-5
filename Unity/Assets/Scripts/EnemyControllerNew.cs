using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerNew : MonoBehaviour
{
    public  GameConstants gameConstants;
	private  int moveRight;
	private  float originalX;
	private  Vector2 velocity;
	private  Rigidbody2D enemyBody;
	private SpriteRenderer enemySprite;
	private bool playerNotDead;
	private bool dead;
	void  Start()
	{
		enemyBody  =  GetComponent<Rigidbody2D>();
		enemySprite = GetComponent<SpriteRenderer>();
		GameManager.OnPlayerDeath  +=  EnemyRejoice;
		// get the starting position
		originalX  =  transform.position.x;
		playerNotDead = true;
		dead = false;
		// randomise initial direction
		moveRight  =  Random.Range(0, 2) ==  0  ?  -1  :  1;
		
		// compute initial velocity
		ComputeVelocity();
	}
	
	void  ComputeVelocity()
	{
			velocity  =  new  Vector2((moveRight) *  gameConstants.maxOffset  /  gameConstants.enemyPatroltime, 0);
	}
  
	void  MoveEnemy()
	{
		enemyBody.MovePosition(enemyBody.position  +  velocity  *  Time.fixedDeltaTime);
	}

	void  Update()
	{
		if (playerNotDead){
			if (Mathf.Abs(enemyBody.position.x  -  originalX) <  gameConstants.maxOffset)
			{// move gomba
				MoveEnemy();
			}
			else
			{
				// change direction
				moveRight  *=  -1;
				ComputeVelocity();
				MoveEnemy();
			}
		}else{
			if (enemySprite.flipX) enemySprite.flipX = false; else enemySprite.flipX = true;
		}
	}

    void  OnTriggerEnter2D(Collider2D other){
		// check if it collides with Mario
		if (!dead){
			if (other.gameObject.tag  ==  "Player"){
				// check if collides on top
				float yoffset = (other.transform.position.y  -  this.transform.position.y);
				if (yoffset  >  0.75f){
					CentralManager.centralManagerInstance.increaseScore();
					CentralManager.centralManagerInstance.damageEnemy();
					KillSelf();	
					dead = true;
				}
				else{
					// hurt player
					CentralManager.centralManagerInstance.damagePlayer();
				}
			}
		}
	}

    void  KillSelf(){
		// enemy dies
		StartCoroutine(flatten());
		Debug.Log("Kill sequence ends");
		
	}

    IEnumerator  flatten(){
		Debug.Log("Flatten starts");
		int steps =  5;
		float stepper =  1.0f/(float) steps;
		

		for (int i =  0; i  <  steps; i  ++){
			this.transform.localScale  =  new  Vector3(this.transform.localScale.x, this.transform.localScale.y  -  stepper, this.transform.localScale.z);

			// make sure enemy is still above ground
			this.transform.position  =  new  Vector3(this.transform.position.x, gameConstants.groundSurface +  GetComponent<SpriteRenderer>().bounds.extents.y, this.transform.position.z);
			yield  return  null;
		}
		Debug.Log("Flatten ends");
		this.gameObject.SetActive(false);
		Debug.Log("Enemy returned to pool");
		dead = false;
		yield  break;
	}

	void  EnemyRejoice(){
		Debug.Log("Enemy killed Mario");
		playerNotDead = false;
	
		// do whatever you want here, animate etc
		// ...
	}
	
}
