using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEnemyCount : MonoBehaviour
{
    public Text enemyCountUI;
    public GameObject gameManager;

    private GameManager manager;
    private int enemiesRemaining;

    private void Start()
    {
        manager = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        enemiesRemaining = manager.enemiesLeft;
        enemyCountUI.text = enemiesRemaining.ToString();
    }
}
