using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMoveController : MonoBehaviour
{
    #region ---Fields---
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private float movementSpeed = 5;
    private GJP2022InputActions playerInput;
    #endregion ---Fields---

    #region ---Methods---
    void Start()
    {
        playerInput = new GJP2022InputActions();
        playerInput.PlayerMovement.Move.performed += SetMovement;
        playerInput.PlayerMovement.Move.canceled += SetMovement;
        playerInput.PlayerMovement.Move.Enable();

    }

    // Update is called once per frame
    void SetMovement(CallbackContext ctx)
    {
        Vector2 direction = ctx.ReadValue<Vector2>();
        if (direction.x != 0 && direction.y != 0)
            playerRb.velocity = new Vector3(direction.x, 0, direction.y) * movementSpeed * Time.deltaTime * 1000;
        else
            playerRb.velocity = Vector3.zero;
        Debug.Log(playerRb.velocity);
    }
    #endregion ---Methods---
}
