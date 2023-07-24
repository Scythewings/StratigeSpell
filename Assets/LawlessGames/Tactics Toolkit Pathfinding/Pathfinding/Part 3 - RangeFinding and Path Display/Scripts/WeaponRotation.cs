using finished3;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    [SerializeField] GameObject weapons;
    public CharacterDetail charactermode;
    public GameObject bulletPrefab;
    public float bulletSpeed = 60f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            charactermode.attackMode = !charactermode.attackMode;

        }

        if (charactermode.attackMode && charactermode.isMoving)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            Vector3 difference = mousePos - weapons.transform.position;
            float ratationz = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            weapons.GetComponentInChildren<SpriteRenderer>().transform.rotation = Quaternion.Euler(0.0f, 0.0f, ratationz);
            if (Input.GetKeyDown("s"))
            {
                float distance = difference.magnitude;
                Vector2 direction = difference / distance;
                direction.Normalize();
                FireBullet(direction, ratationz);
                charactermode.attackMode = false;
            }
        }

    }

    void FireBullet(Vector2 direction, float ratationZ)
    {
        GameObject b = Instantiate(bulletPrefab) as GameObject;
        b.transform.position = weapons.transform.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, ratationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}
