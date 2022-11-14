using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{    
    public Vector2 CurrentMovement { get; set; }
    public bool NormalMovement { get; set; }

    private Rigidbody2D _rigidBody2D;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        NormalMovement = true;
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (NormalMovement)
        {
            HandleMovement();
        }
    }

    /// <summary>
    /// Move the Player character
    /// </summary>
    private void HandleMovement()
    {
        Vector2 currentMovePosition = _rigidBody2D.position + CurrentMovement * Time.fixedDeltaTime;
        _rigidBody2D.MovePosition(currentMovePosition);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="newPosition"></param>
    public void MovePosition(Vector2 newPosition)
    {
        _rigidBody2D.MovePosition(newPosition);
    }

    /// <summary>
    /// Set the players movement position
    /// </summary>
    /// <param name="newPosition">value to set the players movent to</param>
    public void SetMovement(Vector2 newPosition)
    {
        CurrentMovement = newPosition;
    }
}
