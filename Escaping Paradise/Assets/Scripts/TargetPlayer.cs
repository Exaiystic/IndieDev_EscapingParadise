using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayer : MonoBehaviour
{
    private GameObject player;
    private Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        dir = player.transform.position - transform.position; //Gets difference between player and enemy
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90; //Turns vector into an angle
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); //Rotates enemy based off angle
    }
}
