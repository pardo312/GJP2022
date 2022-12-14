using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerDisableState : PlayerStateBase
{
    public PlayerDisableState(PlayerStateMachine player, params object[] parameters) : base(player) { }

    public override void Attack(bool isStrongAttack) { }
    public override void Jump(CallbackContext ctx) { }
    public override void Move(CallbackContext ctx) { }

    public override void UpdateState() { }
}
