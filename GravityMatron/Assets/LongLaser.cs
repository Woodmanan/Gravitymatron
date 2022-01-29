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
    public float timeToSpan;
    public float width;
    public float holdTime;
    public float repeatAfter;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MainRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
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
        while(true)
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

}
