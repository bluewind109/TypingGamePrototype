using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPoint_UI : MonoBehaviour
{
    [SerializeField] private List<Image> apIcons = new List<Image>();

    public void UpdateUI(int currentAP)
    {
        for (int i = 0; i < apIcons.Count; i++)
        {
            apIcons[i].enabled = i < currentAP;
        }
    }
}
