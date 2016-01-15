using UnityEngine;
using System.Collections;

public class SpawnObjects : MonoBehaviour {

    public GameObject[] spawnObjects;

	void Start ()
    {
        StartCoroutine(Spawner());
    }
	
    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(Random.Range(1, 5));
        GameObject fallObject = Instantiate(spawnObjects[Random.Range(0, spawnObjects.Length)],transform.position,transform.rotation) as GameObject;
        fallObject.GetComponent<Rigidbody>().AddForce(Vector3.down * 50);
        StartCoroutine(Spawner());
    }
}
