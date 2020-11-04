using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   private InputHandler inputHandler;
   private Animator anim;
   
   private void Start()
   {
      inputHandler = GetComponent<InputHandler>();
      anim = GetComponentInChildren<Animator>();
   }

   private void Update()
   {
      inputHandler.isInteracting = anim.GetBool("isInteracting");
      inputHandler.rollFlag = false;
      inputHandler.sprintFlag = false;
   }
}
