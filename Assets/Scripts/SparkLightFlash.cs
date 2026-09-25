using UnityEngine;

public class SparkLightFlash : MonoBehaviour
{
    public ParticleSystem sparks;
    public Light sparkLight;
    public float flashIntensity = 5f;
    public float flashDuration = 0.08f;

    public float timer;

    void Start()
    {
        sparkLight.intensity = 0f;
    }

    void Update()
    {
        if (sparks.isPlaying)
        {
            sparkLight.intensity = flashIntensity;

            timer += Time.deltaTime;

            if (timer >= flashDuration)
            {
                sparkLight.intensity = 0f;
                timer = 0f;
            }
        }
        else
        {
            sparkLight.intensity = 0f;
            timer = 0f;
        }
    }
}

