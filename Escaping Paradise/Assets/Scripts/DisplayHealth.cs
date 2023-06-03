using UnityEngine;
using UnityEngine.UI;

public class DisplayHealth : MonoBehaviour
{

    public Text healthUI;

    private GameObject player;
    private int playerHealth;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        playerHealth = player.GetComponent<PlayerManager>().currentHealth;

        healthUI.text = playerHealth.ToString();
    }
}
