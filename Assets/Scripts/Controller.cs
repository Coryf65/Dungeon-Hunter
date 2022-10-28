using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{    
    private Vector2 _currentMovement { get; set; }
    private Rigidbody2D _rigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    // Move the Player character
    void HandleMovement()
    {
        Vector2 currentMovePosition = _rigidBody2D.position + _currentMovement * Time.fixedDeltaTime;
        _rigidBody2D.MovePosition(currentMovePosition);
    }

    /// <summary>
    /// Set the players movement position
    /// </summary>
    /// <param name="newPosition">value to set the players movent to</param>
    public void SetMovement(Vector2 newPosition)
    {
        _currentMovement = newPosition;
    }
}
