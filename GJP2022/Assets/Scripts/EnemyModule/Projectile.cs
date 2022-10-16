using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int projectileDamage = 10;
    public float speed = 5;
    public string tagTarget = "Player";

    private Vector3 direction;

    public void Init(Vector3 _direction)
    {
        direction = _direction;
    }

    private void FixedUpdate()
    {
        transform.position += direction * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagTarget))
        {
            IDamageable character = other.GetComponent<IDamageable>();
            character.AddDamage(new NormalDamage() { amount = projectileDamage, target = character });
            Destroy(this.gameObject);
        }

    }
}