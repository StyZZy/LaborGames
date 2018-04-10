using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour {
    public float lifeTime;
    public float explosionForce;
    public float explosionRadius;
    public float upModifier;
    public LayerMask bombMask;
    public GameObject explosionEffect;
	// Use this for initialization
	void Start () {
        Invoke("explode", lifeTime);
	}
	
	// Update is called once per frame
	void explode () {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, bombMask);
        foreach(Collider c in hitColliders)
        {
            Rigidbody r = c.GetComponent<Rigidbody>();
            if (r != null)
                r.AddExplosionForce(explosionForce, transform.position, explosionRadius, upModifier);
        }
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        gameObject.GetComponent<AudioSource>().Play();
        gameObject.GetComponent<Renderer>().enabled = false;
        Invoke("destroy", 2);
	}
    void destroy()
    {
        Destroy(gameObject);
    }
}
