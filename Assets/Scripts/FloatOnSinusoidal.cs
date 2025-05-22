using UnityEngine;

public class FloatOnSinusoidal : MonoBehaviour
{
    public WavesController waveSystem;
    private Wave waveInfo;

    public float radius = 0.5f;
    public float mass = 500f;
    public float damping = 0.98f;

    private Vector3 previousPosition;
    private float gravity = -9.81f;

    void Start()
    {
        waveInfo = waveSystem.GetWaveInfo();
        previousPosition = transform.position;
    }

    void Update()
    {
        if (waveSystem == null || waveSystem.GetCurrentMethod() != Method.SINUSOIDAL)
            return;

        float time = waveSystem.GetTime();
        Vector3 currentPosition = transform.position;

        float waterHeight = waveSystem.sinusoidal.GetWaveHeight(currentPosition, waveInfo, time);
        bool isSubmerged = currentPosition.y - radius < waterHeight;

        float totalForce;

        if (isSubmerged)
        {
            float gravityForce = GetGravityForce();
            float buoyantForce = GetBuoyantForce(waterHeight);
            totalForce = gravityForce + buoyantForce;
        }
        else
        {
            totalForce = GetGravityForce();
        }

        float acceleration = totalForce / mass;
        float dt = Time.deltaTime;

        Vector3 velocity = currentPosition - previousPosition;
        if (isSubmerged)
            velocity *= damping;

        Vector3 newPosition = currentPosition + velocity + Vector3.up * acceleration * dt * dt;

        previousPosition = currentPosition;
        transform.position = newPosition;
    }

    float GetGravityForce()
    {
        return mass * gravity;
    }

    float GetBuoyantForce(float waterHeight)
    {
        float fluidDensity = 1000;
        float displacedVolume = GetDisplacedVolume(waterHeight);

        return fluidDensity * Mathf.Abs(gravity) * displacedVolume;
    }

    float GetDisplacedVolume(float waterHeight)
    {
        float heightOfDisplacedVolume = waterHeight - (transform.position.y - radius);
        heightOfDisplacedVolume = Mathf.Clamp(heightOfDisplacedVolume, 0f, 2f * radius);

        if (heightOfDisplacedVolume >= 2.0f * radius)
        {
            return 4.0f / 3.0f * Mathf.PI * Mathf.Pow(radius, 3);
        }
        else
        {
            return 1.0f / 3.0f * Mathf.PI * Mathf.Pow(heightOfDisplacedVolume, 2) * (3 * radius - heightOfDisplacedVolume);
        }
    }
}
