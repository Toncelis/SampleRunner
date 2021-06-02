using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    static LevelController _instance;
    private void SetInstance ()
    {
        _instance = this;
    }

    public static LevelController GetInstance ()
    {
        return _instance;
    }

    private void OnEnable()
    {
        SetInstance();
    }

    [SerializeField] Transform target;
    public Vector3 GetTargetPosition ()
    {
        return target.position;
    }
}
