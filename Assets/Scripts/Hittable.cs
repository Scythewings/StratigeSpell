using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Hittable 
{
  public void RecieveHit(RaycastHit2D Hit);
}
