using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    private GameObject parentEnemy;
    private int damage;
    private float attackRate;
    private bool inMeleeRange;
    private PlayerManager playerManager;
    private float attackCooldown = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Transform parentTransform = this.transform.parent;
        parentEnemy = parentTransform.gameObject;
        Enemy enemyScript = parentEnemy.GetComponent<Enemy>();

        damage = enemyScript.meleeDamage;
        attackRate = enemyScript.meleeAttackRate;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        playerManager = collision.GetComponent<PlayerManager>();
        
        if (playerManager != null && Time.time >= attackCooldown)
        {
            attackCooldown = Time.time + 1f / attackRate;
            MeleeAttack(playerManager);
        }
    }
    
    private void MeleeAttack(PlayerManager target)
    {
        target.TakeDamage(damage);
    }
}
