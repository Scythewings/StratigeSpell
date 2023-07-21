using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class SpellControl : MonoBehaviour
{
    [SerializeField] private float _Speed;
    [SerializeField] private float _WeaponRange = 10f;
    [SerializeField] private Transform _Spell;
    [SerializeField] private GameObject _BulletTrail;
    [SerializeField] private Animator _BulletTrailAnimator;
    public PlayerInput input;

    public void Shoot(bool projAtk)
    {
        if (projAtk)
        {
    //   _BulletTrailAnimator.SetTrigger("shoot");
            var hit = Physics2D.Raycast(_Spell.position, transform.up, _WeaponRange);
            var trail = Instantiate(_BulletTrail, _Spell.position, transform.rotation);
            var trailScript = trail.GetComponent<BulletTrail>();
            if (hit.collider != null)
            {
                trailScript.SetTargetPos(hit.point);
                var hittable = hit.collider.GetComponent<Hittable>();
                hittable?.RecieveHit(hit);
               
                    
            }
            else
            {
                var EndPos = _Spell.position + transform.up * _WeaponRange;
                trailScript.SetTargetPos(EndPos);
            }
        }
    }
    
    void Update()
    {       
        LookAtMouse();
    }
    public void LookAtMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.up = (mousePos - new Vector2(transform.position.x, transform.position.y));
    }
   
}
