using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    public Text attributesText;
    private Character character;

    // Start is called before the first frame update
    void Start()
    {
        character = FindObjectOfType<Character>().GetComponent<Character>();

        // Set the initial position of the UI Text element
        RectTransform attributesRectTransform = attributesText.GetComponent<RectTransform>();
        attributesRectTransform.anchorMin = new Vector2(0, 1);
        attributesRectTransform.anchorMax = new Vector2(0, 1);
        attributesRectTransform.pivot = new Vector2(0, 1);
        attributesRectTransform.anchoredPosition = new Vector2(50, -50);
    }

    // Update is called once per frame
    void Update()
    {
        ShowLevelAndHealth();
    }

    private void ShowLevelAndHealth()
    {
        // Update UI Text elements with player's level and health values
        if (character != null)
        {
            attributesText.text = "Level: " + character.level.ToString() + "\nHealth: " + character.health.ToString(); 
        }
        else
        {
            // Handle the case where the player reference is null (player object not found)
            attributesText.text = "Level: N/A Health: N/A";
        }
    }
}
