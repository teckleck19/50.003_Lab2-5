using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text score;
    private  int playerScore =  0;
    public  delegate  void gameEvent();
    public  static  event  gameEvent OnPlayerDeath;
    public  static  event  gameEvent OnEnemyDeath;
    
    

    // Start is called before the first frame update
    void Start()
    {
        
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public  void  increaseScore(){
        playerScore  +=  1;
        score.text  =  "SCORE: "  +  playerScore.ToString();
    }

    public  void  damagePlayer(){
	    OnPlayerDeath();
    }

    public void damageEnemy(){
        OnEnemyDeath();
    }
}
