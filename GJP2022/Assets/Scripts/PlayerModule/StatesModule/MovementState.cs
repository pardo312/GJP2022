using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class MovementState : PlayerStateBase
{
    #region ---Fields---
    private int verticalHash;
    private int horizontalHash;
    private int lightAttackHash;
    private int strongAttackHash;

    private Rigidbody playerRb;
    private float movementSpeed;
    Vector2 direction;

    private int jumpState = 0;
    private float jumpSpeed;
    private float fallSpeed;
    bool jumping = false;
    private CharacterResources characterResources;
    #endregion ---Fields---

    #region ---Mehtods---
    public MovementState(PlayerStateMachine player, params object[] parameters) : base(player)
    {
        this.player = player;

        playerRb = parameters[0] as Rigidbody;
        movementSpeed = (float)parameters[1];
        jumpSpeed = (float)parameters[2];
        fallSpeed = (float)parameters[3];
        characterResources = parameters[4] as CharacterResources;

        verticalHash = Animator.StringToHash("isMoving");
        lightAttackHash = Animator.StringToHash("lightAttack");
        strongAttackHash = Animator.StringToHash("strongAttack");
        horizontalHash = Animator.StringToHash("isRotating");
    }

    public override void Attack(bool isStrongAttack)
    {
        Collider[] attackRange = Physics.OverlapBox(player.transform.position + (player.transform.forward * 1.5f), player.transform.localScale * 2, Quaternion.identity, LayerMask.GetMask("Player"));
        if (attackRange.Length <= 0)
            return;

        IDamageable enemy = attackRange[0].GetComponent<IDamageable>();
        enemy.AddDamage(new NormalDamage() { amount = isStrongAttack ? 20 : 10, target = enemy });
        characterResources.comboCounter++;

        player.animator.SetInteger(isStrongAttack ? strongAttackHash : lightAttackHash, characterResources.comboCounter);
    }

    public override void Move(CallbackContext ctx)
    {
        direction = ctx.ReadValue<Vector2>();
    }

    public override void Jump(CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (jumpState < 2)
            {
                jumping = true;
                playerRb.velocity = new Vector3(playerRb.velocity.x, jumpSpeed, playerRb.velocity.z);
                jumpState++;
            }
        }
        if (ctx.canceled)
            jumping = false;
    }

    private float timerToStopCombo;
    public override void UpdateState()
    {
        if (playerRb)
        {
            float movementMultiplier = movementSpeed * Time.fixedDeltaTime * 1000;
            playerRb.AddRelativeForce(direction.x * movementMultiplier, 0, direction.y * movementMultiplier, ForceMode.VelocityChange);
            playerRb.velocity = new Vector3(0, playerRb.velocity.y, 0);
            Transform modelTransform = playerRb.transform.GetChild(0);
            modelTransform.rotation = Quaternion.Lerp(modelTransform.rotation, Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y), Vector3.up), 5 * Time.deltaTime);

            if (playerRb.velocity.y > 0 && !jumping)
                playerRb.velocity += Vector3.up * Physics.gravity.y * (2.5f) * Time.fixedDeltaTime;
            else
                playerRb.velocity += Vector3.up * Physics.gravity.y * (1) * Time.fixedDeltaTime;

        }

        if (characterResources.comboCounter > 0)
        {
            timerToStopCombo -= Time.deltaTime;
            if (timerToStopCombo <= 0)
                characterResources.comboCounter = 0;
        }
        if (IsGrounded())
            jumpState = 0;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(playerRb.transform.position, -Vector3.up, 1);
    }

    #endregion ---Mehtods---
}
