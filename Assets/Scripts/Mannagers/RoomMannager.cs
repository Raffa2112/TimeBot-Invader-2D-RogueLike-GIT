using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMannager : MonoBehaviour
{
    [SerializeField] GameObject[] doorsToClose;
    [SerializeField] bool closeDoorOnPlayerEnter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (closeDoorOnPlayerEnter)
        {
            foreach(GameObject door in doorsToClose)
            {
                door.SetActive(true);
            }
        }
    }
}
