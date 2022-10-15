using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[System.Serializable]
public abstract class PlayerStateBase
{
    protected PlayerStateMachine player;

    public PlayerStateBase(PlayerStateMachine player)
    {
        this.player = player;
    }

    public abstract void UpdateState();
    public abstract void ProcessInput(CallbackContext ctx);
}
