using UnityEngine;

public class SentenceDisplay : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI text;

    [SerializeField] private Color typedColor = Color.yellow;
    [SerializeField] private Color remainingColor = Color.white;
    [SerializeField] private Color completedColor = Color.green;

    private string fullSentence = string.Empty;
    private int typedCount = 0;

    public void SetSentence(string sentence)
    {
        fullSentence = sentence ?? string.Empty;
        typedCount = 0;
        UpdateDisplay();
    }

    public void RemoveLetter()
    {
        if (typedCount >= fullSentence.Length)
        {
            return;
        }

        typedCount++;
        UpdateDisplay();
    }

    public void RemoveSentence()
    {
        // gameObject.SetActive(false);
        // Destroy(gameObject);
    }

    private void UpdateDisplay()
    {
        string typedPart = fullSentence.Substring(0, typedCount);
        string remainingPart = fullSentence.Substring(typedCount);

        text.text = $"<color=#{ColorUtility.ToHtmlStringRGB(typedColor)}>{typedPart}</color><color=#{ColorUtility.ToHtmlStringRGB(remainingColor)}>{remainingPart}</color>";
        if (typedCount >= fullSentence.Length)
        {
            text.text = $"<color=#{ColorUtility.ToHtmlStringRGB(completedColor)}>{fullSentence}</color>";
        }
    }
}
