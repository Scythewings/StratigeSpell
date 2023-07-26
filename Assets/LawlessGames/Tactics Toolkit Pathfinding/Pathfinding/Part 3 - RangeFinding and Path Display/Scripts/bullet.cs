using finished3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] int bulletDamage = 1;
    [SerializeField] bool Skill = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Physics2D.IgnoreLayerCollision(0, 7);

        if (collision.gameObject.TryGetComponent<CharacterDetail>(out CharacterDetail enemyComponent))
        {
            if (Skill)
            {
                enemyComponent.isFreeze = true;
            }
            else
            {
                enemyComponent.takeDamage(bulletDamage);
            }
        }
        Destroy(gameObject);
    }
}
