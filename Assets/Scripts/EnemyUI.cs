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
    }

    // Update is called once per frame
    void Update()
    {
        // Update UI Text elements with player's level and health values
        if (enemy.isInAggroRange && !enemy.isDead)
        {
            enemyText.text = "Enemy Level: " + enemy.level.ToString();
        } else
        {
            enemyText.text = "";
        }
    }
}
