using UnityEngine;
using System.Text;

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
        string typedPartDisplay = BuildTypedPartDisplay(typedPart, ColorUtility.ToHtmlStringRGB(typedColor));

        text.text = $"<color=#{ColorUtility.ToHtmlStringRGB(typedColor)}>{typedPartDisplay}</color><color=#{ColorUtility.ToHtmlStringRGB(remainingColor)}>{remainingPart}</color>";
        if (typedCount >= fullSentence.Length)
        {
            text.text = $"<color=#{ColorUtility.ToHtmlStringRGB(completedColor)}>{fullSentence}</color>";
        }
    }

    private static string BuildTypedPartDisplay(string typedPart, string typedSpaceCueColorHtml)
    {
        StringBuilder builder = new StringBuilder(typedPart.Length * 12);

        foreach (char character in typedPart)
        {
            if (character == ' ')
            {
                // Replace typed spaces with a strong marker so players can clearly confirm gap input.
                builder.Append($"<color=#{typedSpaceCueColorHtml}><b>_</b></color>");
            }
            else
            {
                builder.Append(character);
            }
        }

        return builder.ToString();
    }
}
