using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform _camera;
    void Start()
    {
        _camera = GameObject.FindObjectOfType<Camera>().gameObject.transform;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + _camera.forward);
    }
}
