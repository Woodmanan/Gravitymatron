using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    public GameObject Bullet;

    private Stack<GameObject> pooledObjects;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

     void Start(){
        pooledObjects = new Stack<GameObject>();
        for (int i = 0; i < 10; i++)
        {
            GameObject newBullet = Instantiate(Bullet);
            newBullet.SetActive(false);
            pooledObjects.Push(newBullet);
        }
    }


    public GameObject GetBullet()
    {
        if (pooledObjects.Count > 0)
        {
            GameObject pooledBullet = pooledObjects.Pop();
            pooledBullet.SetActive(true);
            return pooledBullet;
        }
        else
        {
            return Instantiate(Bullet);
        }
    }

    public void StoreBullet(GameObject newBullet)
    {
        newBullet.SetActive(false);
        pooledObjects.Push(newBullet);
    }
}
