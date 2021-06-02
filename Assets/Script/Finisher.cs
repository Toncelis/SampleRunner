using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finisher : MonoBehaviour
{
    [SerializeField] ParticleSystem leavesEmitter;
    [SerializeField] List<ParticleSystem> confettis;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            leavesEmitter.Play();
            PlayerController.GetInstance().Finish();

            foreach (ParticleSystem ps in confettis)
            {
                ps.Play();
            }
        }
    }
}
