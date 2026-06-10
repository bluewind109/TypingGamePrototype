using System;
using UnityEngine;

public class Health_UI : MonoBehaviour
{
    [SerializeField] private Entity entity;
    [SerializeField] private TMPro.TextMeshProUGUI healthText;

    void Start()
    {
        if (entity != null && entity.health != null)
        {
            entity.health.onHealthChanged += UpdateHealthText;
            UpdateHealthText(entity.health.currentHealth);
        }
    }

    void OnDestroy()
    {
        if (entity != null && entity.health != null)
        {
            entity.health.onHealthChanged -= UpdateHealthText;
        }
    }

	private void UpdateHealthText(int currentHealth)
	{
		if (healthText == null) return;
		healthText.text = currentHealth.ToString();
	}
}
