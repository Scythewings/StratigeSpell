using finished3;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    [SerializeField] GameObject weapons;
    public CharacterDetail charactermode;
    public List<GameObject> bulletPrefab = new List<GameObject>();
    public float bulletSpeed = 60f;
    public GameObject buttleStartPos;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w"))
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
            if (Input.GetKeyDown("e"))
            {
                float distance = difference.magnitude;
                Vector2 direction = difference / distance;
                direction.Normalize();
                FireBullet(direction, ratationz, bulletPrefab[0]);
                charactermode.attackMode = false;
            }           
            if (Input.GetKeyDown("r") && charactermode.skillscountDown == 0)
            {
                float distance = difference.magnitude;
                Vector2 direction = difference / distance;
                direction.Normalize();
                FireBullet(direction, ratationz, bulletPrefab[1]);
                charactermode.attackMode = false;
                charactermode.skillscountDown = charactermode.skillsCoolDown;
            }

        }

    }

    void FireBullet(Vector2 direction, float ratationZ,GameObject bullet)
    {
        GameObject b = Instantiate(bullet) as GameObject;
        b.transform.position = buttleStartPos.transform.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, ratationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}
