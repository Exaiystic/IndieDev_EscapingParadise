using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    //Basics
    public int health = 500;
    public float moveSpeed = 1.5f;
    public float attackRange;

    //Attack
    public float fireRate;
    public float secondAttackRate;
    public float thirdAttackRate;
    public float meleeAttackRate;
    public int meleeDamage;
    public int rangedMinDamage;
    public int rangedMaxDamage;
    public float projectileSpeed;
    public float secondAttackProjectileSpeed;
    public float thirdAttackProjectileSpeed;
    public int attack1Length;
    public int attack2Length;
    public int attack3Length;

    //Technical
    public Transform leftGunFirepoint;
    public Transform leftGunFirepoint2;
    public Transform rightGunFirepoint;
    public Transform rightGunFirepoint2;
    public GameObject attack1BulletPrefab;
    public GameObject attack2BulletPrefab;
    public GameObject attack3BulletPrefab;

    private GameObject player;
    private Rigidbody2D rb;
    private float currentHealth;
    private Vector2 movement;
    private float attackCooldown = 0f;
    private Vector2 playerDistance;
    private Vector3 dir;
    private Vector3 range;
    private int numberOfAttacks = 0;

    // Update is called once per frame
    private void Start()
    {
        player = GameObject.Find("Player");

        rb = this.GetComponent<Rigidbody2D>();
        currentHealth = health;

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

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < attackRange)
        {
            if (numberOfAttacks <= attack1Length)
            {
                attack1();
            }
            else if (numberOfAttacks >= attack1Length + 1 && numberOfAttacks <= attack2Length)
            {
                attack2();
            }
            else if (numberOfAttacks >= attack2Length + 1 && numberOfAttacks <= attack3Length)
            {
                attack3();
            }
            else
            {
                numberOfAttacks = 0;
            }
        }
        else
        {
            approach(movement);
        }
    }

    public void approach(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    public void attack1()
    {        
        if (Time.time >= attackCooldown)
        {
            attackCooldown = Time.time + 1f / fireRate;

            //Fires from the left gun
            GameObject leftBullet = Instantiate(attack1BulletPrefab, leftGunFirepoint.position, leftGunFirepoint.rotation);
            Rigidbody2D leftRB = leftBullet.GetComponent<Rigidbody2D>();
            leftRB.AddForce(leftGunFirepoint.up * projectileSpeed, ForceMode2D.Impulse);
            BossBullet leftBulletScript = leftBullet.GetComponent<BossBullet>();
            leftBulletScript.getBulletDetails(this.gameObject);

            //Fires from the right gun
            GameObject rightBullet = Instantiate(attack1BulletPrefab, rightGunFirepoint.position, rightGunFirepoint.rotation);
            Rigidbody2D rightRB = rightBullet.GetComponent<Rigidbody2D>();
            rightRB.AddForce(rightGunFirepoint.up * projectileSpeed, ForceMode2D.Impulse);
            BossBullet rightBulletScript = rightBullet.GetComponent<BossBullet>();
            rightBulletScript.getBulletDetails(this.gameObject);

            numberOfAttacks++;
        }
    }

    public void attack2()
    {
        if (Time.time >= attackCooldown)
        {
            attackCooldown = Time.time + 1f / secondAttackRate;

            //Fires from the left gun
            GameObject leftBullet = Instantiate(attack2BulletPrefab, leftGunFirepoint2.position, leftGunFirepoint2.rotation);
            Rigidbody2D leftRB = leftBullet.GetComponent<Rigidbody2D>();
            leftRB.AddForce(leftGunFirepoint.up * secondAttackProjectileSpeed, ForceMode2D.Impulse);
            BossBullet leftBulletScript = leftBullet.GetComponent<BossBullet>();
            leftBulletScript.getBulletDetails(this.gameObject);

            //Fires from the right gun
            GameObject rightBullet = Instantiate(attack2BulletPrefab, rightGunFirepoint2.position, rightGunFirepoint2.rotation);
            Rigidbody2D rightRB = rightBullet.GetComponent<Rigidbody2D>();
            rightRB.AddForce(rightGunFirepoint.up * secondAttackProjectileSpeed, ForceMode2D.Impulse);
            BossBullet rightBulletScript = rightBullet.GetComponent<BossBullet>();
            rightBulletScript.getBulletDetails(this.gameObject);

            numberOfAttacks++;
        }
    }

    public void attack3()
    {
        attackCooldown = Time.time + 1f / thirdAttackRate;

        //Fires from the left gun
        GameObject leftBullet = Instantiate(attack3BulletPrefab, leftGunFirepoint.position, leftGunFirepoint.rotation);
        Rigidbody2D leftRB = leftBullet.GetComponent<Rigidbody2D>();
        leftRB.AddForce(leftGunFirepoint.up * thirdAttackProjectileSpeed, ForceMode2D.Impulse);
        BossBullet leftBulletScript = leftBullet.GetComponent<BossBullet>();
        leftBulletScript.getBulletDetails(this.gameObject);

        //Fires from the right gun
        GameObject rightBullet = Instantiate(attack3BulletPrefab, rightGunFirepoint.position, rightGunFirepoint.rotation);
        Rigidbody2D rightRB = rightBullet.GetComponent<Rigidbody2D>();
        rightRB.AddForce(rightGunFirepoint.up * thirdAttackProjectileSpeed, ForceMode2D.Impulse);
        BossBullet rightBulletScript = rightBullet.GetComponent<BossBullet>();
        rightBulletScript.getBulletDetails(this.gameObject);

        numberOfAttacks++;
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
