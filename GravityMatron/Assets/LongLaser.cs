using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongLaser : MonoBehaviour
{
    Vector2 start;
    Vector2 end;
    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*IEnumerator ExtendLaser()
    {
        Ray2D ray = new Ray2D()
    }*/

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(direction.normalized * 2));
        
    }

}
