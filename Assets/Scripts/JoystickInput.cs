using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInput : IUserInput
{

    [Header("===== Joystick Settings =====")]
    public string axisX = "axisX";
    public string axisY = "axisY";
    public string axisJright = "axis4";
    public string axisJup = "axis5";
    //public string btnA = "btn0";
    //public string btnB = "btn1";
    //public string btnC = "btn2";
    //public string btnD = "btn3";
    public string btnA = "Fire1";
    public string btnB = "Fire2";
    public string btnC = "Fire3";
    public string btnD = "Jump";
    public string btnLB = "LB";
    public string btnLT = "LT";

    public MyButton buttonA = new MyButton();
    public MyButton buttonB = new MyButton();
    public MyButton buttonC = new MyButton();
    public MyButton buttonD = new MyButton();
    public MyButton buttonLB = new MyButton();
    public MyButton buttonLT = new MyButton();


    //// Use this for initialization
    //void Start () {

    //}

    // Update is called once per frame
    void Update()
    {
        buttonA.Tick(Input.GetButton(btnA));
        buttonB.Tick(Input.GetButton(btnB));
        buttonC.Tick(Input.GetButton(btnC));
        buttonD.Tick(Input.GetButton(btnD));
        buttonLB.Tick(Input.GetButton(btnLB));
        buttonLT.Tick(Input.GetButton(btnLT));



        Jup = -Input.GetAxis(axisJup);
        Jright = Input.GetAxis(axisJright);

        if (inputEnabled)
        {
            targetDup = Input.GetAxis(axisY);
            targetDright = Input.GetAxis(axisX);
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

        run = buttonA.IsPressing;
        defense = buttonLB.IsPressing;
        jump = buttonB.OnPressed;
        attack = buttonC.OnPressed;
    }



}
