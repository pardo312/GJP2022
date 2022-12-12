using Jiufen.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static UnityEngine.InputSystem.InputAction;

public enum PlayerAction
{
    MOVE,
    JUMP,
    ATTACK
}

public class PlayerStateMachine : CharacterStateMachine
{
    #region ---Fields---
    [Header("StateMachine")]
    [SerializeField] private string stateName;
    [SerializeField] private PlayerStateBase currentState;
    public PlayerMovementController playerMovementController;
    public JumpController jumpController;

    [Header("Movment")]
    private GJP2022InputActions playerInput;
    public Rigidbody PlayerRb { get => playerRb; }
    [SerializeField] private Rigidbody playerRb;
    public float MovementSpeed { get => movementSpeed; }
    [SerializeField] private float movementSpeed = 5;
    public float JumpSpeed { get => jumpSpeed; }
    [SerializeField] private float jumpSpeed = 5;
    public float FallSpeed { get => jumpSpeed; }
    [SerializeField] private float fallSpeed = 1;


    [Header("Attack")]
    [SerializeField] private float cooldown = 2;
    public float Cooldown { get => cooldown; }

    [Header("Damage")]
    [SerializeField] private GameObject postProcessGO;
    Vignette vignette;

    //Class variables
    public bool hasBeenHit = false;
    #endregion ---Fields---

    #region ----Methods----

    #region Unity Methods
    private void Awake()
    {
        SetState(new PlayerDisableState(this));
        GameFlowManager.Singleton.OnGameStateChanged += HandleLevelStageChanged;
    }

    private void Start()
    {
        RegisterInputActions();
        InitiateVignette();
    }

    #region Start helpers

    public void RegisterInputActions()
    {
        playerInput = new GJP2022InputActions();

        playerInput.PlayerMovement.Attack1.started += (ctx) => currentState.Execute(PlayerAction.ATTACK, ctx, false);
        playerInput.PlayerMovement.Attack2.started += (ctx) => currentState.Execute(PlayerAction.ATTACK, true);

        playerInput.PlayerMovement.Jump.performed += (ctx) => currentState.Execute(PlayerAction.JUMP, ctx);
        playerInput.PlayerMovement.Jump.canceled += (ctx) => currentState.Execute(PlayerAction.JUMP, ctx);

        playerInput.PlayerMovement.Move.performed += (ctx) => currentState.Execute(PlayerAction.MOVE, ctx);
        playerInput.PlayerMovement.Move.canceled += (ctx) => currentState.Execute(PlayerAction.MOVE, ctx);

        playerInput.PlayerMovement.Attack1.Enable();
        playerInput.PlayerMovement.Attack2.Enable();
        playerInput.PlayerMovement.Jump.Enable();
        playerInput.PlayerMovement.Move.Enable();
    }


    public void InitiateVignette()
    {
        VolumeProfile volumeProfile = postProcessGO.GetComponent<Volume>()?.profile;
        if (!volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));
        if (!volumeProfile.TryGet(out vignette)) throw new System.NullReferenceException(nameof(vignette));
    }
    #endregion Start helpers

    private void FixedUpdate()
    {
        currentState.UpdateState();
    }
    #endregion Unity Methods

    #region State
    public PlayerStateBase SetState(PlayerStateBase state, params object[] parameters)
    {
        currentState = state;
        stateName = currentState.ToString();
        return currentState;
    }

    private void HandleLevelStageChanged(LevelStage stage)
    {
        if (stage == LevelStage.gameMode)
        {
            SetState(new OnGroundState(this));
        }
        if (stage == LevelStage.inbetween)
        {
            SetState(new PlayerDisableState(this));
        }
        if (stage == LevelStage.victory)
        {
            SetState(new PlayerDisableState(this));
        }
    }
    #endregion State

    #region Damage

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);

        bool isPlayerDead = CharacterResources.health <= 0;
        if (isPlayerDead)
        {
            animator.SetTrigger("Death");
            SetState(new PlayerDisableState(this));
        }
        else
        {
            HasBeenHitDelay();
            AudioManager.PlayAudio("SFX_HIT_1");
            animator.SetTrigger("Damage");
            vignette.color.Override(Color.red);
            vignette.intensity.Override(1 - (CharacterResources.health / 100));
        }
    }

    public async void HasBeenHitDelay()
    {
        hasBeenHit = true;
        await Task.Delay(1300);
        hasBeenHit = false;
    }

    public override void AddDamage(Damage damageTaken)
    {
        base.AddDamage(damageTaken);
        //animator.Play(getHitHash);
    }
    #endregion Damage
    #endregion ----Methods----
}
