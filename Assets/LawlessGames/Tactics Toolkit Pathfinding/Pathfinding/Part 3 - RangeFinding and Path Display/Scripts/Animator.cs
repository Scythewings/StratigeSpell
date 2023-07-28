using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator : MonoBehaviour
{
    public Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>(); 
    }
    public void AnimPlay(CharacterAnim _animType = CharacterAnim.Idle) // Idle is default animation
    {
        switch (_animType)
        //break is for ending case
        {
            case CharacterAnim.Idle:

                break;
            case CharacterAnim.Walk:
                anim.SetTrigger("Walk");
                break;
            case CharacterAnim.Atk:
                break;
            case CharacterAnim.Dead:
                anim.SetTrigger("Dead");
                break;
        }

    }

    public enum CharacterAnim
    {
        Idle,
        Walk,
        Atk,
        Dead,
    }
}
