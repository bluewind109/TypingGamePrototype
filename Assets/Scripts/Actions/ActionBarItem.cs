using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

public class ActionBarItem : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image bg;
    [SerializeField] private Image actionIcon;
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color activeColor = Color.green;

    void Awake()
    {
        canvasGroup.alpha = 0f;
    }

    void Start()
    {
        _ = FadeIn();
    }

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

    private async Task FadeIn()
    {
        float duration = 0.5f; // Duration of the fade-in effect
        canvasGroup.alpha = 0f; // Start fully transparent
        await canvasGroup.DOFade(1f, duration).AsyncWaitForCompletion();
    }

    public async Task FadeOutAndDestroy()
    {
        float duration = 0.5f; // Duration of the fade-out effect
        // Fade out the item over the specified duration
        await canvasGroup.DOFade(0f, duration).AsyncWaitForCompletion();
        Destroy(gameObject);
    }
}
