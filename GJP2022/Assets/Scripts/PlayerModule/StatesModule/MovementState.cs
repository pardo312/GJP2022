using Jiufen.Audio;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    bool previousAttackWasStrong = false;
    private float cooldownAttack;
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
        cooldownAttack = (float)parameters[5];

        verticalHash = Animator.StringToHash("isMoving");
        lightAttackHash = Animator.StringToHash("lightAttack");
        strongAttackHash = Animator.StringToHash("strongAttack");
        horizontalHash = Animator.StringToHash("isRotating");
    }

    public override void UpdateState()
    {
        if (playerRb)
        {
            MovePlayer();
            RotateModel();
            LowHighJumpVerification();
        }

        AttackQueue();
        ComboCounterTick();
        if (IsGrounded())
            jumpState = 0;
    }


    #region Move Inputs
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
                AudioManager.PlayAudio("SFX_JUMP");
                player.animator.SetTrigger("Jump");
                jumping = true;
                playerRb.velocity = new Vector3(playerRb.velocity.x, jumpSpeed, playerRb.velocity.z);
                jumpState++;
            }
        }
        if (ctx.canceled)
            jumping = false;
    }
    #endregion Move Inputs

    #region MoveHelpers
    #region Movement
    const float footstepSoundCooldown = .5f;
    float timerFootStep = footstepSoundCooldown;
    private void MovePlayer()
    {
        timerFootStep -= Time.deltaTime;
        if (timerFootStep <= 0)
        {
            AudioManager.PlayAudio("SFX_FOOTSTEP_" + Random.Range(1, 6));
            timerFootStep = footstepSoundCooldown;
        }
        float movementMultiplier = movementSpeed * Time.fixedDeltaTime * 1000;
        playerRb.AddRelativeForce(direction.x * movementMultiplier, 0, direction.y * movementMultiplier, ForceMode.VelocityChange);
        playerRb.velocity = new Vector3(0, playerRb.velocity.y, 0);
        player.animator.SetFloat("velocity", direction.magnitude);
    }

    public async void OnPathComplete()
    {
    }

    private void RotateModel()
    {
        if (direction.x != 0 || direction.y != 0)
        {
            Transform modelTransform = playerRb.transform.GetChild(0);
            modelTransform.rotation = Quaternion.Lerp(modelTransform.rotation, Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y), Vector3.up), 5 * Time.deltaTime);
        }
    }
    #endregion Movement

    #region Jump
    private void LowHighJumpVerification()
    {
        if (playerRb.velocity.y > 0 && !jumping)
            playerRb.velocity += Vector3.up * Physics.gravity.y * (2.5f) * Time.fixedDeltaTime;
        else
            playerRb.velocity += Vector3.up * Physics.gravity.y * (1) * Time.fixedDeltaTime;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(playerRb.transform.position, -Vector3.up, 1);
    }
    #endregion Jump
    #endregion MoveHelpers


    #region Attack Inputs
    private float timerAttackCooldown;
    public override void Attack(bool isStrongAttack)
    {
        if (queueAttacks.Count < 3 && !comboGrabbed)
            queueAttacks.Enqueue(isStrongAttack);
        if (queueAttacks.Count == 3)
            comboGrabbed = true;
    }

    public Queue<bool> queueAttacks = new Queue<bool>();
    public bool comboGrabbed = false;
    public void AttackQueue()
    {
        if (timerAttackCooldown > 0)
            timerAttackCooldown -= Time.deltaTime;

        if (timerAttackCooldown <= 0 && queueAttacks.Count > 0)
        {
            timerAttackCooldown = cooldownAttack;
            ExecuteAttack(queueAttacks.Dequeue());
            if (queueAttacks.Count == 0 && comboGrabbed)
                comboGrabbed = false;
        }
    }

    public void ExecuteAttack(bool isStrongAttack)
    {
        AttackAnimations(isStrongAttack);
        characterResources.comboCounter++;
        DamageEnemy(isStrongAttack);
    }

    private void DamageEnemy(bool isStrongAttack)
    {
        Collider[] attackRange = Physics.OverlapBox(player.transform.position + (player.transform.forward * 2f), player.transform.localScale * 4, Quaternion.identity, LayerMask.GetMask("Enemy"));
        if (attackRange.Length <= 0)
            return;

        IDamageable enemy = attackRange[0].GetComponent<IDamageable>();
        enemy.AddDamage(new NormalDamage() { amount = isStrongAttack ? 20 : 10, target = enemy });
    }
    #endregion Attack Inputs

    #region  Attack helpers
    private const float stopComboCooldown = 2;
    private float timerToStopCombo = stopComboCooldown;
    private int previousComboCounter = 0;
    private void ComboCounterTick()
    {
        if (characterResources.comboCounter > 0)
        {
            if (previousComboCounter == characterResources.comboCounter)
            {
                timerToStopCombo -= Time.deltaTime;
                if (timerToStopCombo <= 0)
                {
                    timerToStopCombo = stopComboCooldown;
                    characterResources.comboCounter = 0;
                    previousComboCounter = 0;
                    player.animator.SetFloat(strongAttackHash, 0);
                    player.animator.SetFloat(lightAttackHash, 0);
                }
            }
            else
            {
                timerToStopCombo = stopComboCooldown;
                previousComboCounter = characterResources.comboCounter;
            }
        }
    }

    private void AttackAnimations(bool isStrongAttack)
    {
        int attackAnim = (characterResources.comboCounter % 3) + 1;
        //if (attackAnim == 1 || previousAttackWasStrong != isStrongAttack)
        player.animator.SetTrigger("Attacking");

        if (previousAttackWasStrong != isStrongAttack)
        {
            previousAttackWasStrong = isStrongAttack;
            player.animator.SetFloat(isStrongAttack ? lightAttackHash : strongAttackHash, 0);
        }

        player.animator.SetFloat(isStrongAttack ? strongAttackHash : lightAttackHash, attackAnim);
    }
    #endregion Player Attack
    #endregion ---Mehtods---
}
