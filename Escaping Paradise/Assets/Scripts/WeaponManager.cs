using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponManager : MonoBehaviour
{
    public bool automatic;
    public float fireRate;
    public int minimumDamage;
    public int maximumDamage;
    public int ammoCapacity;
    public float reloadSpeed;
    public float aimDeviation;
    public bool hitscan;
    public float projectileSpeed;
    public int currentAmmo;

    public Transform firePoint;
    public GameObject bulletPrefab;

    private bool canFire;
    private bool isReloading;
    private float fireCooldown = 0f;
    
    void Start()
    {
        currentAmmo = ammoCapacity;
    }

    void Update()
    {
         if (currentAmmo == 0)
         {
            canFire = false;
         }
         else
         {
            canFire = true;
         }

         if (Input.GetButton("Reload") && currentAmmo < ammoCapacity)
        {
            StartCoroutine(Reload());
        }

        //If the weapon is automatic...
        if (automatic)
        {
            //and if "Fire1" is being held AND the fire cooldown has expired...
            if (Input.GetButton("Fire1") && Time.time >= fireCooldown)
            {
                //reset the cooldown and execute the fire func
                fireCooldown = Time.time + 1f / fireRate;
                Fire();
            }
        //If the weapon is otherwise semiautomatic...
        } else
        {
            //And if "Fire1" has been pressed AND the fire cooldown has expired...
            if (Input.GetButtonDown("Fire1") && Time.time >= fireCooldown)
            {
                //reset the cooldown and execute the fire func
                fireCooldown = Time.time + 1f / fireRate;
                Fire();
            }
        }
        
    }

    protected virtual void Fire()
    {
        if (canFire)
        {
            //Stating that the weapon is not ready to fire
            canFire = false;

            //Create the bullet
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            currentAmmo--;

            if (currentAmmo == 0)
            {
                StartCoroutine(Reload());
            }

            //Applying a force to the bullet
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);

            //Stating that the firing sequence is complete and that the weapon can fire again
            canFire = true;
        }
    }

    protected virtual IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadSpeed);

        currentAmmo = ammoCapacity;
        isReloading = false;
    }
}
