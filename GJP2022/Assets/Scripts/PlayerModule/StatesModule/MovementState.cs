using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
public class OnGroundState : PlayerStateBase
{
    #region ---Fields---
    private PlayerMovementController playerMovementController;
    private JumpController jumpController;
    #endregion ---Fields---

    #region ---Mehtods---
    #region State Base
    public OnGroundState(PlayerStateMachine _player) : base(_player)
    {
        this.player = _player;
        this.playerMovementController = _player.playerMovementController;
        this.jumpController = _player.jumpController;
    }

    public override void UpdateState()
    {
        playerMovementController.MovePlayer();
        playerMovementController.RotateModel();
    }
    #endregion State Base

    #region Movement
    public override void Move(CallbackContext ctx)
    {
        playerMovementController.SetDirection(ctx.ReadValue<Vector2>());
    }

    public override void Jump(CallbackContext ctx)
    {
        player.SetState(new OnAir(player)).Jump(ctx);
    }

    public override void Attack(bool isStrongAttack)
    {
        player.SetState(new AttackingState(player)).Attack(isStrongAttack);
    }

    #endregion Movement
    #endregion ---Mehtods---
}
