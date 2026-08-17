using UnityEngine;
using System.Text;

public class SentenceDisplay : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI text;

    [Header("Colors")]
    [SerializeField] private Color typedColor = Color.yellow;
    [SerializeField] private Color remainingColor = Color.white;
    [SerializeField] private Color completedColor = Color.green;
    [Header("Color By State")]
    [SerializeField] private Color pendingColor = new Color(1f, 1f, 1f, 10f / 255f);
    [SerializeField] private Color finishedColor = new Color(0f, 1f, 0f, 10f / 255f);

    private string fullSentence = string.Empty;
    private int typedCount = 0;

    public void SetSentence(string sentence)
    {
        fullSentence = sentence ?? string.Empty;
        text.color = remainingColor;
        typedCount = 0;
        UpdateDisplay();
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
