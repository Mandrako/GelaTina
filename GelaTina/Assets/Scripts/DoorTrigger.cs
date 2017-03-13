using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Door door;

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Player")
        {
            door.Open();
        }
    }

    void OnTriggerExit2D(Collider2D target)
    {
        if(target.gameObject.tag == "Player")
        {
            door.Close();
        }
    }
}
