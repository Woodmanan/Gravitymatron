using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public bool DDOL = true;
    public int firstSceneIndex;
    public void MoveToScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadStart()
    {
        MoveToScene(firstSceneIndex);
    }

    public void MoveToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Reset()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("GameController");
        Destroy(obj);
        SceneManager.LoadScene(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (DDOL)
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
