using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortLaser : MonoBehaviour
{
    public SwitchMode activeMode;
    public float delayBetweenLasers;
    public float initialDelay;

    public float speed;
    public float length;
    public float width;

    public float maxDistance;
    public Vector2 direction;

    public GameObject beamPrefab;
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
        for (float t = 0; t < initialDelay; t += Time.deltaTime)
        {
            yield return null;
        }

        while (true)
        {
            float distance;
            RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, direction, maxDistance, LayerMask.GetMask(new string[] { "Jumpable", "Wall" }));
            if (hit.collider)
            {
                Debug.Log($"Hit! Distance is {hit.distance}", this);
                distance = hit.distance;
            }
            else
            {
                Debug.LogError("Lasers should be aimed at walls!", this);
                distance = maxDistance;
            }

            Vector2 start = (Vector2)transform.position;
            //Vector2 finalEnd = (Vector2)transform.position + distance * direction;

            GameObject laserObj = Instantiate(beamPrefab);
            LaserBeam beam = laserObj.GetComponent<LaserBeam>();
            beam.speed = speed;
            beam.length = length;
            beam.width = width;
            beam.activeMode = activeMode;
            beam.Fire(start, direction.normalized, distance);

            for (float t = 0; t < delayBetweenLasers; t += Time.deltaTime)
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
