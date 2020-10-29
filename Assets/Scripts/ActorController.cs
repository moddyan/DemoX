using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{

    public GameObject model;
    public IUserInput pi;
    public float walkSpeed = 2.4f;
    public float runMultiplier = 2.0f;
    public float jumpVelocity = 3.0f;
    public float rollVelocity = 1.0f;

    [Header("===== Friction Settings  =====")]
    public PhysicMaterial frictionOne;
    public PhysicMaterial frictionZero;

    private Animator anim;
    private Rigidbody rigid;
    private Vector3 planarVec;
    private Vector3 thrustVec;
    private bool canAttack;
    private bool lockPlanar = false;
    private CapsuleCollider col;
    private float lerpTarget;
    private Vector3 deltaPos;

    // Use this for initialization
    void Awake()
    {
        var inputs = GetComponents<IUserInput>();
        foreach (var input in inputs)
        {
            if (input.enabled)
            {
                pi = input;
                break;
            }
        }
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), pi.run ? 2.0f : 1.0f, 0.5f));
        anim.SetBool("defense", pi.defense);

        if (pi.jump && rigid.velocity.magnitude > 0f)
        {
            anim.SetTrigger("roll");
        }
        if (pi.jump)
        {
            anim.SetTrigger("jump");
            canAttack = false;
        }
        if (pi.attack && checkState("ground") && canAttack)
        {
            anim.SetTrigger("attack");
        }

        if (pi.Dmag > 0.1f)
        {
            model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);
        }
        if (lockPlanar==false)
        {
            planarVec = pi.Dmag * model.transform.forward * walkSpeed * (pi.run ? runMultiplier : 1.0f);
        }

    }

    private void FixedUpdate()
    {
        rigid.position += deltaPos;
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
        thrustVec = Vector3.zero;
        deltaPos = Vector3.zero;
    }


    private bool checkState(string stateName, string layerName = "Base Layer")
    {
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsName(stateName);
    }

    /// <summary>
    /// Message processing block
    /// </summary>
    public void OnJumpEnter()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
        thrustVec = new Vector3(0, jumpVelocity, 0);
    }

    public void IsGround()
    {
        anim.SetBool("isGround", true);
    }

    public void IsNotGround()
    {
        anim.SetBool("isGround", false);
    }

    public void OnGroundEnter()
    {
        pi.inputEnabled = true;
        lockPlanar = false;   
        canAttack = true;
        col.material = frictionOne;
    }

    public void OnGroundExit()
    {
        pi.inputEnabled = true;
        lockPlanar = false;
        canAttack = true;
        col.material = frictionZero;
    }

    public void OnFallEnter()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
    }

    public void OnRollEnter()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
        thrustVec = new Vector3(0, rollVelocity, 0);
    }

    public void OnJabEnter()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
    }

    public void OnJabUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("jabVelocity");
    }

    public void OnAttack1hAEnter()
    {
        pi.inputEnabled = false;
        planarVec = Vector3.zero;
        lerpTarget = 1.0f;
    }

    public void OnAttack1hAUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("attack1hAVelocity");
        float currentWeight = Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("Attack")), lerpTarget, 0.5f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack"), currentWeight);
    }

    public void OnAttackIdleEnter()
    {
        pi.inputEnabled = true;
        lerpTarget = 0.0f;
    }

    public void OnAttackIdleUpdate()
    {
        float currentWeight = Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("Attack")), lerpTarget, 0.5f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack"), currentWeight);
    }

    public void OnUpdateRootMotion(object _deltaPos)
    {
        if (checkState("attack1hC", "Attack"))
        {
            deltaPos += (0.6f * deltaPos + 0.4f * (Vector3)_deltaPos);
        }
    }
}
