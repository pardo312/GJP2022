using UnityEngine;

public abstract class CollectableControllerBase : MonoBehaviour
{
    #region ----Fields----
    public bool m_shouldRotate = false;
    public float m_rotateSpeed = 2f;
    public Transform m_modelTranform;
    #endregion ----Fields----

    #region ----Methods----
    public virtual void Update()
    {
        if (m_shouldRotate)
            m_modelTranform.Rotate(new Vector3(0, m_rotateSpeed, 0));
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            CollideWithPlayer(other);
    }

    private protected virtual void CollideWithPlayer(Collider player)
    {
        Destroy(this.gameObject);
    }
    #endregion ----Methods----
}
