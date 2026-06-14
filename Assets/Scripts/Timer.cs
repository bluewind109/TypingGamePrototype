using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public System.Action onTimerComplete;

    [SerializeField] private Image progressBar;

    private float duration;
    private float timer;
    public bool IsPaused { get; private set; }
    public bool IsRunning => timer > 0;

    private void Update()
    {
        if (IsPaused) return;

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                onTimerComplete?.Invoke();
            }
        }
        UpdateProgressBar();
    }

    public void StartTimer(float newDuration)
    {
        duration = newDuration;
        timer = duration;
        UpdateProgressBar();
    }

    public void StopTimer()
    {
        timer = 0;
        UpdateProgressBar();
    }

    public void PauseTimer()
    {
        IsPaused = true;
    }

    public void ResumeTimer()
    {
        IsPaused = false;
    }

    private void UpdateProgressBar()
    {
        if (progressBar == null) return;
        progressBar.fillAmount = duration > 0 ? timer / duration : 0;
    }

}
