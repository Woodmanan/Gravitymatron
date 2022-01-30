using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed;
    public float jumpSpeed;
    // set to true when picking up GravityMatron
    public bool canToggleModes;
    // set to room's respawn point upon entering room
    public Vector3 respawnPosition;

    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private KeyCode switchModeKey;
    [SerializeField] private KeyCode respawnKey;
    [SerializeField] private AudioClip flipToTopSound;
    [SerializeField] private AudioClip flipToSideSound;
    [SerializeField] private AudioClip flipOTronicSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip jumpSound;

    private Rigidbody2D _body;
    private Animator _anim;
    private AudioSource _audio;
    private float _gravity;
    private float _phaseTime;

    private void Start()
    {
        GlobalSwitch.SwitchModes += PlayFlipSound;
        
        _body = GetComponent<Rigidbody2D>();
        _anim = transform.GetChild(0).GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
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
                _anim.SetBool("SideOrTop", true);
                if (x != 0.0 || y != 0.0)
                {
                    _anim.SetFloat("Direction", Mathf.Rad2Deg * Mathf.Atan2(y, x));
                }

                _body.velocity = new Vector2(x, y) * maxSpeed;
                break;
            case SwitchMode.SideScroller:
                _body.gravityScale = _gravity;
                _anim.SetBool("SideOrTop", false);
                _anim.SetFloat("Walking", x != 0.0f ? 1.0f : 0.0f);
                if (x != 0.0f)
                {
                    _anim.SetBool("LeftOrRight", x > 0.0f);
                }

                var vy = _body.velocity.y;
                if (Input.GetKeyDown(jumpKey) && IsGrounded())
                {
                    if (y >= -.1f)
                    {
                        _audio.clip = jumpSound;
                        _audio.volume = 1;
                        _audio.Play();
                        vy = jumpSpeed;
                    }
                }

                _body.velocity = new Vector2(x * maxSpeed, vy);

                //Switch to semisolid mode if we're jumping!
                if (_body.velocity.y > .1f)
                {
                    gameObject.layer = 9; //NoSemisolidInteraction
                }
                else
                {
                    gameObject.layer = 0; //Default
                }

                if (Input.GetKey(jumpKey) && y < -.1f)
                {
                    _phaseTime = .2f;
                    
                }

                if (_phaseTime > 0)
                {
                    gameObject.layer = 9; //NoSemisolidInteraction
                    _phaseTime -= Time.deltaTime;
                }
                break;
        }

        if (Input.GetKeyDown(switchModeKey) && canToggleModes)
        {
            switch (GlobalSwitch.currentMode)
            {
                case SwitchMode.SideScroller:
                    GlobalSwitch.SwitchModeTo(SwitchMode.TopDown);
                    var xform = transform;
                    xform.position += new Vector3(0.0f, 0.01f, 0.0f);
                    break;
                case SwitchMode.TopDown:
                    GlobalSwitch.SwitchModeTo(SwitchMode.SideScroller);
                    break;
            }
        }

        if (Input.GetKeyDown(respawnKey))
        {
            Kill();
        }
    }

    public void PickUpFlipOTronic()
    {
        canToggleModes = true;
        _audio.clip = flipOTronicSound;
        _audio.volume = 0.12f;
        _audio.Play();
    }

    private void PlayFlipSound(SwitchMode mode)
    {
        switch (mode)
        {
            case SwitchMode.SideScroller:
                _audio.clip = flipToSideSound;
                _audio.volume = 1;
                _audio.Play();
                break;
            case SwitchMode.TopDown:
                _audio.clip = flipToTopSound;
                _audio.volume = 1;
                _audio.Play();
                break;
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
            LayerMask.GetMask("Jumpable", "Semisolid")
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
        _audio.clip = deathSound;
        _audio.volume = 0.25f;
        _audio.Play();
        // GlobalSwitch.SwitchModeTo(SwitchMode.TopDown);
    }
}
