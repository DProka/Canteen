
using DG.Tweening;
using UnityEngine;

public class NpcPrefab : MonoBehaviour
{
    public int id { get; private set; }

    [SerializeField] Animator animator;

    private float startPosX = 0;
    private float walkTime = 5f;

    private bool isFliped;

    public void Init(int _id, float startPos)
    {
        id = _id;
        startPosX = startPos;
        isFliped = false;

        if (startPosX < 0)
            isFliped = true;

        Debug.Log("NPC initialized: " + id);
    }

    public void StartWalkToPoint(Vector2 point, float _walkTime)
    {
        walkTime = _walkTime;
        StartWalkAnimation();
        CheckPrefabScale(true);
        transform.DOMoveX(point.x, walkTime).OnComplete(() => StopAtPoint());
    }

    public void StopAtPoint()
    {
        StartIdleAnimation();
        EventBus.OnNpcStoped?.Invoke(id);
    }

    public void StartWalkToExtit()
    {
        StartWalkAnimation();
        CheckPrefabScale(false);
        transform.DOMoveX(startPosX, walkTime).OnComplete(() => Destroy(gameObject));
    }

    private void CheckPrefabScale(bool isGoingToPoint)
    {
        if (isGoingToPoint)
        {
            if(isFliped)
                transform.localScale = new Vector2(-1, transform.localScale.y);
            else
                transform.localScale = new Vector2(1, transform.localScale.y);
        }
        else
        {
            if (isFliped)
                transform.localScale = new Vector2(1, transform.localScale.y);
            else
                transform.localScale = new Vector2(-1, transform.localScale.y);
        }
    }

    #region Animations

    private void StartIdleAnimation()
    {
        animator.SetBool("IsWalking", false);
    }

    private void StartWalkAnimation()
    {
        animator.SetBool("IsWalking", true);
    }

    #endregion
}
