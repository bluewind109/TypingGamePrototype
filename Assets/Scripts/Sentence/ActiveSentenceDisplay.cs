using UnityEngine;
using System.Text;
using TMPro;

public class ActiveSentenceDisplay : MonoBehaviour
{
	[SerializeField] private TMP_InputField _textInput;
	[SerializeField] private Color _textColor = Color.white;

	private string _fullSentence = string.Empty;
	private int _typedCount = 0;

	public void SetSentence(string sentence)
	{
		_fullSentence = sentence ?? string.Empty;

		_typedCount = 0;
		UpdateDisplay();
	}

	private void UpdateDisplay()
	{
		string typedPart = _fullSentence.Substring(0, _typedCount);
		string remainingPart = _fullSentence.Substring(_typedCount);
		string typedPartDisplay = BuildTypedPartDisplay(typedPart, ColorUtility.ToHtmlStringRGB(_textColor));

		_textInput.text = $"<color=#{ColorUtility.ToHtmlStringRGB(_textColor)}>{typedPartDisplay}</color><color=#{ColorUtility.ToHtmlStringRGB(_textColor)}>{remainingPart}</color>";
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
