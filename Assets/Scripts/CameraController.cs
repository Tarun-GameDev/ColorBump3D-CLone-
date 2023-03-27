using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public float moveSpeed = 10f;
    public bool playerDead = false;
    public bool canmove = false;
    public Vector3 cameraVel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    private void Start()
    {
        canmove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerDead && canmove)
        {
            transform.position += new Vector3(0f, 0f, moveSpeed * Time.deltaTime);
            cameraVel = new Vector3(0f, 0f, moveSpeed * Time.deltaTime);
        }
          
    }
}
