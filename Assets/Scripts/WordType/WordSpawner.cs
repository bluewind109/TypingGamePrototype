using UnityEngine;

public class WordSpawner : MonoBehaviour
{
    [SerializeField] private GameObject wordPrefab;
    [SerializeField] private Transform wordCanvas;
    [SerializeField] private Vector2 spawnRange = new Vector2(-200f, 200f);

    public WordDisplay SpawnWord()
    {
        Vector3 randomPosition = new Vector3(Random.Range(spawnRange.x, spawnRange.y), 360f);
        WordDisplay wordInstance = Instantiate(wordPrefab, wordCanvas).GetComponent<WordDisplay>();
        wordInstance.transform.localPosition = randomPosition;
        wordInstance.transform.localRotation = Quaternion.identity;
        return wordInstance;
    }
}