using System;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed;
    public float jumpSpeed;
    // set to true when picking up GravityMatron
    public bool canToggleModes = false;
    // set to room's respawn point upon entering room
    public Vector3 respawnPosition;

    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private KeyCode switchModeKey;

    private Rigidbody2D _body;
    private float _gravity;

    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _gravity = _body.gravityScale;
        respawnPosition = transform.position;
    }

    private void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        switch (GlobalSwitch.currentMode)
        {
            case SwitchMode.TopDown:
                _body.gravityScale = 0.0f;
            
                _body.velocity = new Vector2(x, y) * maxSpeed;
                break;
            case SwitchMode.SideScroller:
                _body.gravityScale = _gravity;
            
                var vy = _body.velocity.y;
                if (Input.GetKeyDown(jumpKey) && IsGrounded())
                {
                    vy = jumpSpeed;
                }
                _body.velocity = new Vector2(x * maxSpeed, vy);
                break;
        }

        if (Input.GetKeyDown(switchModeKey) && canToggleModes)
        {
            switch (GlobalSwitch.currentMode)
            {
                case SwitchMode.SideScroller:
                    GlobalSwitch.SwitchModeTo(SwitchMode.TopDown);
                    break;
                case SwitchMode.TopDown:
                    GlobalSwitch.SwitchModeTo(SwitchMode.SideScroller);
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Kill();
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(
            transform.position, 
            new Vector2(1.0f, 1.0f),
            0.0f, 
            Vector2.down, 
            1.5f,
            LayerMask.GetMask("Jumpable")
        ).collider != null;
    }

    private void OnDrawGizmos()
    {
        if (IsGrounded())
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(1.0f, 1.0f, 1.0f));
        }
    }

    // Callback to cause the player to respawn at the start of the room
    public void Kill()
    {
        transform.position = respawnPosition;
        _body.velocity = Vector2.zero;
        GlobalSwitch.SwitchModeTo(SwitchMode.TopDown);
    }
}
