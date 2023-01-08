using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class OnAir : PlayerStateBase
{
    #region ----Fields----
    private PlayerMovementController playerMovementController;
    private JumpController jumpController;
    #endregion ----Fields----

    #region ----Methods----
    public OnAir(PlayerStateMachine _player) : base(_player)
    {
        this.player = _player;
        this.playerMovementController = _player.playerMovementController;
        this.jumpController = _player.jumpController;
    }

    public override void Move(CallbackContext ctx)
    {
        playerMovementController.SetDirection(ctx.ReadValue<Vector2>());
    }

    public override void Jump(CallbackContext ctx)
    {
        if (ctx.started)
            jumpController.Jump();
        if (ctx.canceled)
            jumpController.ReleaseJump();
    }
    public override void Attack(bool isStrongAttack) { }

    public override void UpdateState()
    {
        playerMovementController.MovePlayer();
        playerMovementController.RotateModel();
        jumpController.LowHighJumpVerification();
        if (jumpController.IsGrounded())
            player.SetState(new OnGroundState(player));
    }


    #endregion ----Methods----
}
