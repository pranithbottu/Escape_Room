using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	public Transform explosion;

    public bool runExplosion;
    private bool didRun = false;

    public bool escaped = false;
    // Use this for initialization
    void Start () {
		explosion.GetComponent<ParticleSystem> ().enableEmission = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(runExplosion && !didRun)
        {
            didRun = true;
            explosion.GetComponent<ParticleSystem>().enableEmission = true;
            StartCoroutine(stopExplosion());
        }
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag.Equals ("GunPowder")) 
		{
			explosion.GetComponent<ParticleSystem> ().enableEmission = true;
			StartCoroutine (stopExplosion ());
            GameObject.Destroy(collider.gameObject);
            escaped = true;
        }
	}

	IEnumerator stopExplosion()
	{
		yield return new WaitForSeconds (.4f);
		explosion.GetComponent<ParticleSystem> ().enableEmission = false;
	}
}
