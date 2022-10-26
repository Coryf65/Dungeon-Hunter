using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    protected float horizontalInput;
    protected float verticalInput;

    protected Controller controller;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        controller = GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleAbility();
    }

    /// <summary>
    /// Character Abilities
    /// move, run, dash, shoot
    /// </summary>
    protected virtual void HandleAbility()
    {
        InternalInput();
    }

    /// <summary>
    /// Get input to perform actions
    /// get buttons from player
    /// </summary>
    protected virtual void HandleInput()
    {
        
    }

    /// <summary>
    /// Get the input to control our character
    /// x and y movements
    /// </summary>
    protected virtual void InternalInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }
}
