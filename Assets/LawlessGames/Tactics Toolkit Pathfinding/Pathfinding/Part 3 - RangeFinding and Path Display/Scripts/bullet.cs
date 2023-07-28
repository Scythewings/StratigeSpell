using finished3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] int bulletDamage = 1;
    [SerializeField] bool freezeSkill = false;
    [SerializeField] bool isProtected = false;
    [SerializeField] float spellTime = 10f;

    private void Update()
    {
        spellTime -= Time.deltaTime;
        if (spellTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Physics2D.IgnoreLayerCollision(0, 7);

        if (collision.gameObject.TryGetComponent<CharacterDetail>(out CharacterDetail enemyComponent))
        {
            if (freezeSkill)
            {
                enemyComponent.isFreeze = true;
            }
            else if (isProtected)
            {
                enemyComponent.isProtected = true;
            }
            else
            {
                if(!enemyComponent.isProtected) enemyComponent.takeDamage(bulletDamage);
            }
        }
        Destroy(gameObject);
    }
}
