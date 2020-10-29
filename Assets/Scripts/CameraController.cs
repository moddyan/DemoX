using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private IUserInput pi;
    public float horizontalSpeed = 20.0f;
    public float verticalSpeed = 20.0f;
    public float cameraDampValue = 0.2f;

    private GameObject playerHandle;
    private GameObject cameraHandle;
    private float tempEulerX;
    private GameObject model;
    private Camera camera;
    private Vector3 cameraDampVelocity;

	// Use this for initialization
	void Awake () {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        tempEulerX = 20;
        model = playerHandle.GetComponent<ActorController>().model;

        camera = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        pi = playerHandle.GetComponent<ActorController>().pi;
    }

    // Update is called once per frame
    void FixedUpdate () {
        Vector3 tempModelEuler = model.transform.eulerAngles;

        playerHandle.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime);
        tempEulerX -= pi.Jup * verticalSpeed * Time.fixedDeltaTime;
        tempEulerX = Mathf.Clamp(tempEulerX, -40, 30);
        cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);

        model.transform.eulerAngles = tempModelEuler;
        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, transform.position, ref cameraDampVelocity, cameraDampValue);
        //camera.transform.eulerAngles = transform.eulerAngles;

        camera.transform.LookAt(cameraHandle.transform);
	}
}
