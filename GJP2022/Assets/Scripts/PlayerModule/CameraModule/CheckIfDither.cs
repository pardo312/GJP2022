using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfDither : MonoBehaviour
{
    #region ----Fields----
    public Camera camera;
    public Transform player;
    public List<int> objectsWithDitherActivated = new List<int>();
    #endregion ----Fields----

    #region ----Methods----

    void Update()
    {
        RaycastHit hit;
        Vector3 direction = (player.position - camera.transform.position).normalized;
        if (Physics.Raycast(new Ray(camera.transform.position, direction), out hit))
        {
            MeshRenderer meshRenderer;
            if (!hit.collider.TryGetComponent<MeshRenderer>(out meshRenderer))
                return;

            Material mat = meshRenderer.material;
            if (mat.HasProperty("_ActivateDither") && mat.GetInt("_ActivateDither") == 0)
            {
                objectsWithDitherActivated.Add(hit.collider.gameObject.GetInstanceID());
                mat.SetInt("_ActivateDither", 1);
            }

            if (objectsWithDitherActivated.Contains(hit.collider.gameObject.GetInstanceID()))
            {
                mat.SetInt("_ActivateDither", 0);
                objectsWithDitherActivated.Remove(hit.collider.gameObject.GetInstanceID());
            }
        }

    }
    #endregion ----Methods----
}
