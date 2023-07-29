using UnityEngine;

namespace finished3
{
    public class CharacterDetail : MonoBehaviour
    {
        private CamController camcon;
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
        [HideInInspector] public int protectedTime = 2;
        public int skillsCoolDown = 3;
        [HideInInspector] public int skillscountDown = 0;

        [SerializeField] HealthBar healthBar;


        private void Start()
        {
            health = maxHealth;
            foreach (var go in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                if (go.GetComponent<CamController>())
                {
                    camcon = go.GetComponent<CamController>();
                }
            }
        }
        public void takeDamage(float damageAmount)
        {
            health -= damageAmount;
            healthBar.UpdateHealthBars(health, maxHealth);
            camcon.cameraShake(2, 1);

            if (health <= 0)
            {
                gameObject.GetComponent<CharacterDetail>().isDead = true;
                gameObject.layer = 0;
                Destroy(gameObject,2f);
            }
        }
    }

}
