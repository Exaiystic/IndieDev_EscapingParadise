using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCurrentAmmo : MonoBehaviour
{
    public GameObject weaponFirepoint;
    public Text currentAmmoUI;

    private int playerAmmo;

    // Update is called once per frame
    void Update()
    {
        playerAmmo = weaponFirepoint.GetComponent<WeaponManager>().currentAmmo;

        currentAmmoUI.text = playerAmmo.ToString();
    }
}
