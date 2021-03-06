using UnityEngine;

public class GravityMatron : MonoBehaviour
{
    // The object to destroy once item collected
    [SerializeField] private GameObject destroyObject;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GlobalSwitch.SwitchModeTo(SwitchMode.SideScroller);
            other.GetComponent<PlayerController>().PickUpFlipOTronic();
            Destroy(destroyObject);
            Destroy(gameObject);
        }
    }
}