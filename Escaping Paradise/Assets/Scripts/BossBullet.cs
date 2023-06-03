using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public int damageMultiplier = 1;
    
    private GameObject go;
    private int minDamage;
    private int maxDamage;

    public void getBulletDetails(GameObject source)
    {
        go = source;

        minDamage = go.GetComponent<Boss>().rangedMinDamage;
        maxDamage = go.GetComponent<Boss>().rangedMaxDamage;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int damage = Random.Range(minDamage, maxDamage);

        PlayerManager player = collision.GetComponent<PlayerManager>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
