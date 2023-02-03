using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBehaviour : MonoBehaviour
{
    [Header("Настройка движения")]
    [SerializeField] private float _moveSpeed = 4f;    
    [SerializeField] private float _rotateSpeed = 75f;

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private float _vInput;
    private float _hInput;
    private Rigidbody _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _vInput = Input.GetAxis(_vertical) * _moveSpeed;
        _hInput = Input.GetAxis(_horizontal) * _rotateSpeed;
    }

    void FixedUpdate()
    {
        Vector3 rotation1 = Vector3.up * _hInput;
        Quaternion angleRot = Quaternion.Euler(rotation1 * Time.fixedDeltaTime);
        _rb.MovePosition(transform.position + transform.forward * _vInput * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * angleRot);        
    }
}

