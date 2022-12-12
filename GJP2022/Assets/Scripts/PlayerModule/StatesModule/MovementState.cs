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
                player.SetState(new JumpingState(player)).Execute(PlayerAction.JUMP, ctx);
                break;
            case PlayerAction.ATTACK:
                player.SetState(new AttackState(player));
                break;
        }
    }

    #endregion Movement
    #endregion ---Mehtods---
}
