using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    public Text enemyText;
    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = FindObjectOfType<Enemy>().GetComponent<Enemy>();
        enemyText.text = "";

        // Set the initial position of the UI Text element
        RectTransform attributesRectTransform = enemyText.GetComponent<RectTransform>();
        attributesRectTransform.anchorMin = new Vector2(1, 1);
        attributesRectTransform.anchorMax = new Vector2(1, 1);
        attributesRectTransform.pivot = new Vector2(1, 1);
        attributesRectTransform.anchoredPosition = new Vector2(-50, -50);
    }

    // Update is called once per frame
    void Update()
    {
        // Update UI Text elements with player's level and health values
        if (enemy.isInAggroRange && !enemy.isDead)
        {
            enemyText.text = "Enemy Level: " + enemy.level.ToString() + "\nEnemy Health: " + enemy.health.ToString();
        } else
        {
            enemyText.text = "";
        }
    }
}
