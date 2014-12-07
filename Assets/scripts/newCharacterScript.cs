using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class newCharacterScript : MonoBehaviour {

    // движение
	public float Speed = 10f;
    public float SprintSpeed = 20f;
    private float CurrentSpeed = 10f;
    public float throwForce = 500f;
    public GameObject bottle;
    public GameObject weight;
    public int XMinMax;
	public float move;
	bool facingRight = true;

    // счет
    private int bottleCount = 0;

    // спринт
    private float SprintTime = 0.7f;
    private float CurrentSprintTime = 0f;
    private float SprintCooldown = 5f;
    private float CurrentSprintCooldown = 0;


    // таймеры
    private enum Timers
    { Global, Bottle}
    public float[] timer;

	// Use this for initialization
	void Start ()
    {
        timer = new float[3];
        timer[(int)Timers.Global] = 60f;
	}

	void FixedUpdate()
	{
		move = Input.GetAxis ("Horizontal");
	}

	// Update is called once per frame
	void Update () {
		rigidbody2D.velocity = new Vector2 (move * CurrentSpeed, rigidbody2D.velocity.y);
        rigidbody2D.position = new Vector2(Mathf.Clamp(rigidbody2D.position.x,-XMinMax,XMinMax),rigidbody2D.position.y);
		if (facingRight && move < 0 || !facingRight && move > 0)
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
		

        // уменьшаем таймеры
        for (int i = 0; i < timer.GetLength(0); i++)
            if (timer[i] > 0)
                timer[i] -= Time.deltaTime;

	    GameObject someObj;
        var l = new List<GameObject>();
        if (timer[(int)Timers.Bottle] <= 0)
        {
            l.Add(Instantiate((Random.Range(1, 6) == 5)?weight:bottle, new Vector3(-9.5f + Random.Range(0, 5) * 4.75f, 2.7f, 0),
                Quaternion.identity) as GameObject);
            l.Last().rigidbody2D.velocity = new Vector2(Random.Range(-1,2), Random.Range(0,2))*throwForce;
           timer[(int)Timers.Bottle] = 1 / 20f * timer[(int)Timers.Global];
        }
	    foreach (var o in l)
	    {

	        o.gameObject.transform.Rotate( new Vector3(0,0,1), Random.Range(1,100)*10);
	    }

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
	    if (col.gameObject.tag == "Collectable")
	    {
	        bottleCount++;
	        Destroy(col.gameObject);
	    }
	    if (col.gameObject.tag == "Killable")
            Application.LoadLevel(Application.loadedLevel);
	}


    void OnGUI()
    {
        GUI.Box(new Rect(5, 5, 200, 50), "Бутылок собрано: " + bottleCount);// + "\nВремени осталось: " + (int) timer[0]);

        GUI.Box(new Rect(Screen.width - 200, 5, 200, 50),
            "\nВремени осталось: " + (int)timer[0] + "\n" + timer[(int)Timers.Bottle]);
    }
}
