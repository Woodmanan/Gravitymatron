using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongLaser : MonoBehaviour
{
    Vector2 start;
    Vector2 end;
    public Vector2 direction;
    public float maxDist;
    public float speed;
    public float width;
    public float holdTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExtendLaser());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ExtendLaser()
    {
        if (speed == 0)
        {
            Debug.LogError("Speed must be positive!", this);
            yield break;
        }
        float distance;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, direction, maxDist, LayerMask.GetMask(new string[]{ "Jumpable", "Wall" }));
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

        float timeToFinish = distance / speed;

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

        yield return new WaitForSeconds(holdTime);

        renderer.enabled = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(direction.normalized * 2));
    }

}
