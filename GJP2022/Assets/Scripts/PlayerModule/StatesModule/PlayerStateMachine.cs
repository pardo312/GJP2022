using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    //Draw the Box Overlap as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
        Gizmos.DrawWireCube(transform.position + (transform.forward * 1.5f), transform.localScale * 2);
    }

    private void Start()
    {
        playerInput = new GJP2022InputActions();
        playerInput.PlayerMovement.Attack1.started += currentState.Attack;
        playerInput.PlayerMovement.Attack1.canceled += currentState.Attack;
        playerInput.PlayerMovement.Attack1.Enable();

        playerInput.PlayerMovement.Jump.started += currentState.Jump;
        playerInput.PlayerMovement.Jump.canceled += currentState.Jump;
        playerInput.PlayerMovement.Jump.Enable();

        playerInput.PlayerMovement.Move.performed += currentState.Move;
        playerInput.PlayerMovement.Move.canceled += currentState.Move;
        playerInput.PlayerMovement.Move.Enable();
    }

    private void FixedUpdate()
    {
        currentState.UpdateState();
    }

    private void HandleLevelStageChanged(LevelStage stage)
    {
        if (stage == LevelStage.gameMode)
        {
            SetState(new MovementState(this, playerRb, movementSpeed, jumpSpeed, fallSpeed));
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

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        if (characterResources.health <= 0)
            SetState(new PlayerDisableState(this));
    }

    public override void AddDamage(Damage damageTaken)
    {
        base.AddDamage(damageTaken);
        //animator.Play(getHitHash);
    }
}
