using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackingState : PlayerStateBase
{
    #region ----Fields----
    private float timerAttackCooldown;
    public bool? currentAttack = null;
    public bool hasCombo = false;

    private const float stopComboCooldown = 2f;
    private float timerToStopCombo = stopComboCooldown;
    private int previousComboCounter = 0;

    private float cooldownAttack;
    private CharacterResources characterResources;
    bool previousAttackWasStrong = false;

    private int lightAttackHash;
    private int strongAttackHash;

    private PlayerMovementController playerMovementController;
    #endregion ----Fields----

    #region ----Methods----
    #region State Methods
    public AttackingState(PlayerStateMachine player, params object[] parameters) : base(player)
    {
        characterResources = player.CharacterResources;
        cooldownAttack = player.Cooldown;
        this.playerMovementController = player.playerMovementController;

        playerMovementController.StopMovement();
        lightAttackHash = Animator.StringToHash("lightAttack");
        strongAttackHash = Animator.StringToHash("strongAttack");
    }

    public override void UpdateState()
    {
        playerMovementController.StopMovement();
        playerMovementController.RotateModel();
        AttackQueue();
        ComboCounterTick();
    }

    public int attackCount = 0;
    public override void Attack(bool isStrongAttack)
    {
        if (attackCount == 3 || currentAttack.HasValue)
            return;
        currentAttack = isStrongAttack;
        attackCount++;
        Debug.Log("attack: " + attackCount);
    }

    public override void Move(InputAction.CallbackContext ctx)
    {
        playerMovementController.SetDirection(ctx.ReadValue<Vector2>());
    }

    public override void Jump(InputAction.CallbackContext ctx) { }
    #endregion State Methods

    #region Attack Inputs
    public void AttackQueue()
    {
        if (player.hasBeenHit)
        {
            player.SetState(new OnGroundState(player));
            return;
        }

        if (timerAttackCooldown > 0)
            timerAttackCooldown -= Time.deltaTime;

        if (timerAttackCooldown <= 0 && !isAttacking)
        {
            // Attack before time limit
            if (currentAttack != null)
            {
                ExecuteAttack(currentAttack.Value, (attackExecuting) =>
                {
                    if (attackExecuting == 3)
                        player.SetState(new OnGroundState(player));
                });

                timerAttackCooldown = currentAnimTime / 1000;
            }
            // Time limit for continuing combo
            else
                player.SetState(new OnGroundState(player));
        }
    }
    public bool isAttacking = false;

    public async void ExecuteAttack(bool isStrongAttack, Action<int> onEndAttack)
    {
        var attackExecuting = attackCount;
        isAttacking = true;
        AttackAnimations(isStrongAttack);

        Debug.Log("time1");
        await Task.Delay((int)(currentAnimTime * .5f));

        characterResources.comboCounter++;
        currentAttack = null;
        DamageEnemy(isStrongAttack);

        Debug.Log("time2");
        await Task.Delay((int)(currentAnimTime * .5f));

        Debug.Log("time3");
        isAttacking = false;
        onEndAttack?.Invoke(attackExecuting);
    }

    private void DamageEnemy(bool isStrongAttack)
    {
        Transform model = player.transform.GetChild(0).GetChild(0);
        Collider[] attackRange = Physics.OverlapBox(model.transform.position + (model.forward * 1.5f), player.transform.localScale * 4, Quaternion.identity, LayerMask.GetMask("Enemy"));
        if (attackRange.Length <= 0)
            return;

        IDamageable enemy = attackRange[0].GetComponent<IDamageable>();
        enemy.AddDamage(new NormalDamage() { amount = isStrongAttack ? 40 : 20, target = enemy });
    }
    #endregion Attack Inputs

    #region  Attack helpers
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
        player.animator.SetTrigger("Attacking");
        int animHash = (isStrongAttack ? lightAttackHash : strongAttackHash);

        if (previousAttackWasStrong != isStrongAttack)
        {
            previousAttackWasStrong = isStrongAttack;
            player.animator.SetFloat(animHash, 0);
        }
        player.animator.SetFloat(isStrongAttack ? strongAttackHash : lightAttackHash, attackCount);

        string attackName = (isStrongAttack ? "Strong" : "Light") + attackCount;
        currentAnimTime = (player.animator.runtimeAnimatorController.animationClips.First(animClip => animClip.name.Equals(attackName)).length / 1.5f) * 1000;
    }
    private float currentAnimTime = -1;

    #endregion Player Attack
    #endregion ----Methods----
}
