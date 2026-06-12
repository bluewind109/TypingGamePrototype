using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusEffect : MonoBehaviour
{
    public System.Action<StatusEffectType> onStatusEffectTimedOut;

    [SerializeField] private Image progressBar;
    [SerializeField] private TextMeshProUGUI stackText;

    private StatusEffectType effectType;
    private int currentStacks;
    private int maxStacks;
    private float timer = 0f;
    private float totalDuration = 0f;

    public StatusEffectType EffectType => effectType;

    public void Initialize(StatusEffectType type, int stacks, float duration)
    {
        effectType = type;
        maxStacks = StatusEffectManager.Instance.GetStatusEffectInfo(type)?.maxStacks ?? 1;
        UpdateStack(stacks); // Initialize stack text
        timer = duration;
        totalDuration = duration;
        this.gameObject.SetActive(true);
    }

    public void UpdateStack(int stackDelta)
    {
        currentStacks = Mathf.Min(currentStacks + stackDelta, maxStacks);
        stackText.text = currentStacks > 1 ? currentStacks.ToString() : string.Empty;

        if (currentStacks <= 0)
        {
            this.gameObject.SetActive(false);
            return;
        }

        timer = totalDuration;
    }

    public void UpdateDuration(float deltaTime)
    {
        if (currentStacks <= 0) return;

        if (timer > 0f)
        {
            timer -= deltaTime;
            progressBar.fillAmount = timer / totalDuration;
            stackText.text = currentStacks > 1 ? currentStacks.ToString() : string.Empty;
            if (timer <= 0f)
            {
                UpdateStack(-1); // Reduce stack by 1 when duration expires
                onStatusEffectTimedOut?.Invoke(effectType);
            }
        }
    }
}
