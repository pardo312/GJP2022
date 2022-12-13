using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackingState : PlayerStateBase
{
    #region ----Fields----
    private float timerAttackCooldown;
    public Queue<bool> queueAttacks = new Queue<bool>();
    public bool hasCombo = false;

    private const float stopComboCooldown = 2;
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

        lightAttackHash = Animator.StringToHash("lightAttack");
        strongAttackHash = Animator.StringToHash("strongAttack");
    }

    public override void UpdateState()
    {
        playerMovementController.RotateModel();
        AttackQueue();
        ComboCounterTick();
    }

    public override void Attack(bool isStrongAttack)
    {
        if (queueAttacks.Count < 3 && !hasCombo)
            queueAttacks.Enqueue(isStrongAttack);
        if (queueAttacks.Count == 3)
            hasCombo = true;
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
            EndCombo();
        else
        {
            if (timerAttackCooldown > 0)
                timerAttackCooldown -= Time.deltaTime;

            if (timerAttackCooldown <= 0 && queueAttacks.Count > 0)
            {
                timerAttackCooldown = cooldownAttack;
                ExecuteAttack(queueAttacks.Dequeue());
                if (queueAttacks.Count == 0 && hasCombo)
                    EndCombo();
            }
        }
    }

    public void EndCombo()
    {
        hasCombo = false;
        queueAttacks.Clear();
        player.SetState(new OnGroundState(player));
    }

    public async void ExecuteAttack(bool isStrongAttack)
    {
        AttackAnimations(isStrongAttack);
        characterResources.comboCounter++;
        await Task.Delay(1300);
        DamageEnemy(isStrongAttack);
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
    #endregion ----Methods----
}
