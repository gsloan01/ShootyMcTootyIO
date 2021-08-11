using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    public Player.WeaponInfo weaponInfo;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit " + other.name);
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().weapon = weaponInfo;
            Destroy(gameObject);
        }
    }
}
