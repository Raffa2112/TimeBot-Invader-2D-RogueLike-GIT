using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] string levelToLoad;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(SceneManager.sceneCount > 0)
                {
                SceneManager.LoadScene(levelToLoad);
                }
        }
    }

}
