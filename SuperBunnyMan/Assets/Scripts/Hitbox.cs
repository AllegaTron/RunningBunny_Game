using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (gameObject.name == "HITBOX1")
                {
                    enemy.EnemyTakeDamage(25);
                }
                else if (gameObject.name == "HITBOX2")
                {
                    enemy.EnemyTakeDamage(25);
                }
                else if (gameObject.name == "HITBOX3")
                {
                    enemy.EnemyTakeDamage(50);
                }
            }
        }
    }
}
