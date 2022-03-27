using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public float duration = 1f;
    public AnimationCurve animationCurve;
    public bool start = false;
    IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float strength = animationCurve.Evaluate(elapsedTime / duration) * 5;
            elapsedTime += Time.deltaTime;
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }
    }
}
