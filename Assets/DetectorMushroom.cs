using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetectorMushroom : MonoBehaviour
{
    void OnCollisionEnter(Collision col) {
        if(col.collider.gameObject.GetComponent<MonsterAI>() != null)
            return;
        Debug.Log("Mushroom Touched");
        MonsterAI.instance.MushroomAlert(transform.position);
    }
}
