using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRun : CharacterComponent
{
    [SerializeField] private float runSpeed = 10f;

    protected override void HandleInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Run();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopRun();
        }
    }

    private void Run()
    {
        characterMovement.MovementSpeed = runSpeed;
    }

    private void StopRun()
    {
        characterMovement.ResetMovementSpeed();
    }
}
