
using UnityEngine;

public class SpriteHoverOutline : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Camera targetCamera;
    [SerializeField] private LayerMask highlightLayer = ~0;
    [SerializeField] private Color outlineColor = Color.green;
    [SerializeField] private float outlineWidth = 5f;

    private Outline currentOutline;

    private void Start()
    {
        if (targetCamera == null)
            targetCamera = Camera.main;
    }

    private void Update()
    {
        CheckHover();
    }

    private void CheckHover()
    {
        Vector2 mouseWorld =
            targetCamera.ScreenToWorldPoint(Input.mousePosition);

        Collider2D hit = Physics2D.OverlapPoint(mouseWorld);
        Outline newRenderer = null;

        if (hit != null)
        {
            newRenderer = hit.GetComponent<Outline>();

            Debug.Log("Hitted collider " + hit.name);
        }

        if (newRenderer != currentOutline)
        {
            DisableCurrentOutline();

            currentOutline = newRenderer;

            if (currentOutline != null)
            {
                currentOutline.OutlineColor = outlineColor;
                currentOutline.OutlineWidth = outlineWidth;
                currentOutline.enabled = true;
            }

            if (currentOutline != null)
                Debug.Log("current outline " + currentOutline.name);
        }
    }

    private void DisableCurrentOutline()
    {
        if (currentOutline != null)
        {
            currentOutline.enabled = false;
            currentOutline = null;
        }
    }

    public void SetOutlineColor(Color color)
    {
        outlineColor = color;

        if (currentOutline != null)
            currentOutline.OutlineColor = color;
    }

    public void SetOutlineWidth(float width)
    {
        outlineWidth = width;

        if (currentOutline != null)
            currentOutline.OutlineWidth = width;
    }
}
