using UnityEngine;
using System.Collections;

public class FlickerLight : MonoBehaviour
{
    public Light lightSource;

    public float minTime = 0.05f;
    public float maxTime = 0.3f;

    void Start()
    {
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            lightSource.enabled = !lightSource.enabled;

            yield return new WaitForSeconds(
                Random.Range(minTime, maxTime)
            );
        }
    }
}
