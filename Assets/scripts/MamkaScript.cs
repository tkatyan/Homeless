using System;
using UnityEngine;
using System.Collections;

public class MamkaScript : MonoBehaviour {


    public Transform bomzh;
    public ChildScript child;

    public float speed = 4f;

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
        
        var ret = new Vector3();
        {
            if (child.clockwise)
            {
                if (transform.position.x > 0 && transform.position.y > 0) // 1
                    ret = new Vector3(0, 2, 0);
                if (transform.position.x > 0 && transform.position.y <= 0) // 4
                    ret = new Vector3(7, 0, 0);
                if (transform.position.x <= 0 && transform.position.y > 0) // 2
                    ret = new Vector3(-7, 0, 0);
                if (transform.position.x <= 0 && transform.position.y <= 0) // 3
                    ret = new Vector3(0, -2, 0);

            }
            else
            {
                if (transform.position.x >= 0 && transform.position.y >= 0)
                    ret = new Vector3(7, 0, 0);
                if (transform.position.x >= 0 && transform.position.y < 0)
                    ret = new Vector3(0, -2, 0);
                if (transform.position.x < 0 && transform.position.y >= 0)
                    ret = new Vector3(0, 2, 0);
                if (transform.position.x < 0 && transform.position.y < 0)
                    ret = new Vector3(-7, 0, 0);
            }
            ret -= new Vector3(transform.position.x, transform.position.y, 0);
        }

        if (quater(transform.position.x, transform.position.y) ==
            quater(bomzh.transform.position.x, bomzh.transform.position.y))
        {
            quater(bomzh.transform.position.x, bomzh.transform.position.y);
            ret = bomzh.transform.position - transform.position;
        }

        ret.Normalize();
        return ret;
    }
    
    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(0.69f, 0.67f, 0);
    }

    // Update is called once per frame  
    void Update()
    {
        transform.position += getDirection() * Time.deltaTime * speed;
    }
}
