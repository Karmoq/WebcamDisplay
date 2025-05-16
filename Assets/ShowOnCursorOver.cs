using UnityEngine;
using UnityEngine.InputSystem;

public class ShowOnCursorOver : MonoBehaviour
{
    public RectTransform RectTransform;
    
    private void Show(bool show)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(show);
        }
    }

    private void Update()
    {
        Show(RectTransformUtility.RectangleContainsScreenPoint(RectTransform, Input.mousePosition));
    }
}
