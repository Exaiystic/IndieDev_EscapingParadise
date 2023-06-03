using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject go;
    private int minDamage;
    private int maxDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        go = GameObject.Find("Firepoint");

        minDamage = go.GetComponent<WeaponManager>().minimumDamage;
        maxDamage = go.GetComponent<WeaponManager>().maximumDamage;

        int damage = Random.Range(minDamage, maxDamage);

        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Boss boss = collision.GetComponent<Boss>();
        if (boss != null)
        {
            boss.TakeDamage(damage);
        }

        Destroy(gameObject);

    }
}
