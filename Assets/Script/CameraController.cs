using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform followedObject;
    private Vector3 translocation;
    private PlayerController playerController;

    [SerializeField] float rotationSpeed;

    private void Start()
    {
        translocation = transform.position - followedObject.position;
        playerController = PlayerController.GetInstance();
    }

    private void Update()
    {
        if (playerController.Finished)
        {
            //transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.position = translocation + followedObject.position;
        }
    }
}
