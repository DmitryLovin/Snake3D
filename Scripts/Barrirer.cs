using UnityEngine;

public class Barrirer : MonoBehaviour {

    public Vector3 dir1, dir2;

    /*private void OnTriggerEnter(Collider other)
    {
        if (_sn.GetComponent<SnakeMove>().curSide == dir1)
        {
            _sn.GetComponent<SnakeMove>().curSide = dir2;
            //_sn.GetComponent<CreateTrail>().renderDirection = dir2;
        }
        else {
            _sn.GetComponent<SnakeMove>().curSide = dir1;
            //_sn.GetComponent<CreateTrail>().renderDirection = dir1;
        }
        _sn.transform.rotation *= Quaternion.Euler(90f, 0, 0);
        Camera.main.GetComponent<CamMove>().newAngle *= Quaternion.Euler(90f, 0, 0);
    }*/
}
