using System;
using System.Collections;
using UnityEngine;

public class TwoStatePlatform : MonoBehaviour
{
    [SerializeField] private Transform firstPos;
    [SerializeField] private Transform secondPos;
    [SerializeField] private Transform platform;
    [SerializeField] private float moveSpeed = 5f;

    private Coroutine moveCoroutine = null;

    public void SetMove(bool toSecond = false)
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(Move(toSecond ? secondPos : firstPos));
    }

    private IEnumerator Move(Transform targetPos)
    {
        while (Vector3.Distance(firstPos.position, secondPos.position) > .1f)
        {
            platform.position = Vector3.MoveTowards(platform.position, targetPos.position, moveSpeed * Time.fixedDeltaTime);
            yield return null;
        }
        yield return null;
    }
}