using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class JumpingState : PlayerStateBase
{
    #region ----Fields----
    private PlayerMovementController playerMovementController;
    private JumpController jumpController;
    #endregion ----Fields----

    #region ----Methods----
    public JumpingState(PlayerStateMachine _player) : base(_player)
    {
        this.player = _player;
        this.playerMovementController = _player.playerMovementController;
        this.jumpController = _player.jumpController;
    }

    public override void Execute(params object[] parameters)
    {
        PlayerAction playerStateToExecute = (PlayerAction)parameters[0];
        CallbackContext ctx = (CallbackContext)parameters[1];
        switch (playerStateToExecute)
        {
            case PlayerAction.MOVE:
                playerMovementController.SetDirection(ctx.ReadValue<Vector2>());
                break;
            case PlayerAction.JUMP:
                if (ctx.started)
                    jumpController.Jump();
                if (ctx.canceled)
                    jumpController.ReleaseJump();
                break;
        }
    }

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
