using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour {

    public GameObject _sn;
    public Quaternion newAngle;
    public Vector3 direct;
    private Vector3 tmpDir;
    public float dist = 30f;
    public float side_dist = 1.5f;

    private void Start()
    {
        direct = _sn.transform.forward;
        tmpDir = direct;
        newAngle = transform.rotation;
    }

    void LateUpdate() {

        Vector3 tmpPos = _sn.transform.position;
        Vector3 curSide = _sn.GetComponent<SnakeMove>().curSide;
        if (curSide.x != 0) {
            tmpPos.x = curSide.x * (dist - Mathf.Max(Mathf.Abs(_sn.transform.position.y), Mathf.Abs(_sn.transform.position.z)));
            tmpPos.y *= side_dist;
            tmpPos.z *= side_dist;
        }
        else if (curSide.y != 0)
        {
            tmpPos.x *= side_dist;
            tmpPos.y = curSide.y * (dist - Mathf.Max(Mathf.Abs(_sn.transform.position.x), Mathf.Abs(_sn.transform.position.z)));
            tmpPos.z *= side_dist;
        }
        else if (curSide.z != 0)
        {
            tmpPos.x *= side_dist;
            tmpPos.y *= side_dist;
            tmpPos.z = curSide.z * (dist - Mathf.Max(Mathf.Abs(_sn.transform.position.y), Mathf.Abs(_sn.transform.position.x)));
        }
        transform.position = Vector3.Lerp(transform.position, tmpPos, Time.deltaTime * _sn.GetComponent<SnakeMove>().speed);
        Quaternion tmpRot;
        tmpDir = Vector3.Lerp(tmpDir, direct, Time.deltaTime * 10f);
        tmpRot = Quaternion.LookRotation(Vector3.zero - transform.position, tmpDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, tmpRot, Time.deltaTime*10f);
    }
}
