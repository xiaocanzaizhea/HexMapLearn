using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings
{
    public static int IsMoving = Animator.StringToHash("IsMoving");
    public static int IsDead = Animator.StringToHash("IsDead");
    
    public const float rotationSpeed = 180f;
    public const float travelSpeed = 4f;
}
