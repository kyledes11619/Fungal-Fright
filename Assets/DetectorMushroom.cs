using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetectorMushroom : MonoBehaviour
{
    public GameObject particles;

    void OnCollisionEnter(Collision col) {
        if(col.collider.gameObject.GetComponent<MonsterAI>() != null)
            return;
        Destroy(Instantiate(particles, transform), 1);
        MonsterAI.instance.MushroomAlert(transform.position);
    }
}
