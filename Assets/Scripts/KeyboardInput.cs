using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : IUserInput
{

    // Variable
    [Header("===== Key settings =====")]
    public string keyUp = "w";
    public string keyDown = "s";
    public string keyLeft = "a";
    public string keyRight = "d";

    public KeyCode keyRun;
    public KeyCode keyJump;
    public KeyCode keyAttack;
    public KeyCode keyDefense;


    [Header("===== Mouse settings =====")]
    public float mouseSensitivityX = 1.0f;
    public float mouseSensitivityY = 1.0f;

    public MyButton buttonRun = new MyButton();
    public MyButton buttonJump = new MyButton();
    public MyButton buttonAttack = new MyButton();
    public MyButton buttonDefense = new MyButton();
    public MyButton buttonLB = new MyButton();
    public MyButton buttonLT = new MyButton();


    //// Use this for initialization
    //void Start () {

    //}

    // Update is called once per frame
    void Update () {
        buttonRun.Tick(Input.GetKey(keyRun));
        buttonJump.Tick(Input.GetKey(keyJump));
        buttonAttack.Tick(Input.GetKey(keyAttack));
        buttonDefense.Tick(Input.GetKey(keyDefense));
        //buttonLB.Tick(Input.GetButton(btnLB));
        //buttonLT.Tick(Input.GetButton(btnLT));

        Jup = Input.GetAxis("Mouse Y") * 2.0f * mouseSensitivityY;
        Jright = Input.GetAxis("Mouse X") * 2.5f * mouseSensitivityX;

        if (inputEnabled)
        {
            targetDup = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);
            targetDright = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);
        }
        else
        {
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

        Vector2 tmp = SquaretoCircle(Dright, Dup);
        Dmag = Mathf.Sqrt(tmp.x * tmp.x + tmp.y * tmp.y);
        Dvec = Dright * transform.right + Dup * transform.forward;

        run = buttonRun.IsPressing; //buttonA.IsPressing && !buttonA.IsDelaying || buttonA.IsExtending;
        defense = buttonDefense.IsPressing;
        jump = buttonJump.OnPressed;
        attack = buttonAttack.OnPressed;

    }
    
}
