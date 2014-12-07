using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Collections;

public class ChildScript : MonoBehaviour
{
    public Transform bomzh;

    public bool clockwise;

    public float speed = 6f;

    int quater(float a, float b)
    {
        if (a >= 0 && b >= 0)
            return 1;
        if (a < 0 && b >= 0)
            return 2;
        if (a < 0 && b < 0)
            return 3;
        return 4;
    }

    Vector3 getDirection()
    {
        Vector3 ret = new Vector3();
        if (quater(transform.position.x, transform.position.y) ==
            quater(bomzh.transform.position.x, bomzh.transform.position.y))
            ret = bomzh.transform.position - transform.position;
        else
        {
            if (quater(transform.position.x, transform.position.y)==1 && quater(bomzh.transform.position.x, bomzh.transform.position.y) ==2 ||
                quater(transform.position.x, transform.position.y)==2 && quater(bomzh.transform.position.x, bomzh.transform.position.y) ==3 ||
                quater(transform.position.x, transform.position.y)==3 && quater(bomzh.transform.position.x, bomzh.transform.position.y) ==4 ||
                quater(transform.position.x, transform.position.y)==4 && quater(bomzh.transform.position.x, bomzh.transform.position.y) ==1)
            {
                if (transform.position.x > 0 && transform.position.y > 0) // 1
                    ret = new Vector3(0, 3, 0);
                if (transform.position.x > 0 && transform.position.y <= 0) // 4
                    ret = new Vector3(9, 0, 0);
                if (transform.position.x <= 0 && transform.position.y > 0) // 2
                    ret = new Vector3(-9, 0, 0);
                if (transform.position.x <= 0 && transform.position.y <= 0) // 3
                    ret = new Vector3(0, -3, 0);
                clockwise = false;

            }
            else
            {
                if (transform.position.x >= 0 && transform.position.y >= 0)
                    ret = new Vector3(9, 0, 0);
                if (transform.position.x >= 0 && transform.position.y < 0)
                    ret = new Vector3(0, -3, 0);
                if (transform.position.x < 0 && transform.position.y >= 0)
                    ret = new Vector3(0, 3, 0);
                if (transform.position.x < 0 && transform.position.y < 0)
                    ret = new Vector3(-9, 0, 0);
                clockwise = true;
            }
            ret -= new Vector3(transform.position.x, transform.position.y, 0);
        }
        ret.Normalize();
        return ret;
    }

	// Use this for initialization
	void Start ()
    {
        transform.position = new Vector3(0.69f, 0.67f, 0);
    }
	
	// Update is called once per frame  
	void Update ()
	{
	    transform.position += getDirection() * speed * Time.deltaTime;
	}
}
