using UnityEngine;
using System;
using System.Collections;
using Gamekit3D;
using UnityEngine.Events;


public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance
    {
        get { return s_Instance; }
    }

    public UnityEvent OnAimActivate;
    public UnityEvent OnAimDeactive;

    AbilitiesPrototype prototypeScript;

    protected static PlayerInput s_Instance;

    [HideInInspector]
    public bool playerControllerInputBlocked;

    protected Vector2 m_Movement;
    protected Vector2 m_Camera;
    protected bool m_Jump;
    protected bool m_Attack;
    protected bool m_Aim;
    protected bool m_Pause;
    protected bool m_ExternalInputBlocked;

    Actions Spartan;
    Vector3 moveInput;

    public Vector2 MoveInput
    {
        get
        {
            if(playerControllerInputBlocked || m_ExternalInputBlocked)
                return Vector2.zero;
            return m_Movement;
        }
    }

    public Vector2 CameraInput
    {
        get
        {
            if(playerControllerInputBlocked || m_ExternalInputBlocked)
                return Vector2.zero;
            return m_Camera;
        }
    }

    public bool JumpInput
    {
        get { return m_Jump && !playerControllerInputBlocked && !m_ExternalInputBlocked; }
    }

    public bool Attack
    {
        get { return m_Attack && !playerControllerInputBlocked && !m_ExternalInputBlocked; }
    }

    public bool Aim
    {
        get { return m_Aim && !playerControllerInputBlocked && !m_ExternalInputBlocked; }
    }

    public bool Pause
    {
        get { return m_Pause; }
    }

    WaitForSeconds m_AttackInputWait;
    Coroutine m_AttackWaitCoroutine;

    const float k_AttackInputDuration = 0.03f;

    void Awake()
    {
        m_AttackInputWait = new WaitForSeconds(k_AttackInputDuration);

        if (s_Instance == null)
            s_Instance = this;
        else if (s_Instance != this)
            throw new UnityException("There cannot be more than one PlayerInput script.  The instances are " + s_Instance.name + " and " + name + ".");

        if (TryGetComponent(out AbilitiesPrototype prototype))
        {
            if (prototype)
            {
                Debug.Log($"Found {prototypeScript}");
                prototypeScript = prototype;
            }
            else
            {
                Debug.Log("Unable to find script");
                return;
            }
        }
    }


    void Update()
    {
        //Spartan.Player.Move.performed += i => moveInput = i.ReadValue<Vector3>();
        
        m_Movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        m_Camera.Set(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        m_Jump = Input.GetButton("Jump");

        if (Input.GetButtonDown("Fire1"))
        {
            if (m_AttackWaitCoroutine != null)
                StopCoroutine(m_AttackWaitCoroutine);

            m_AttackWaitCoroutine = StartCoroutine(AttackWait());
        }

        bool tempAim = m_Aim;
        m_Aim = Input.GetButton("Fire2");
        if (m_Aim != tempAim)
        {
            if (m_Aim)
            {
                OnAimActivate.Invoke();
            }
            else
            {
                OnAimDeactive.Invoke();
            }
        }

        m_Pause = Input.GetButtonDown ("Pause");
    }

    IEnumerator AttackWait()
    {
        m_Attack = true;

        yield return m_AttackInputWait;

        m_Attack = false;
    }

    public bool HaveControl()
    {
        return !m_ExternalInputBlocked;
    }

    public void ReleaseControl()
    {
        m_ExternalInputBlocked = true;
    }

    public void GainControl()
    {
        m_ExternalInputBlocked = false;
    }

    private void OnEnable()
    {
        if (Spartan == null)
        {
            Spartan = new Actions();
            Spartan.Player.Roll.performed += i => 
            {
                //PlayerController.instance.m_Animator.CrossFade("Roll", 0.2f); 
                PlayerController.instance.canRoll = true;
            };

            Spartan.Player.Move.performed += i => 
            { 
                moveInput = i.ReadValue<Vector2>(); 

            };

            //Button inputs
            Spartan.Player.Ability1.performed += i =>
            {
                if (prototypeScript.CurrentAbility != 1)
                {
                    prototypeScript.CurrentAbility = 1;
                    prototypeScript.ability[0].isSelected = true;
                    prototypeScript.ability[1].isSelected = false;
                    prototypeScript.ability[2].isSelected = false;
                }
                else
                {
                    prototypeScript.CurrentAbility = 0;
                    prototypeScript.ability[0].isSelected = false;
                }
            };
            Spartan.Player.Ability2.performed += i =>
            {
                if (prototypeScript.CurrentAbility != 2)
                {
                    prototypeScript.CurrentAbility = 2;
                    prototypeScript.ability[0].isSelected = false;
                    prototypeScript.ability[1].isSelected = true;
                    prototypeScript.ability[2].isSelected = false;
                }
                else
                {
                    prototypeScript.CurrentAbility = 0;
                    prototypeScript.ability[1].isSelected = false;
                }
            };
            Spartan.Player.Ability3.performed += i =>
            {
                if (prototypeScript.CurrentAbility != 3)
                {
                    prototypeScript.CurrentAbility = 3;
                    prototypeScript.ability[0].isSelected = false;
                    prototypeScript.ability[1].isSelected = false;
                    prototypeScript.ability[2].isSelected = true;
                }
                else
                {
                    prototypeScript.CurrentAbility = 0;
                    prototypeScript.ability[2].isSelected = false;
                }
            };
        }
        Spartan.Enable();
    }

    private void OnDisable()
    {
        Spartan.Disable();
    }
}
