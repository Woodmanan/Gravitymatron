using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed;
    public float gravity;

    [SerializeField] private KeyCode jumpKey;

    private Rigidbody2D _body;

    // Start is called before the first frame update
    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        _body.velocity = new Vector2(x, y) * maxSpeed;
        
        if (Input.GetKeyDown(jumpKey))
        {
            
        }
    }
}
