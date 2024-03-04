using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public void OnCollisionEnter(Collision other)
    {
        if(other.collider.GetComponent<PlayerController>() != null)
            SceneManager.LoadScene("Basement");
    }
}
