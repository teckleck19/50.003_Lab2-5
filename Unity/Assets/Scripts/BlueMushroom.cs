using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class BlueMushroom : MonoBehaviour, ConsumableInterface
{
	public  Texture t;
	public  void  consumedBy(GameObject player){
		// give player jump boost
		player.GetComponent<PlayerController>().maxSpeed  *=1.5f;
		StartCoroutine(removeEffect(player));
	}

	IEnumerator  removeEffect(GameObject player){
		yield  return  new  WaitForSeconds(5.0f);
		player.GetComponent<PlayerController>().maxSpeed  /= 1.5f;
	}
    void  OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player")){
            // update UI
            CentralManager.centralManagerInstance.addPowerup(t, 1, this);
            GetComponent<Collider2D>().enabled  =  false;
        }
    }
}
