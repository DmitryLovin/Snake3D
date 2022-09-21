using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManuCamera : MonoBehaviour {

    private int sign_x, sign_y, sign_z;
    private float angle_x, angle_y, angle_z;

	void Start () {
        sign_x = Random.Range(0, 2);
        sign_y = Random.Range(0, 2);
        sign_z = Random.Range(0, 2);
        angle_x = Random.Range(0.05f, 0.55f);
        angle_y = Random.Range(0.05f, 0.55f);
        angle_z = Random.Range(0.05f, 0.55f);
        StartCoroutine("newAngle");
    }
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation * Quaternion.Euler(angle_x * (sign_x - 1 + (1 * sign_x)), angle_y * (sign_y - 1 + (1 * sign_y)), angle_z * (sign_z - 1 + (1 * sign_z))),Time.deltaTime);
	}

    IEnumerator newAngle() {
        yield return new WaitForSeconds(5f);
        angle_x = Random.Range(0.05f, 0.55f);
        angle_y = Random.Range(0.05f, 0.55f);
        angle_z = Random.Range(0.05f, 0.55f);
        StartCoroutine("newAngle");
    }
}
