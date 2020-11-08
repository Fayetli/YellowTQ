using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Vector3 _moveVector;
    [SerializeField] private float _radius;

    private FixedJoystick _mobileController;
    [SerializeField] private Transform _target;
    private float _angle = -90;
    [SerializeField] private float _yPosition;
    private float _smoothSpeed = 0.125f;

    private void Start()
    {
        //_target = GameObject.FindObjectOfType<CharacterController>().transform;
        _mobileController = GameObject.FindGameObjectWithTag("CameraMoveJoystick").GetComponent<FixedJoystick>();
    }
    void LateUpdate() => Rotate();

    void Rotate()
    {
        _moveVector = Vector3.zero;
        _moveVector.y = _target.position.y + _yPosition;

        _angle -= _mobileController.Horizontal * _rotationSpeed;

        _moveVector.x = Mathf.Cos(_angle) * _radius;
        _moveVector.z = Mathf.Sin(_angle) * _radius;

        Vector3 target = _moveVector + _target.position;

        Vector3 smoothedPostion = Vector3.Lerp(transform.position, target, _smoothSpeed);
        transform.position = smoothedPostion;

        transform.LookAt(_target.position + Vector3.up * 2);

    }
}
