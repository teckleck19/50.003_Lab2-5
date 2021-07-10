using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =  "GameConstants", menuName =  "ScriptableObjects/GameConstants", order =  1)]
public class GameConstants : ScriptableObject
{
    int currentScore;
    int currentPlayerHealth;

    // for Reset values
    //Vector3 gombaSpawnPointStart = new Vector3(2.5f, -0.45f, 0); // hardcoded location
    // .. other reset values 

    // for Consume.cs
    public  int consumeTimeStep =  10;
    public  int consumeLargestScale =  4;
    
    // for Break.cs
    public  int breakTimeStep =  30;
    public  int breakDebrisTorque =  10;
    public  int breakDebrisForce =  10;
    
    // for SpawnDebris.cs
    public  int spawnNumberOfDebris =  5;
    
    // for Rotator.cs
    public  int rotatorRotateSpeed =  6;
    
    public float maxOffset = 5.0f;
    public float enemyPatroltime = 2.0f;
    public int moveRight = -1;

    public float groundSurface = -4.52f;
    // for testing
    public  int testValue;
    
    
    //lab5 scriptable
    public int playerStartingMaxSpeed = 5;
    public int playerMaxJumpSpeed = 30;
    public int playerDefaultForce = 150;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
