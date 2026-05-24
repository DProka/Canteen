
using System.Collections.Generic;
using UnityEngine;

public class ClickManager
{
    public static ClickManager Instance { get; private set; }

    private Dictionary<Collider2D, IClickable> _lookup;

    public ClickManager()
    {
        if (Instance != null && Instance != this)
        {
            return;
        }
        else
        {
            Instance = this;
        }

        _lookup = new Dictionary<Collider2D, IClickable>();
    }

    public void UpdateScript()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D[] hits = Physics2D.RaycastAll(worldPos, Vector2.zero);

            if (hits.Length > 0)
            {
                RaycastHit2D topHit = hits[0];
                //int topOrder = int.MinValue;

                //foreach (var hit in hits)
                //{
                //    var sr = hit.collider.GetComponent<SpriteRenderer>();
                //    if (sr != null && sr.sortingOrder > topOrder)
                //    {
                //        topOrder = sr.sortingOrder;
                //        topHit = hit;
                //    }
                //}

                if (_lookup.TryGetValue(topHit.collider, out var clickable))
                {
                    clickable.OnClick();
                }
                else
                {
                    Collider2D collider = topHit.collider.GetComponent<Collider2D>();
                    IClickable iclick = collider.GetComponent<IClickable>();
                    RegisterClickable(collider, iclick);
                    iclick.OnClick();
                }
                
                //topHit.collider.GetComponent<IClickable>()?.OnClick();
            }
        }
    }

    private void RegisterClickable(Collider2D collider, IClickable clickable)
    {
        if (!_lookup.ContainsKey(collider))
        {
            _lookup.Add(collider, clickable);
        }
    }
}
