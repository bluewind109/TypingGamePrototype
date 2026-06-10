using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPoint_UI : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private List<Image> apIcons = new List<Image>();

    private void Start()
    {
        if (player != null)
        {
            player.onActionPointsChanged += UpdateAPDisplay;
            UpdateAPDisplay(player.ActionPoints); // Initialize display
        }
    }

    private void UpdateAPDisplay(int currentAP)
    {
        for (int i = 0; i < apIcons.Count; i++)
        {
            apIcons[i].enabled = i < currentAP;
        }
    }
}
