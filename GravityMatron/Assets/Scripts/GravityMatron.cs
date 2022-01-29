using UnityEngine;

public class GravityMatron : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().canToggleModes = true;
            Destroy(gameObject);
        }
    }
}