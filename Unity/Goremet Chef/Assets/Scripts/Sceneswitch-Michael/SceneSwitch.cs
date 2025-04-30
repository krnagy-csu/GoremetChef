using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{

    public float timer = 10f;  


    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0) 
        {
            SceneManager.LoadScene(sceneName:"Kitchen");
        }
    }
}
