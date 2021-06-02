using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem pickEffect;

    [SerializeField] Rigidbody myRb;

    private void Start()
    {
        //myRb = GetComponent<Rigidbody>();
        myRb.angularVelocity = new Vector3(0, Random.Range(1, 30), 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //pickEffect.Play();
            PlayerController.GetInstance().RaiseScore();
            animator.SetTrigger("PickedUp");
            animator.transform.SetParent(PlayerController.GetInstance().transform);

        }
    }
}
