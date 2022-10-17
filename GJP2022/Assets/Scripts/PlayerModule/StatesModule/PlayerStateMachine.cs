using Jiufen.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static UnityEngine.InputSystem.InputAction;

public class PlayerStateMachine : CharacterStateMachine
{
    #region ---Fields---
    [Header("StateMachine")]
    [SerializeField] private string stateName;
    [SerializeField] private PlayerStateBase currentState;

    [Header("Movment")]
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float jumpSpeed = 5;
    [SerializeField] private float fallSpeed = 1;
    private GJP2022InputActions playerInput;

    [Header("Attack")]
    [SerializeField] private float cooldown = 2;

    [Header("Damage")]
    [SerializeField] private GameObject postProcessGO;
    Vignette vignette;
    #endregion ---Fields---

    public void SetState(PlayerStateBase state)
    {
        currentState = state;
        stateName = currentState.ToString();
    }

    private void Awake()
    {
        SetState(new PlayerDisableState(this));
        GameFlowManager.Singleton.OnGameStateChanged += HandleLevelStageChanged;
    }

    private void Start()
    {
        playerInput = new GJP2022InputActions();
        playerInput.PlayerMovement.Attack1.started += (ctx) => currentState.Attack(false);
        playerInput.PlayerMovement.Attack1.Enable();

        playerInput.PlayerMovement.Attack2.started += (ctx) => currentState.Attack(true);
        playerInput.PlayerMovement.Attack2.Enable();

        playerInput.PlayerMovement.Jump.started += currentState.Jump;
        playerInput.PlayerMovement.Jump.canceled += currentState.Jump;
        playerInput.PlayerMovement.Jump.Enable();

        playerInput.PlayerMovement.Move.performed += currentState.Move;
        playerInput.PlayerMovement.Move.canceled += currentState.Move;
        playerInput.PlayerMovement.Move.Enable();

        VolumeProfile volumeProfile = postProcessGO.GetComponent<Volume>()?.profile;
        if (!volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));
        if (!volumeProfile.TryGet(out vignette)) throw new System.NullReferenceException(nameof(vignette));
    }

    private void FixedUpdate()
    {
        currentState.UpdateState();
    }

    private void HandleLevelStageChanged(LevelStage stage)
    {
        if (stage == LevelStage.gameMode)
        {
            SetState(new MovementState(this, playerRb, movementSpeed, jumpSpeed, fallSpeed, characterResources, cooldown));
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

    public bool hasBeenHit = false;
    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);

        if (characterResources.health <= 0)
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
            vignette.intensity.Override(1 - (characterResources.health / 100));
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
}
