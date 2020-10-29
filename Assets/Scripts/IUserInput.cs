using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUserInput : MonoBehaviour {

    [Header("===== Output signals =====")]

    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec;

    public float Jup;
    public float Jright;

    // 1. pressing signal
    public bool run;
    public bool defense;
    // 2. trigger once signal
    public bool jump;
    protected bool lastJump;
    public bool attack;
    protected bool lastAttack;
    // 3. double trigger


    [Header("===== Others =====")]

    public bool inputEnabled = true;

    protected float targetDup;
    protected float targetDright;
    protected float velocityDup;
    protected float velocityDright;



    protected Vector2 SquaretoCircle(float x, float y)
    {
        return new Vector2(x * Mathf.Sqrt(1 - y * y / 2.0f), y * Mathf.Sqrt(1 - x * x / 2.0f));
    }
}
