using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    private bool _exploding = false;

    public void IsExploding()
    {
        if (gameObject.transform.position == null)// player)
        {
            _exploding = true;
            Destroy(gameObject);
            // FindObjectOfType<Sound>("Exploding");
            // player  -HP, slow movement for few sec
        }

        ParticleEffect();
    }

    public void ParticleEffect()
    {

    }
}
