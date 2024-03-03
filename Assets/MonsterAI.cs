using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public static MonsterAI instance;

    void Awake() {
        instance = this;
    }

    NavMeshAgent nav;
    public float wanderSpeed, investigateSpeed, huntSpeed, checkingDistanceBeforeChange, mushroomSenseDistance, playerSenseDistance, loseHuntDist, loseHuntTime, killRange;
    public int currentState;
    public Transform[] wanderPoints;
    float timeSincePlayerLastSeen;

    void Start() {
        nav = gameObject.GetComponent<NavMeshAgent>();
        nav.destination = wanderPoints[Random.Range(0, wanderPoints.Length)].position;
    }

    void Update() {
        //If wandering...
        if(currentState == 0) {
            //and it runs into a player, hunt the player
            if(Vector3.Distance(PlayerController.instance.transform.position, transform.position) < playerSenseDistance * (SettingsManager.hardMode ? 1.5 : 1)) {
                Debug.Log("Player spotted during wander, hunting...");
                currentState = 2;
                nav.speed = (SettingsManager.hardMode ? PlayerController.instance.runSpeed : huntSpeed);
                nav.destination = PlayerController.instance.transform.position;
                timeSincePlayerLastSeen = 0;
            }
            //and it has reached it's destination, change it to a new random wander point
            if(Vector3.Distance(nav.destination, transform.position) < checkingDistanceBeforeChange) {
                nav.speed = wanderSpeed;
                nav.destination = wanderPoints[Random.Range(0, wanderPoints.Length)].position;
            }
        }
        //If investigating...
        else if(currentState == 1) {
            //and it runs into a player, hunt the player
            if(Vector3.Distance(PlayerController.instance.transform.position, transform.position) < playerSenseDistance * (SettingsManager.hardMode ? 1.5 : 1)) {
                Debug.Log("Player spotted during investigation, hunting...");
                currentState = 2;
                nav.speed = (SettingsManager.hardMode ? PlayerController.instance.runSpeed : huntSpeed);
                nav.destination = PlayerController.instance.transform.position;
                timeSincePlayerLastSeen = 0;
            }
            //and it has reached it's destination without starting a hunt, go back to wandering
            if(Vector3.Distance(nav.destination, transform.position) < checkingDistanceBeforeChange) {
                Debug.Log("Nothing found in investigation, wandering...");
                currentState = 0;
                nav.speed = wanderSpeed;
                nav.destination = wanderPoints[Random.Range(0, wanderPoints.Length)].position;
            }
        }
        //If hunting...
        else if(currentState == 2) {
            //Track player
            nav.destination = PlayerController.instance.transform.position;
            //and if the player is in killing range...
            if(Vector3.Distance(nav.destination, transform.position) > killRange) {
                Time.timeScale = 0;
                PlayerController.instance.EndLevel(false);
            }
            //and the player is out of range...
            else if(Vector3.Distance(nav.destination, transform.position) > loseHuntDist) {
                timeSincePlayerLastSeen += Time.deltaTime;
                //and it has been too long since it last had the player in its range, return to wandering, but at a faster investigate speed
                if(timeSincePlayerLastSeen > loseHuntTime * (SettingsManager.hardMode ? 1.5 : 1)) {
                    Debug.Log("Lost the player, wandering...");
                    currentState = 0;
                    nav.speed = investigateSpeed;
                    nav.destination = wanderPoints[Random.Range(0, wanderPoints.Length)].position;
                }
            }
        }
    }

    public void MushroomAlert(Vector3 t) {
        //If it is hunting, ignore alerts, its chasing the player right now
        if(currentState == 2)
            return;
        //If it was already investigating, speed up
        else if(currentState == 1)
            nav.speed = huntSpeed;
        //If it wasn't investigating, go to investigate speed
        else
            nav.speed = investigateSpeed;
        //Then investigate that location
        nav.destination = t;
        currentState = 1;
        Debug.Log("Movement sensed by mushrooms, investigating...");
    }
}