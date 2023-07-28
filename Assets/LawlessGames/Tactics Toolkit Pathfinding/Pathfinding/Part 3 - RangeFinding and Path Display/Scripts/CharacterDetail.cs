using UnityEngine;

namespace finished3
{
    public class CharacterDetail : MonoBehaviour
    {
        [SerializeField] float health, maxHealth = 3f;
        public OverlayTile standingOnTile;
        public int numberOfMovement = 3;
        public int attackRange = 2;
        [HideInInspector] public bool isMoving = false;
        public bool attackMode = false;
        [HideInInspector] public bool isFreeze = false;
        [HideInInspector] public bool isDead = false;
        [HideInInspector] public bool isProtected = false;
        public string team;
        public int protectedTime = 2;
        public int skillsCoolDown = 3;
        public int skillscountDown = 0;
        public AnimationController animController;
        public Animator anim;
        public float deathTime = 2f;

        private void Start()
        {
            health = maxHealth;
            anim = GetComponent<Animator>();
        }
        public void takeDamage(float damageAmount)
        {
            health -= damageAmount;
            if (health <= 0)
            {
                gameObject.GetComponent<CharacterDetail>().isDead = true;
                gameObject.layer = 0;
                //Destroy(gameObject);
            }
        }
    }

}
