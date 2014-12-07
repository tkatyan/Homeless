using UnityEngine;
using System.Collections;

public class HeroMover : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        var z = new Vector3(h, v, 0);
        z.Normalize();
        transform.position += z*speed*Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Application.LoadLevel(col.gameObject.tag);
    }
}
