using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public int playerNumber = 1;
    [Space(5)]
    public float walkSpeed;
    public float jumpForce;
    [Space(5)]
    public Material platformColor;
    public Platforms platformPositions;
    


    Rigidbody2D rb;
    Animator animator;
    bool grounded;
    BoxCollider2D col;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
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

        //Jumping
        float jump;
        if (grounded && Input.GetButtonDown("P" + playerNumber + "_Jump"))
        {
            jump = jumpForce;
            animator.SetTrigger("Jump");
        }
        else jump = rb.velocity.y;

        //Movement
        float xMovement = Input.GetAxis("P" + playerNumber + "_Horizontal");
        rb.velocity = new Vector2(xMovement * walkSpeed, jump);

        //Create platforms
        if (Input.GetButtonDown("P" + playerNumber + "_DrawLeft")) //left platform
        {
            //spawn platform from resources
            GameObject plat = Instantiate(Resources.Load("Platform"), transform.position + 
                new Vector3(platformPositions.left.x, platformPositions.left.y, 0), new Quaternion(0, 0, 0, 0)) as GameObject;
            plat.GetComponent<Renderer>().material = platformColor;
            
        }
        else if (Input.GetButtonDown("P" + playerNumber + "_DrawRight")) //right platform
        {
            //spawn platform from resources
            GameObject plat = Instantiate(Resources.Load("Platform"), transform.position +
                new Vector3(platformPositions.right.x, platformPositions.right.y, 0), new Quaternion(0, 0, 0, 0)) as GameObject;
            plat.GetComponent<Renderer>().material = platformColor;
        }

        //Animation paramaters
        animator.SetFloat("Speed", Mathf.Abs(xMovement));
        animator.SetBool("Grounded", grounded);
        animator.speed = Mathf.Abs(xMovement);
        Vector3 ls = transform.localScale;
        if (xMovement < -0.1f) transform.localScale = new Vector3(-Mathf.Abs(ls.x), ls.y, ls.z);
        if (xMovement > 0.1f) transform.localScale = new Vector3(Mathf.Abs(ls.x), ls.y, ls.z);

    }

    [System.Serializable]
    public class Platforms
    {
        public Vector2 left;
        public Vector2 right; 
    }
}
