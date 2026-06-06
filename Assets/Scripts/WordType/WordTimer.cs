using UnityEngine;

public class WordTimer : MonoBehaviour
{
    public System.Action onWordTimeout;

    [SerializeField] private float wordDelay = 1.5f;

    private float nextWordTime = 0f;

    void Update()
    {
        if (Time.time >= nextWordTime)
        {
            onWordTimeout?.Invoke();
            nextWordTime = Time.time + wordDelay;
            wordDelay *= 0.99f; // Decrease delay for next word
        }
    }
}
