using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(1.0f, 1.0f, 1.0f));
    }
}
