using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var pos = transform.position;
            other.GetComponent<PlayerController>().respawnPosition = new Vector3(pos.x, pos.y, 0.0f);
        }
    }
}
