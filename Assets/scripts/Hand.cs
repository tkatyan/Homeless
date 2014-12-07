using UnityEngine;

public class Hand : MonoBehaviour
{
    public int score;
    public GameObject Bottle;
    public GameObject Weight;
    public float RotatingSpeed = 10;
    public float speed = 10;
    public float returningSpeed = 20;
    public float SpeedCatched = 5;
    private LineRenderer line;
    private Ray handRay;
    private bool toRight;
    private bool catching;
    private bool returning;
    private bool catched;
    private Vector3 startPosition;
    private GameObject catchedObject;
	
    // Use this for initialization
    private void Awake()
    {
        score = 0;
        catched = false;
        returning = false;
        toRight = true;
        catching = false;
        startPosition = transform.position;
        line = GetComponent<LineRenderer>();
        line.SetColors(Color.clear, Color.black);
        line.SetPosition(0, startPosition);
        line.enabled = true;
    }

    private void Start()
    {
        Generate(5);
    }
    private void FixedUpdate()
    {
        hand();
    }

    void Generate(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var go = Instantiate((Random.Range(1, 6) == 5) ? Weight : Bottle,
                new Vector3(Random.Range(-13, 14), Random.Range(-6, 1)), Quaternion.identity) as GameObject;
            go.rigidbody2D.gravityScale = 0;
            Destroy(go.GetComponent<Destruction>());
            go.GetComponent<Collider2D>().isTrigger = true;
        }
    }

    void hand()
    {
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Joystick1Button0) ) catching = true;
        line.SetPosition(1, transform.position);
        if (Vector3.Distance(startPosition, transform.position) < 1 && (returning || catched))
        {
            if (catched)
                Destroy(catchedObject);
            transform.position = startPosition;
            returning = false;
            catched = false;
            rigidbody2D.velocity = Vector3.zero;
        }
        if (!catching && !returning && !catched)
            Rotating();
        else
            if (catching && !catched && !returning)
            {
                Catch();
            }
    }
    void Rotating()
    {
        if (toRight)
        {
            transform.Rotate(new Vector3(0, 0, 1), RotatingSpeed);
            if (transform.rotation.eulerAngles.z > 260)
                toRight = !toRight;
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, 1), -RotatingSpeed);
            if (transform.rotation.eulerAngles.z < 100)
                toRight = !toRight;
        }
        returning = false;
        catching = false;
        catched = false;
    }

    void Catch()
    {
        var dir = rigidbody2D.transform.up;
        dir.Normalize();
        rigidbody2D.velocity = dir*Time.deltaTime*speed;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Borders")
        {
            ReturnHand();
        }
    }

    void ReturnHand()
    {
        catching = false;
        returning = true;
        var dir = rigidbody2D.transform.up;
        dir.Normalize();
        rigidbody2D.velocity = -dir * Time.deltaTime * returningSpeed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Collectable" || col.gameObject.tag == "Killable")
        {
            catched = true;
            
            var dir = rigidbody2D.transform.up;
            dir.Normalize();
            
            var tran = col.gameObject.transform;
            col.gameObject.rigidbody2D.velocity = -dir*(1/(tran.lossyScale.x*tran.lossyScale.y)/10)*SpeedCatched*Time.deltaTime;
            rigidbody2D.velocity = col.gameObject.rigidbody2D.velocity;
            catching = false;
            returning = false;
            catchedObject = col.gameObject;
            score += (int)col.gameObject.rigidbody2D.mass;
        }
    }
}
