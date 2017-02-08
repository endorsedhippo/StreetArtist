using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public int playerNumber = 1;
    [Space(5)]
    public float walkSpeed;
    public float jumpForce;
    [Space(5)]
    public Material platformColor;
    public Vector2 platformPosition;
    public GameObject playerIcon;
    public float airControl;

    [Range(-1, 1)]
    public int direction;
    Rigidbody2D rb;
    Animator animator;
    bool grounded;
    BoxCollider2D col;
    float xScale;
    bool canCreatePlatform = true;
    float xMovement;

    PlayerIcon icon;

	// Use this for initialization
	void Awake ()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        xScale = Mathf.Abs(transform.localScale.x);
        icon = playerIcon.GetComponent<PlayerIcon>();
        icon.SetPlayerNumber(playerNumber);

        if (direction == 0)
            direction = 1;
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (!Pauser.paused)
        {
            CheckGround();

            JumpandMovement();

            Platforms();
        }
        else
        {
            IconInputs();
        }

        //Animation paramaters
        animator.SetFloat("Speed", Mathf.Abs(xMovement));
        animator.SetBool("Grounded", grounded);
        animator.speed = Mathf.Abs(xMovement);
        Vector3 ls = transform.localScale;
        transform.localScale = new Vector3(direction * xScale, ls.y, ls.z);

    }

    void JumpandMovement()
    {
        //Jumping
        float jump;
        if (grounded && Input.GetButtonDown("P" + playerNumber + "_Jump"))
        {
            jump = jumpForce;
            animator.SetTrigger("Jump");
        }
        else jump = rb.velocity.y;

        //Movement
        xMovement = Input.GetAxis("P" + playerNumber + "_Horizontal");

        if (grounded)
        {
            if (xMovement > 0)
                direction = 1;
            else if (xMovement < 0)
                direction = -1;
            rb.velocity = new Vector2(xMovement * walkSpeed, jump);
        }
        else //floaty jumps
        {
            float vx = rb.velocity.x;
            rb.velocity = new Vector2(vx, jump);
            if (Mathf.Abs(vx) > walkSpeed) //Don't go over maxSpeed
            {
                if (vx < 0)
                    rb.velocity = new Vector2(-walkSpeed, jump);
                if (vx > 0)
                    rb.velocity = new Vector2(walkSpeed, jump);
            }
            rb.AddForce(new Vector2((xMovement * walkSpeed) * airControl, 0));
        }
    }

    void CheckGround()
    {
        //Grounded test
        grounded = false;
        RaycastHit2D[] allHits;
        //allHits = Physics2D.RaycastAll(transform.position, new Vector3(0, -1, 0), col.bounds.extents.y * 1.5f); //create an array of all things hit
        allHits = Physics2D.BoxCastAll(transform.position, new Vector3(col.bounds.extents.x * 1.5f, col.bounds.extents.y, col.bounds.extents.z), 0, new Vector3(0, -1, 0), col.bounds.extents.y * 1.5f);
        Debug.Log("Objects under player: " + allHits.Length);
        foreach (var hit in allHits)
        {
            if (hit.transform.position != transform.position &&
                hit.transform.GetComponent<Rigidbody2D>() != null) //if collided with something other than self
            {
                grounded = true;
                break;
            }
        }
    }

    void Platforms()
    {
        //Create platform
        if (canCreatePlatform && (Input.GetAxis("P" + playerNumber + "_DrawLeft") > 0.9f || Input.GetAxis("P" + playerNumber + "_DrawRight") > 0.9f))
        {
            GameObject plat = Instantiate(Resources.Load("Platform"), transform.position +
                    new Vector3(platformPosition.x * direction, platformPosition.y, 0), new Quaternion(0, 0, 0, 0)) as GameObject;
            plat.GetComponent<Renderer>().material = platformColor;
            canCreatePlatform = false;
        }
        if (Input.GetAxis("P" + playerNumber + "_DrawLeft") <= 0.2f && Input.GetAxis("P" + playerNumber + "_DrawRight") <= 0.2f)
            canCreatePlatform = true;
    }

    void IconInputs()
    {
        if (Input.GetAxis("P" + playerNumber + "_DrawRight") > 0.9f
            && Input.GetKey("joystick " + playerNumber + " button 0"))
        {
            icon.TakePicture();
        }
        else if (Input.GetAxis("P" + playerNumber + "_DrawRight") > 0.9f
            && Input.GetKey("joystick " + playerNumber + " button 1")
            || Input.GetKey(playerNumber.ToString()))
        {
            icon.SetToDefault();
        }

        else if (Input.GetAxis("P" + playerNumber + "_DrawRight") > 0.9f
            && Input.GetKey("joystick " + playerNumber + " button 2"))
        {
            icon.SetReady(true);
        }

        else if (Input.GetAxis("P" + playerNumber + "_DrawRight") > 0.9f
            && Input.GetKey("joystick " + playerNumber + " button 3"))
        {
            icon.SetToDefault();
            icon.SetReady(false);
        }
    }
}
