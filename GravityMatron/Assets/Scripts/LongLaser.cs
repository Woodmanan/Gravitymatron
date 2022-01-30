using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongLaser : MonoBehaviour
{
    public SwitchMode activeMode;
    Vector2 start;
    Vector2 end;
    public Vector2 direction;
    public float maxDist;
    public float width;
    public float timeToSpan;
    public float initialDelay;
    public float holdTime;
    public float repeatAfter;

    public Color activeColor;
    public Color inactiveColor;
    public float activeGlowIntensity;
    public float inactiveGlowIntensity;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting Call!");
        GlobalSwitch.SwitchModes += Freeze;
        StartCoroutine(MainRoutine());
        GetComponent<LineRenderer>().useWorldSpace = true;
        Freeze(GlobalSwitch.currentMode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Freeze(SwitchMode newMode)
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        LineRenderer renderer = GetComponent<LineRenderer>();
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

    IEnumerator MainRoutine()
    {
        IEnumerator shot = ExtendLaser();
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

    IEnumerator ExtendLaser()
    {
        for (float t = 0; t < initialDelay; t += Time.deltaTime)
        {
            yield return null;
        }

        while (true)
        {
            if (timeToSpan == 0)
            {
                Debug.LogError("Speed must be positive!", this);
                yield break;
            }
            float distance;
            RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, direction, maxDist, LayerMask.GetMask(new string[] { "Jumpable", "Wall" }));
            if (hit.collider)
            {
                Debug.Log($"Hit! Distance is {hit.distance}");
                distance = hit.distance;
            }
            else
            {
                Debug.LogError("Lasers should be aimed at walls!");
                distance = maxDist;
            }

            start = (Vector2)transform.position;
            Vector2 finalEnd = (Vector2)transform.position + distance * direction;


            LineRenderer renderer = GetComponent<LineRenderer>();

            renderer.widthCurve = AnimationCurve.Constant(0, 100, width);
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            renderer.enabled = true;
            collider.enabled = true; //TODO: Confirm that this doesn't break on the first frame it's enabled - Shouldn't since it will get edited in for loop

            renderer.SetPosition(0, start);

            float timeToFinish = timeToSpan;//distance / timeToSpan;

            Debug.Log($"Time to finish {timeToFinish}");

            for (float t = 0; t < timeToFinish; t += Time.deltaTime)
            {
                end = Vector2.Lerp(start, finalEnd, t / timeToFinish);

                //Draw in the laser
                renderer.SetPosition(1, end);
                collider.size = new Vector2(Mathf.Abs(start.x - end.x), Mathf.Abs(start.y - end.y));
                collider.offset = collider.size / 2;
                collider.offset *= direction;

                if (collider.size.x == 0)
                {
                    collider.size = new Vector2(width, collider.size.y);
                }
                else
                {
                    collider.size = new Vector2(collider.size.x, width);
                }

                yield return null;
            }

            {//Set it to final position
                end = finalEnd;

                //Draw in the laser
                renderer.SetPosition(1, end);
                collider.size = new Vector2(Mathf.Abs(start.x - end.x), Mathf.Abs(start.y - end.y));

                collider.offset = collider.size / 2;
                collider.offset *= direction;

                if (collider.size.x == 0)
                {
                    collider.size = new Vector2(width, collider.size.y);
                }
                else
                {
                    collider.size = new Vector2(collider.size.x, width);
                }
            }

            for (float t = 0; t < holdTime; t += Time.deltaTime)
            {
                yield return null;
            }

            renderer.enabled = false;
            collider.enabled = false;

            for (float t = 0; t < repeatAfter; t += Time.deltaTime)
            {
                yield return null;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(direction.normalized * 2));
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
