using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Basics
    public int health = 100;
    public float moveSpeed = 2.5f;
    public float attackRange;
    public bool meleeOnly;

    //Attack
    public float fireRate;
    public float meleeAttackRate;
    public int meleeDamage;
    public int rangedMinDamage;
    public int rangedMaxDamage;
    public float projectileSpeed;

    //Technical
    public GameObject gun;
    public Transform firePoint;
    public GameObject bulletPrefab;

    private GameObject player;
    private Rigidbody2D rb;
    private float currentHealth;
    private Vector2 movement;
    private PlayerManager playerManager;
    private float attackCooldown = 0f;
    private Vector2 playerDistance;
    private Vector3 dir;
    private Vector3 range;

    // Update is called once per frame
    private void Start()
    {
        player = GameObject.Find("Player");
        
        rb = this.GetComponent<Rigidbody2D>();
        currentHealth = health;

        if (meleeOnly == true)
        {
            gun.SetActive(false);
        }

        range.y = attackRange;
        range.x = attackRange;
        range.z = 1;
    }

    void Update()
    {
        dir = player.transform.position - transform.position; //Gets difference between player and enemy
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90; //Turns vector into an angle
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); //Rotates enemy based off angle
        dir.Normalize(); //Normalises vector
        movement = dir; //Uses normalised vector to move object

        float distanceToPlayer = Vector3.Distance (transform.position, player.transform.position);
        if (meleeOnly)
        {
            approach(movement);
        }
        else
        {
            if (distanceToPlayer < attackRange)
            {
                attack();
            }
            else
            {
                approach(movement);
            }
        }
    }

    public void approach(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    public void attack()
    {
        if (Time.time >= attackCooldown)
        {
            attackCooldown = Time.time + 1f / fireRate;

            //Create the bullet
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
            bulletScript.getBulletDetails(this.gameObject);

            //Applying a force to the bullet
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);
        }
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
