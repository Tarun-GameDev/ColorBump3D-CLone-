using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    Rigidbody rb;
    Collider col;
    Touch touch;
    Vector3 lastMousePos;
    public float sensitivity = 10f;
    public float clampDelta = 42f;
    public float edge = 5f;

    public string enemyName = "Enemy";

    public bool startTheGame;
    public bool playerDead = false,levelCompleted = false;
    bool touchInput = false;
    [SerializeField] GameObject playerFractured;
    [SerializeField] GameObject ChangedplayerFractured;

    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] TrailRenderer trail;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] float raycastDist = .1f;
    [SerializeField] bool isGrounded = false;
    [SerializeField] bool playerTurnToEnemy = false;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
        if (col == null)
            col = GetComponent<Collider>();

        playerDead = false;
        levelCompleted = false;
    }


    void Update()
    {
        if (playerDead && !startTheGame && !levelCompleted)
            return;


        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -edge, edge), transform.position.y, transform.position.z);
        if(!touchInput)
            transform.position += FindObjectOfType<CameraController>().cameraVel;


    }

    private void FixedUpdate()
    {

        if (playerDead && !startTheGame && !levelCompleted)
            return;

        #region Mobile Input
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePos = Input.mousePosition;
            
        }


        if (Input.GetMouseButton(0))
        {
            Vector3 newPos = lastMousePos - Input.mousePosition;
            lastMousePos = Input.mousePosition;
            newPos = new Vector3(newPos.x, 0, newPos.y);
            Vector3 moveForce = Vector3.ClampMagnitude(newPos, clampDelta);
            rb.AddForce(-moveForce * Time.fixedDeltaTime * sensitivity - rb.velocity / 5f, ForceMode.VelocityChange);
            touchInput = true;
        }
        else
            touchInput = false;
       
        rb.velocity.Normalize();
        
        #endregion
    }

    public void ChangeEnemy(Material _mat)
    {
        enemyName = "Enemy1";
        meshRenderer.material = _mat;
        playerTurnToEnemy = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(enemyName) && !levelCompleted && !playerDead && startTheGame)
        {
            Dead();
        }
    }

    void Dead()
    {
        playerDead = true;
        StartCoroutine(CameraShake.instance.Shake(.2f, .5f));
        meshRenderer.enabled = false;
        trail.enabled = false;
        rb.isKinematic= true;
        col.enabled = false;
        rb.velocity = Vector3.zero;
        if(playerTurnToEnemy)
            Instantiate(ChangedplayerFractured, transform.position, Quaternion.identity);
        else
            Instantiate(playerFractured, transform.position, Quaternion.identity);
        CameraController.instance.playerDead = true;
        StartCoroutine(DeadEnum());
        //enable menu
    }

    IEnumerator DeadEnum()
    {
        yield return new WaitForSeconds(2f);
        UIManager.instance.DeadMenu();
    }
}
