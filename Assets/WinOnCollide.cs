using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOnCollide : MonoBehaviour
{
    public void OnCollisionEnter(Collision other)
    {
        if(other.collider.GetComponent<PlayerController>() != null) {
            Time.timeScale = 0;
            PlayerController.instance.EndLevel(true);
        }
    }
}
