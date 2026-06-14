using UnityEngine;
using UnityEngine.UI;

public class EnemyActionItem : MonoBehaviour
{
    [SerializeField] private Image bg;
    [SerializeField] private Image actionIcon;
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color activeColor = Color.green;

    public void Initialize(Sprite icon)
    {
        // Set the icon based on the action type
        actionIcon.sprite = icon;
        SetActive(false);
    }

    public void SetActive(bool isActive)
    {
        bg.color = isActive ? activeColor : defaultColor;
    }
}
