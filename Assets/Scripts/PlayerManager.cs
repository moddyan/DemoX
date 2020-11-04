﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   private InputHandler inputHandler;
   private Animator anim;
   private CameraHandler cameraHandler;
   private PlayerLocomotion playerLocomotion;

   public bool isInteracting;
   
   [Header("Player Flags")]
   public bool isSprinting;

   private void Awake()
   {
      cameraHandler = CameraHandler.singleton;
   }

   private void Start()
   {
      inputHandler = GetComponent<InputHandler>();
      anim = GetComponentInChildren<Animator>();
      playerLocomotion = GetComponentInChildren<PlayerLocomotion>();
   }

   private void Update()
   {
      float delta = Time.deltaTime;
      isInteracting = anim.GetBool("isInteracting");
      
      inputHandler.TickInput(delta);
      playerLocomotion.HandleMovement(delta);
      playerLocomotion.HandleRollingAndSprinting(delta);
   }
   
   private void FixedUpdate()
   {
      var delta = Time.fixedDeltaTime;
      if (cameraHandler != null)
      {
         cameraHandler.FollowTarget(delta);
         cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
      }
   }

   private void LateUpdate()
   {
      inputHandler.rollFlag = false;
      inputHandler.sprintFlag = false;
      isSprinting = inputHandler.b_Input;
   }
}
