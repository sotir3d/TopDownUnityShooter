using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHandler : MonoBehaviour
{
    public void Death()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }
}
