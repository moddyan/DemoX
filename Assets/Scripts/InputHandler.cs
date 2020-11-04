using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;

    public bool b_Input;
    
    public bool rollFlag;
    public bool sprintFlag;
    public float rollInputTimer;
    
    private PlayerControls inputActions;
    
    private Vector2 movementInput;
    private Vector2 cameraInput;




    public void OnEnable()  // TODO why public
    {
        if (inputActions == null)
        {
            inputActions = new PlayerControls();
            inputActions.PlayerMovements.Movement.performed +=
                input => movementInput = input.ReadValue<Vector2>();
            inputActions.PlayerMovements.Camera.performed +=
                input => cameraInput = input.ReadValue<Vector2>();
        }
        inputActions.Enable();
    }
    

    private void OnDisable()
    {
        inputActions.Disable();;
    }

    public void TickInput(float delta)
    {
        MoveInput(delta);
        HandleRollInput(delta);
    }

    void MoveInput(float delta)
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }

    private void HandleRollInput(float delta)
    {
        b_Input = inputActions.PlayerActions.Roll.phase == InputActionPhase.Started;
        if (b_Input)
        {
            rollInputTimer += delta;
            sprintFlag = true;
        }
        else
        {
            if (rollInputTimer > 0 && rollInputTimer < 0.5f)
            {
                sprintFlag = false;
                rollFlag = true;
            }

            rollInputTimer = 0;
        }
    }
}

