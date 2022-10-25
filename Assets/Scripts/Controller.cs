using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    // Character Movement
    // 2D
    void HandleMovement()
    {
        Vector2 movement = new( x: Input.GetAxis("Horizontal"), y: Input.GetAxis("Vertical"));

        _rigidBody2D.MovePosition(_rigidBody2D.position + movement * Time.deltaTime);
    }
}
