using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] RectTransform myRT;
    [SerializeField] private float speed;

    private void Update()
    {
        myRT.Rotate(Vector3.forward, speed * Time.deltaTime);
    }
}
