using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreFinisher : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.GetInstance().PrepareToFinish();
        }
    }
}   
