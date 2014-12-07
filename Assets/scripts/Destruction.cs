using UnityEngine;
using System.Collections;

public class Destruction : MonoBehaviour
{
    public ParticleEmitter Splinters;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Hero")
        {
            if (gameObject.tag == "Collectable")
            {
                Instantiate(Splinters, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }

}
