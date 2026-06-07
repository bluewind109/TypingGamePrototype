using UnityEngine;

public class SentenceDisplay : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI text;

    public void SetSentence(string sentence)
    {
        text.text = sentence;
        text.color = Color.white; // Reset color when setting a new sentence
    }

    public void RemoveLetter()
    {
        text.text = text.text.Remove(0, 1);
        text.color = Color.red; // Current active sentence
    }

    public void RemoveSentence()
    {
        // gameObject.SetActive(false);
        // Destroy(gameObject);
    }

    void Update()
    {
        // transform.Translate(0f, -fallSpeed * Time.deltaTime, 0f);
    }
}
