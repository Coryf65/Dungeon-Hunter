using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDash : CharacterComponent
{
    [SerializeField] private float _dashDistance = 5f;
    [SerializeField] private float _dashDuration = 0.5f;

    private bool _isDashing;
    private float _dashTimer; // how long the dash is
    private Vector2 _dashOrigin; // when we start our dash
    private Vector2 _dashDestination; // calculated
    private Vector2 _newPosition; // new position every frame

    protected override void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dash();
        }
    }

    protected override void HandleAbility()
    {
        base.HandleAbility();

        if (_isDashing)
        {
            if (_dashTimer < _dashDuration)
            {
                _newPosition = Vector2.Lerp(_dashOrigin, _dashDestination, _dashTimer / _dashDuration);
                controller.MovePosition(_newPosition);
                _dashTimer = Time.deltaTime;
            } 
            else
            {
                StopDash();
            }
        }
    }

    private void Dash()
    {
        _isDashing = true;
        _dashTimer = 0f;
        controller.NormalMovement = false;
        _dashOrigin = transform.position;

        _dashDestination = transform.position + (Vector3)controller.CurrentMovement.normalized * _dashDistance;
    }

    private void StopDash()
    {
        _isDashing = false;
    }
}
