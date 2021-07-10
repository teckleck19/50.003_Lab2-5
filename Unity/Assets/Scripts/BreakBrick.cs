using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BreakBrick : MonoBehaviour
{
    public GameObject debris;
    public GameConstants gameConstants;
    private int numDebris;
    private AudioSource BrickBreakAudio;
    private bool broken = false;
    public GameObject coinPrefab;
    // Start is called before the first frame update
    void Start()
    {
        BrickBreakAudio = GetComponent<AudioSource>();
        numDebris = gameConstants.spawnNumberOfDebris;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void  OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Player") &&  !broken){
            broken  =  true;
            BrickBreakAudio.Play();
            // assume we have 5 debris per box
            for (int x =  0; x<numDebris; x++){
                Instantiate(debris, transform.position, Quaternion.identity);
            }
            gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled  =  false;
            gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled  =  false;
            GetComponent<EdgeCollider2D>().enabled  =  false;
            Instantiate(coinPrefab, new  Vector3(this.transform.position.x, this.transform.position.y  +  1.5f, this.transform.position.z), Quaternion.identity);
            //Destroy(transform.parent.gameObject);
            
        }
    }

}
