using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSwitch : MonoBehaviour
{

    public void OnButtonClick()
    {
        SceneManager.LoadScene(sceneName:"Town");
    }
}
