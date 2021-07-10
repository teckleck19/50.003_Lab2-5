using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public  GameConstants gameConstants;
    void Start()
    {
        GameManager.OnEnemyDeath  +=  spawnNewEnemy;
        spawnFromPooler(ObjectType.gombaEnemy);
        spawnFromPooler(ObjectType.greenEnemy);

    }


    void spawnFromPooler(ObjectType i)
    {
        GameObject item = ObjectPooler.SharedInstance.GetPooledObject(i);

        if (item != null)
        {
            //set position
            if (i==ObjectType.gombaEnemy){
                item.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            }else if(i==ObjectType.greenEnemy){
                item.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
            }
            item.transform.position = new Vector3(Random.Range(-4.5f, 4.5f), gameConstants.groundSurface + item.GetComponent<SpriteRenderer>().bounds.extents.y, 0);
            item.SetActive(true);
        }
        else
        {
            Debug.Log("not enough items in the pool!");
        }
    }

    public void spawnNewEnemy()
    {

        ObjectType i = Random.Range(0, 2) == 0 ? ObjectType.gombaEnemy : ObjectType.greenEnemy;
        spawnFromPooler(i);

    }

}
