using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PowerupIndex
{
    REDMUSHROOM = 0,
    BLUEMUSHROOM = 1
}
public class PowerUpManagerEV : MonoBehaviour
{
    // reference of all player stats affected
    public IntVariable marioJumpSpeed;
    public IntVariable marioMaxSpeed;
    public PowerUpInventory powerupInventory;
    public List<GameObject> powerupIcons;

    void Start()
    {
        if (!powerupInventory.gameStarted)
        {
            powerupInventory.gameStarted = true;
            powerupInventory.Setup(powerupIcons.Count);
            resetPowerup();
        }
        else
        {
            // re-render the contents of the powerup from the previous time
            for (int i = 0; i < powerupInventory.Items.Count; i++)
            {
                Powerup p = powerupInventory.Get(i);
                if (p != null)
                {
                    AddPowerupUI(i, p.powerupTexture);
                }
            }
        }
    }
        
    public void resetPowerup()
    {
        for (int i = 0; i < powerupIcons.Count; i++)
        {
            powerupIcons[i].SetActive(false);
        }
    }

    public void removePowerUpIcon(int idx)
    {
          powerupIcons[idx].SetActive(false);
       
    }
        
    void AddPowerupUI(int index, Texture t)
    {
        powerupIcons[index].GetComponent<RawImage>().texture = t;
        powerupIcons[index].SetActive(true);
    }

    public void AddPowerup(Powerup p)
    {
        powerupInventory.Add(p, (int)p.index);
        AddPowerupUI((int)p.index, p.powerupTexture);
    }
    public void CheckPowerUp(int idx){
        if (powerupIcons[idx].activeSelf==true){
            removePowerUpIcon(idx);
            ConsumePowerUp(idx);
        }
    }

    public void ConsumePowerUp(int idx){
        Powerup p = powerupInventory.Get(idx);
        if (p!=null){
            marioMaxSpeed.ApplyChange(p.absoluteSpeedBooster);
            marioJumpSpeed.ApplyChange(p.absoluteJumpBooster);
            StartCoroutine(removeEffect(idx,p));
        }
    }
    public void AttemptConsumePowerUp(KeyCode k){
        Debug.Log("Key pressed: " + k.ToString());
        switch(k){
            case KeyCode.Z:
                CheckPowerUp(0);
                break;
            case KeyCode.X:
                CheckPowerUp(1);
                break;
            default:
                break;
        }
    }

    IEnumerator removeEffect(int idx,Powerup p){
        yield return new WaitForSeconds(p.duration);
        marioMaxSpeed.ApplyChange(-p.absoluteSpeedBooster);
        marioJumpSpeed.ApplyChange(-p.absoluteJumpBooster);
        powerupInventory.Remove(idx);
    }
    public void ResetValues(){
        powerupInventory.Clear();
        resetPowerup();
    }
}