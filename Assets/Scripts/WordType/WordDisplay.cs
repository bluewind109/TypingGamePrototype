using UnityEngine;

public class WordDisplay : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI text;
    [SerializeField] private float fallSpeed = 1f;

    public void SetWord(string word)
    {
        text.text = word;
        text.color = Color.white; // Reset color when setting a new word
    }

    public void RemoveLetter()
    {
        text.text = text.text.Remove(0, 1);
        text.color = Color.red; // Current active word
    }

    public void RemoveWord()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void Update()
    {
        transform.Translate(0f, -fallSpeed * Time.deltaTime, 0f);
    }
}