using UnityEngine;
using UnityEngine.EventSystems;

public class Click : MonoBehaviour, IPointerDownHandler {

    private GameObject pl;
    [SerializeField]
    private int rot,roty;

    private void Start()
    {
        pl = GameObject.Find("player");
    }

    public void OnPointerDown(PointerEventData eventData) {
        pl.GetComponent<SnakeMove>().SetDirection(new Vector2(-rot, 0));
        pl.GetComponent<SnakeMove>().SetDirection(new Vector2(0, roty));
    }
}
