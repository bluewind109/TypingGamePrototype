using UnityEngine;
using UnityEngine.UI;

public class ActionItemUI : MonoBehaviour
{
    [SerializeField] private ActionButtonType buttonType;

    [Header("UI Elements")]
    [SerializeField] private Image activeBg;
    [SerializeField] private TMPro.TextMeshProUGUI actionText;

    [Header("Font Sizes")]
    [SerializeField] private float normalFontSize = 36f;
    [SerializeField] private float activeFontSize = 48f;

    public bool IsActive { get; private set; }
    public ActionButtonType ButtonType => buttonType;

    public void ToggleActive(bool active)
    {
        IsActive = active;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        activeBg.gameObject.SetActive(IsActive);
        actionText.color = IsActive ? Color.black : Color.white;
        actionText.fontStyle = IsActive ? TMPro.FontStyles.Bold : TMPro.FontStyles.Normal;
        actionText.fontSize = IsActive ? activeFontSize : normalFontSize;
    }
}
