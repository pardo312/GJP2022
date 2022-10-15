using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class MovementState : PlayerStateBase
{
    #region ---Fields---
    private int verticalHash;
    private int horizontalHash;

    private Rigidbody playerRb;
    private float movementSpeed;
    #endregion ---Fields---

    #region ---Mehtods---
    public MovementState(PlayerStateMachine player, params object[] parameters) : base(player)
    {
        playerRb = parameters[0] as Rigidbody;
        movementSpeed = (float)parameters[1];

        this.player = player;
        verticalHash = Animator.StringToHash("isMoving");
        horizontalHash = Animator.StringToHash("isRotating");
    }

    public override void ProcessInput(CallbackContext ctx)
    {
        Vector2 direction = ctx.ReadValue<Vector2>();
        if (playerRb)
            playerRb.velocity = new Vector3(direction.x, 0, direction.y).normalized * movementSpeed * Time.fixedDeltaTime * 100;
    }

    public override void UpdateState()
    {
    }
    #endregion ---Mehtods---
}
