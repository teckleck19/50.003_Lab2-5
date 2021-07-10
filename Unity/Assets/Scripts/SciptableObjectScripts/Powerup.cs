using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PowerupManagerEV : MonoBehaviour
{
}
[CreateAssetMenu(fileName = "Powerup", menuName = "ScriptableObjects/Powerup", order = 5)]
public class Powerup : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif
	// index in the UI
    public PowerupIndex index;
	// texture in the UI
    public Texture powerupTexture;
    
    // list of things any powerup can do
    public int absoluteSpeedBooster;
    public int absoluteJumpBooster;

	// effect of powerup
    public int duration;

    public List<int> Utilise(){
        return new List<int> {absoluteSpeedBooster, absoluteJumpBooster};
    }

    public void Reset(){
        absoluteSpeedBooster = 0;
        absoluteJumpBooster = 0;
    }

    public void Enhance(int speedBooster, int jumpBooster){
        absoluteSpeedBooster += speedBooster;
        absoluteJumpBooster += jumpBooster;
    }
}
