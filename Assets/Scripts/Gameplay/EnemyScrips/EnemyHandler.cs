﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public void Death()
    {
        Destroy(gameObject);
    }
}
