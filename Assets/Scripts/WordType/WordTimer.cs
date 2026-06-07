using UnityEngine;

public class WordTimer : MonoBehaviour
{
    public System.Action onWordTimeout;

    [SerializeField] private float wordDuration = 10.0f;
    [SerializeField] private TMPro.TextMeshProUGUI timerText;

    private float wordTimer = 0f;

    public void StartTimer()
    {
        wordTimer = Time.time + wordDuration;
    }

    void Update()
    {
        if (wordTimer == 0f) return; // Timer not started
        if (Time.time >= wordTimer) return; // Already timed out

        if (Time.time >= wordTimer)
        {
            onWordTimeout?.Invoke();
        }
        else
        {
            float remainingTime = wordTimer - Time.time;
            if (timerText != null) 
                timerText.text = remainingTime.ToString("F1");
        }
    }
}
