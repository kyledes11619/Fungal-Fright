using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPickup : MonoBehaviour
{
    void OnCollisionEnter(Collision col) {
        if(col.gameObject.GetComponent<PlayerController>() != null) {
            PlayerController.instance.rocks++;
            PlayerController.instance.rockCounter.text = "" + PlayerController.instance.rocks;
            Destroy(gameObject);
        }
    }
}
