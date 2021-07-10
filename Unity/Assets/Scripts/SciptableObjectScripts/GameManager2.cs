using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    public UnityEvent onApplicationExit;
	void OnApplicationQuit()
    {
        onApplicationExit.Invoke();
    }
}
