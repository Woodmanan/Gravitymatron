using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public SwitchMode activeMode;
    public float width;
    public Vector2 start;
    public Vector2 end;
    public float speed;
    public float length;

    BoxCollider2D collider;
    LineRenderer renderer;

    private bool setup = false;

    public Color activeColor;
    public Color inactiveColor;
    public float activeGlowIntensity;
    public float inactiveGlowIntensity;

    // Start is called before the first frame update
    void Start()
    {
        Freeze(GlobalSwitch.currentMode);
    }

    void Setup()
    {
        if (setup) return;
        collider = GetComponent<BoxCollider2D>();
        renderer = GetComponent<LineRenderer>();
        renderer.useWorldSpace = true;
        renderer.widthCurve = AnimationCurve.Constant(0, 100, width);
        setup = true;
        GlobalSwitch.SwitchModes += Freeze;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(Vector2 startPoint, Vector2 direction, float distance)
    {
        StartCoroutine(MainRoutine(startPoint, direction, distance));
    }

    IEnumerator MainRoutine(Vector2 startPoint, Vector2 direction, float distance)
    {
        IEnumerator shot = MoveLaser(startPoint, direction, distance);
        while (true)
        {
            if ((GlobalSwitch.currentMode & activeMode) > 0)
            {
                if (shot.MoveNext())
                {
                    yield return shot.Current;
                }
                else
                {
                    yield break;
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator MoveLaser(Vector2 startPoint, Vector2 direction, float distance)
    {
        Setup();
        //Initial spawning
        for (float currentLength = 0; currentLength < length; currentLength += speed * Time.deltaTime)
        {
            SetPoints(startPoint, startPoint + direction * currentLength);
            yield return null;
        }

        //Laser is fully extended, move it across the screen
        for (float traveledDistance = 0; traveledDistance < distance - length; traveledDistance += speed * Time.deltaTime)
        {
            SetPoints(startPoint + direction * traveledDistance, startPoint + direction * (traveledDistance + length));
            yield return null;
        }

        //Laser has reached the back wall, collapse it now
        for (float currentLength = length; currentLength > 0; currentLength -= speed * Time.deltaTime)
        {
            SetPoints(startPoint + direction * (distance - currentLength), startPoint + direction * distance);
            yield return null;
        }


        Destroy(this.gameObject);
        GlobalSwitch.SwitchModes -= Freeze;
    }

    public void SetPoints(Vector2 start, Vector2 end)
    {
        this.start = start;
        this.end = end;
        renderer.SetPosition(0, start);
        renderer.SetPosition(1, end);

        Vector2 offsets = new Vector2(Mathf.Abs(start.x - end.x), Mathf.Abs(start.y - end.y));

        collider.offset = Vector2.Min(start, end) + offsets / 2;

        offsets = new Vector2(Mathf.Max(offsets.x, width), Mathf.Max(offsets.y, width));

        collider.size = offsets;
    }

    public void Freeze(SwitchMode newMode)
    {
        Setup();
        
        if ((newMode & activeMode) > 0)
        {
            collider.isTrigger = true;
            gameObject.layer = 0; //Default
            renderer.material.color = activeColor;
            float factor = Mathf.Pow(2f, activeGlowIntensity);
            Color bright = new Color(activeColor.r * factor, activeColor.g * factor, activeColor.b * factor);
            renderer.material.SetColor("_EmissionColor", bright);
        }
        else
        {
            collider.isTrigger = false;
            gameObject.layer = 10; //Semisolid
            renderer.material.color = inactiveColor;
            float factor = Mathf.Pow(2f, inactiveGlowIntensity);
            Color bright = new Color(inactiveColor.r * factor, inactiveColor.g * factor, inactiveColor.b * factor);
            renderer.material.SetColor("_EmissionColor", bright);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            Debug.Log("Killing player!");
            collision.GetComponent<PlayerController>().Kill();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Touched the player!");
        }
    }
}
