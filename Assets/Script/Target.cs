using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] Animator myAnimator;

    [SerializeField] int Star1Score;
    [SerializeField] int Star2Score;
    [SerializeField] int Star3Score;

    int count = 0;
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        other.enabled = false;
        count++;
        Debug.Log(count);
        if (count == Star1Score)
        {
            myAnimator.Play("EmittingStar1");
        }
        if (count == Star2Score)
        {
            myAnimator.Play("EmittingStar2");
        }
        if (count == Star3Score)
        {
            myAnimator.Play("EmittingStar3");
        }
    }
}
