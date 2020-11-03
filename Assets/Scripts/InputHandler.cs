﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;

    private PlayerControls inputActions;
    private CameraHandler cameraHandler;
    
    private Vector2 movementInput;
    private Vector2 cameraInput;

    private void Awake()
    {
        cameraHandler = CameraHandler.singleton;
    }

    private void FixedUpdate()
    {
        var delta = Time.fixedDeltaTime;
        if (cameraHandler != null)
        {
            cameraHandler.FollowTarget(delta);
            Debug.Log(mouseX);
            Debug.Log(mouseY);
            cameraHandler.HandleCameraRotation(delta, mouseX, mouseY);
        }
    }

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
    }

    void MoveInput(float delta)
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }
}

