using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterpolatedMovement : MonoBehaviour
{
    [SerializeField]
    AnimationCurve moveCurve;
    [SerializeField]
    private float lerpSpeed;

    public delegate void noArgs();
    IEnumerator SmoothMovement(Vector3 destination, noArgs onFinish)
    {
        Vector3 startPos = transform.position;
        float t = 0;
        while (Vector3.Distance(transform.position, destination) >= 0.01)
        {
            t += Time.deltaTime * lerpSpeed;
            transform.position = Vector3.Lerp(startPos, destination, moveCurve.Evaluate(t));
            yield return null;
        }
        transform.position = destination;
        onFinish?.Invoke();

    }

    public void MoveToTarget(Vector3 destination, noArgs onFinish = null)
    {
        StartCoroutine(SmoothMovement(destination,onFinish));
    }
}
