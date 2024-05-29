using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int _enemyHealth = 100;

    public void EnemyTakeDamage(int damage)
    {
        _enemyHealth -= damage;
        if (_enemyHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Логика смерти врага
        Destroy(gameObject);
    }
}
