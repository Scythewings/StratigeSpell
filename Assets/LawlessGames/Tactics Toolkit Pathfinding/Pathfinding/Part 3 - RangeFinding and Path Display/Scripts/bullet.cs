using finished3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] int bulletDamage = 1;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Physics2D.IgnoreLayerCollision(0, 7);

        if (collision.gameObject.TryGetComponent<CharacterDetail>(out CharacterDetail enemyComponent))
        {
            enemyComponent.takeDamage(bulletDamage);
        }
        Destroy(gameObject);
    }
}
