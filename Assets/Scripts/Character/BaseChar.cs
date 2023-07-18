using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseChar : MonoBehaviour
{
    [SerializeField] private int _Health;
    [SerializeField] private int _Damage;
    [SerializeField] private int _Action;
    public int ActionPoint => _Action;

}
