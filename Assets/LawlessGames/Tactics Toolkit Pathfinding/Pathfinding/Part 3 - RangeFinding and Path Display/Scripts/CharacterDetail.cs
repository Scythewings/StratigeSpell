using UnityEngine;

namespace finished3
{
    public class CharacterDetail : MonoBehaviour
    {
        [SerializeField] float health, maxHealth = 3f;
        public OverlayTile standingOnTile;
        public int numberOfMovement = 3;
        public int attackRange = 2;
        public bool isMoving = false;
        public bool attackMode = false;
        public bool isFreeze = false;
        public bool isDead = false;
        public bool isProtected = false;

        private void Start()
        {
            health = maxHealth;
        }
        public void takeDamage(float damageAmount)
        {
            health -= damageAmount;
            if (health <= 0)
            {
                gameObject.GetComponent<CharacterDetail>().isDead = true;
                Destroy(gameObject);
            }
        }
    }

}
