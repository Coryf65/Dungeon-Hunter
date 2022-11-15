using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{    
    public Vector2 CurrentMovement { get; set; }
    public bool NormalMovement { get; set; }

    private Rigidbody2D _rigidBody2D;
    private Vector2 _recoilMovement;

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
        Recoil();

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

    public void ApplyRecoil(Vector2 direction, float force)
    {
        _recoilMovement = direction.normalized * force;
    }

    private void Recoil()
    {
        // enough recoil to move the player
        if (_recoilMovement.magnitude > 0.01f)
        {
            _rigidBody2D.AddForce(_recoilMovement);
        }
    }
}
