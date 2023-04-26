using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject TextLever;
    public GameObject Door; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
           TextLever.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
            {
            Door.SetActive(true);
            }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            TextLever.SetActive(false);
        }
    }
}
