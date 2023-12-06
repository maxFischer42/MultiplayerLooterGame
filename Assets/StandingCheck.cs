using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingCheck : MonoBehaviour
{
    public bool isColliding = false;
    public string playerTag;

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        if (other.gameObject.tag != playerTag)
        {
            isColliding = true;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag != playerTag)
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        print(other.gameObject.name + " exit");
        if (other.gameObject.tag != playerTag)
        {
            isColliding = false;
        }
    }
}
