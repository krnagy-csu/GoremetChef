using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KitchentoMenu : MonoBehaviour
{
    private DeliveryManager deliveryManager;

    public float timer = 10f;  
    public TMP_Text timerText;
    public TMP_Text winText;
    public TMP_Text loseText;
    public int points = 0;
    [SerializeField] private Animator panel;

    private bool timesUp;

    void Start()
    {
        deliveryManager = FindObjectOfType<DeliveryManager>();
    }

    void Update()
    {
        points = deliveryManager.GetSuccessRecipesAmount();
        if (!timesUp)
        {
            timer -= Time.deltaTime;
            UpdateTimerUI();

            if (timer <= 0)
            {
                timesUp = true; // Prevent multiple calls
                timer = 0; // Clamp timer
                timerText.enabled = false;
                if (points >= 5)
                {
                    winText.gameObject.SetActive(true);
                }
                else if (points < 5)
                {
                    loseText.gameObject.SetActive(true);
                }
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
        yield return new WaitForSeconds(5f); // Give 5 seconds to read "Win/Lose result"
        SceneManager.LoadScene("MainMenu");
    }
    
    public void StartFade()
    {
        panel.SetTrigger("FadeOut");
    }
}
