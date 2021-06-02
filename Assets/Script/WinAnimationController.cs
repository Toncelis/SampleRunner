using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAnimationController : MonoBehaviour
{
    [SerializeField] Animator myAnimator;
    int amountToThrow
    {
        get => myAnimator.GetInteger("ShurikensToThrow");
        set
        {
            myAnimator.SetInteger("ShurikensToThrow", value);
        }
    }

    [SerializeField] Transform rightHand;
    [SerializeField] GameObject shouriken;
    [SerializeField] float speed;
    public void ThrowShouriken()
    {
        Rigidbody rb = Instantiate(shouriken, rightHand.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.velocity = (LevelController.GetInstance().GetTargetPosition() + Vector3.up * Random.Range(-1f,1f) - rightHand.position).normalized * speed;
        rb.angularVelocity = new Vector3(0, Random.Range(10, 360), 0);
        amountToThrow--;
    }

}
