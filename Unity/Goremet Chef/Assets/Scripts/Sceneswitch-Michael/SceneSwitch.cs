using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{

    public float timer = 10f;  
    public TMP_Text timerText;
    public TMP_Text timeUpText;
    [SerializeField] private Animator panel;

    private bool timesUp;

    
    void Update()
    {
        if (!timesUp)
        {
            timer -= Time.deltaTime;
            UpdateTimerUI();

            if (timer <= 0)
            {
                timesUp = true; // Prevent multiple calls
                timer = 0; // Clamp timer
                timerText.enabled = false;
                timeUpText.gameObject.SetActive(true);

                Invoke(nameof(LoadSceneWithDelay), 0.01f);
            }
        }
    }

    //Converts timer to be minutes and seconds left.
    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = $"{minutes:D2}:{seconds:D2}";
    }
    
    public void LoadSceneWithDelay()
    {
        StartFade();
        StartCoroutine(DelayThenLoad());
    }

    private IEnumerator DelayThenLoad()
    {
        yield return new WaitForSeconds(3f); // Give 3 seconds to read "Time Up"
        SceneManager.LoadScene("Kitchen");
    }
    
    public void StartFade()
    {
        panel.SetTrigger("FadeOut");
    }
}
