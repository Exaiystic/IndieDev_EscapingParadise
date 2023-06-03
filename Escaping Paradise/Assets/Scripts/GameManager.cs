using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int enemiesLeft;
    public float restartDelay;

    private bool gameEnded = false;
    private GameObject[] enemies;

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesLeft = enemies.Length;

        if (enemiesLeft == 0)
        {
            gameWin();
        }
    }

    public void gameWin()
    {
        if (gameEnded == false)
        {
            gameEnded = true;
            Debug.Log("You Win");
            StartCoroutine(Restart());
        }
    }

    public void endGame()
    {
        if (gameEnded == false)
        {
            gameEnded = true;
            Debug.Log("You Died");
            StartCoroutine(Restart());
        }
    }

    protected virtual IEnumerator Restart()
    {
        yield return new WaitForSeconds(restartDelay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
