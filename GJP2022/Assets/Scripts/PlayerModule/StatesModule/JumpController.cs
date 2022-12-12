using Jiufen.Audio;
using UnityEngine;

public class JumpController:MonoBehaviour
{
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private Animator animator;

    private float fallSpeed;
    private float jumpSpeed;
    private bool jumping = false;

    public int jumpState = 0;

    public void Jump()
    {
        if (jumpState < 2)
        {
            AudioManager.PlayAudio("SFX_JUMP");
            animator.SetTrigger("Jump");
            jumping = true;
            playerRb.velocity = new Vector3(playerRb.velocity.x, jumpSpeed, playerRb.velocity.z);
            jumpState++;
        }
    }

    public void ReleaseJump()
    {
        jumping = false;
    }

    public void LowHighJumpVerification()
    {
        if (playerRb.velocity.y > 0 && !jumping)
            playerRb.velocity += Vector3.up * Physics.gravity.y * (2.5f) * Time.fixedDeltaTime;
        else
            playerRb.velocity += Vector3.up * Physics.gravity.y * (1) * Time.fixedDeltaTime;
    }

    public bool IsGrounded()
    {
        bool isGrounded = Physics.Raycast(playerRb.transform.position, -Vector3.up, 1);
        if (isGrounded)
            jumpState = 0;

        return isGrounded;
    }
}
