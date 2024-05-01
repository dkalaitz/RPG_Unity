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
            attributesText.text = "Level: " + character.level.ToString(); 
        }
        else
        {
            // Handle the case where the player reference is null (player object not found)
            attributesText.text = "Level: N/A Health: N/A";
        }
    }
}
