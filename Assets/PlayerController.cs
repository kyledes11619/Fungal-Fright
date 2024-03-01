using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    void Awake() {
        instance = this;
    }

    public float sensitivity = 100;
    public float speed = 10;
    public float runSpeed = 15;
    public Transform mainCamera;
    private float xRot = 0f;
    private Rigidbody rb;
    public int rocks = 3;
    public Text rockCounter;
    public GameObject throwableRock;
    public Transform rockThrowPoint;
    public float rockThrowForce;

    void Start() {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        xRot = Mathf.Clamp(xRot - Input.GetAxis("Mouse Y") * sensitivity * 100 * Time.deltaTime, -90, 90);
        mainCamera.localRotation = Quaternion.Euler(xRot, 0, 0);
        transform.Rotate(100 * Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime * Vector3.up);
        rb.velocity = (Input.GetButton("Fire3") ? runSpeed : speed) * (transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical"));
        if(Input.GetButtonDown("Jump") && rocks > 0) {
            GameObject rock = Instantiate(throwableRock, rockThrowPoint.position, rockThrowPoint.localRotation);
            rock.GetComponent<Rigidbody>().AddForce(rockThrowPoint.transform.forward * rockThrowForce);
            rocks--;
            rockCounter.text = "" + rocks;
        }
    }
}
