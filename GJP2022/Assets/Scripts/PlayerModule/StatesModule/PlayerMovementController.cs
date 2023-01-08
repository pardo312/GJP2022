using Jiufen.Audio;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    #region ----Fields----
    const float footstepSoundCooldown = .5f;

    private int verticalHash;
    private int horizontalHash;

    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private Animator animator;

    private Vector2 direction;
    private float timerFootStep = footstepSoundCooldown;
    [SerializeField] private float movementSpeed;
    #endregion ----Fields----

    #region ----Methods----
    public void Start()
    {
        verticalHash = Animator.StringToHash("isMoving");
        horizontalHash = Animator.StringToHash("isRotating");
    }
    public void SetDirection(Vector2 _direction)
    {
        direction = _direction;
    }

    public void MovePlayer()
    {
        if (playerRb == null)
            return;

        timerFootStep -= Time.deltaTime;
        if (timerFootStep <= 0)
        {
            AudioManager.PlayAudio("SFX_FOOTSTEP_" + Random.Range(1, 6));
            timerFootStep = footstepSoundCooldown;
        }
        float movementMultiplier = movementSpeed * Time.fixedDeltaTime * 100;
        playerRb.AddRelativeForce(direction.x * movementMultiplier, 0, direction.y * movementMultiplier, ForceMode.VelocityChange);
        playerRb.velocity = new Vector3(0, playerRb.velocity.y, 0);
        animator.SetFloat("velocity", direction.magnitude);
    }

    public void StopMovement()
    {
        playerRb.velocity = new Vector3(0, playerRb.velocity.y, 0);
    }
    public void RotateModel()
    {
        if (direction.x != 0 || direction.y != 0)
        {
            Transform modelTransform = playerRb.transform.GetChild(0);
            modelTransform.rotation = Quaternion.Lerp(modelTransform.rotation, Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y), Vector3.up), 5 * Time.deltaTime);
        }
    }
    #endregion ----Methods----
}
