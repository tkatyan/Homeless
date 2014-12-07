using UnityEngine;
using System.Collections;

public class PostHardcoreHeroScript : MonoBehaviour
{

    // движение
	public float Speed = 10f;
    public float SprintSpeed = 20f;
    private float CurrentSpeed = 10f;
	public Vector2 move;
	bool facingRight = true;

    // счет
    private int bottleCount = 0;

    // спринт
    private float SprintTime = 0.7f;
    private float CurrentSprintTime = 0f;
    private float SprintCooldown = 5f;
    private float CurrentSprintCooldown = 0;

    // таймеры
    public float globalTimer;

	// Use this for initialization
	void Start ()
	{

	}

	void FixedUpdate()
	{
	    move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
	}

	// Update is called once per frame
	void Update () {
		rigidbody2D.velocity = new Vector2 (move.x * CurrentSpeed, move.y * CurrentSpeed);
		if (facingRight && move.x < 0 || !facingRight && move.x > 0)
						Flip ();
	    if (CurrentSprintCooldown <= 0 && Input.GetKeyDown(KeyCode.Joystick1Button5))
	    {
            CurrentSprintTime = SprintTime;
	        CurrentSpeed = SprintSpeed;
	        CurrentSprintCooldown = SprintCooldown;
	    }
	    if (CurrentSprintTime > 0)
	    {
	        CurrentSprintTime -= Time.deltaTime;
            if (CurrentSprintTime <= 0)
    	        CurrentSpeed = Speed;
	    }
	    if (CurrentSprintCooldown > 0)
        {
            CurrentSprintCooldown -= Time.deltaTime;
	    }
            
		if (Input.GetKeyDown (KeyCode.R))
						Application.LoadLevel (Application.loadedLevel);
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale; 
	}

	void OnCollisionEnter2D(Collision2D col)
	{
	    if (col.gameObject.tag == "Killer")
	        Application.LoadLevel(Application.loadedLevel);
	}


    void OnGUI()
    {
        //GUI.Box(new Rect(5, 5, 200, 50), "Бутылок собрано: " + bottleCount + "\nВремени осталось: ");

    }
}
