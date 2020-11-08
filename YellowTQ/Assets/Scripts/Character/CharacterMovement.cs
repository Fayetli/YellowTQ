using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    [SerializeField] private float _gravityForce;

    private FixedJoystick _mobileControllerMove;
    private Vector3 _direction;
    private CharacterController _controller;
    float turnSmoothVelocity;
    float turnSmoothTime = 0.1f;
    private Transform _camera;

    private void Start()
    {
        _mobileControllerMove = GameObject.FindGameObjectWithTag("CharacterJoystick").GetComponent<FixedJoystick>();
        _camera = GameObject.FindObjectOfType<Camera>().gameObject.transform;
        _controller = gameObject.GetComponent<CharacterController>();

        GameObject.FindGameObjectWithTag("AttackButton").GetComponent<Button>().onClick.AddListener(Rotate);
    }

    void Update() => Move();

    
    private void Move()
    {
        bool IsGrounded = CheckGravity();

        float horizontal = _mobileControllerMove.Horizontal;
        float vertical = _mobileControllerMove.Vertical;

        horizontal = horizontal > 0.5f ? 1 : horizontal;
        horizontal = horizontal < -0.5f ? -1 : horizontal;
        horizontal = horizontal >= -0.5f && horizontal <= 0.5f ? 0 : horizontal;
        vertical = vertical > 0.5f ? 1 : vertical;
        vertical = vertical < -0.5f ? -1 : vertical;
        vertical = vertical >= -0.5f && vertical <= 0.5f ? 0 : vertical;

        _direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (_direction.magnitude >= 0.1f)
        {
            Rotate();
        }

    }

    private void Rotate()
    {
        float targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        moveDirection.y = _gravityForce;
        _controller.Move(moveDirection.normalized * _moveSpeed * Time.deltaTime);
    }



    private bool CheckGravity()
    {
        bool isGrounded = _controller.isGrounded;
        if (isGrounded == false)
        {
            _gravityForce -= 20f * Time.deltaTime;
        }
        else
        {
            _gravityForce = -1;
        }
        return isGrounded;
    }
}
