using UnityEngine;

public class Health_UI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI healthText;

	public void UpdateHealthText(int currentHealth)
	{
		if (healthText == null) return;
		healthText.text = currentHealth.ToString();
	}
}
