using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats")]
public class Stats : ScriptableObject
{
    [Min(1)] public int health;
    [Min(1)] public int strength;
}
