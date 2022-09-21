using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _anim.Play("Off");
    }

    public void OnPointerDown(PointerEventData eventData) {
        _anim.Play("On");
    }
}
