using UnityEngine;

public class FloatOnSinusoidal : MonoBehaviour
{
    public WavesController waveSystem;
    private Wave waveInfo;

    public Vector3 positionOffset;
    public float radius = 0.5f;
    public float mass = 500f;
    public float damping = 0.98f;

    private Vector3 velocity;
    private float gravity = -9.81f;

    void Start()
    {
        waveInfo = waveSystem.GetWaveInfo(Method.SINUSOIDAL);
        velocity = Vector3.zero;
    }

    void Update()
    {
        if (waveSystem == null || waveSystem.GetCurrentMethod() != Method.SINUSOIDAL)
            return;

        float time = waveSystem.GetTime();

        Vector3 visualPosition = transform.position;
        Vector3 currentPosition = visualPosition - positionOffset;

        float waterHeight = waveSystem.sinusoidal.GetWaveHeight(currentPosition, waveInfo, time);
        bool isSubmerged = currentPosition.y - radius < waterHeight;

        float totalForce = GetGravityForce();
        if (isSubmerged)
            totalForce += GetBuoyantForce(waterHeight, currentPosition);

        float acceleration = totalForce / mass;
        float dt = Time.deltaTime;

        velocity += Vector3.up * acceleration * dt;
        if (isSubmerged)
            velocity *= damping;

        Vector3 newPosition = currentPosition + velocity * dt;
        transform.position = newPosition + positionOffset;
    }

    float GetGravityForce() => mass * gravity;

    float GetBuoyantForce(float waterHeight, Vector3 position)
    {
        float fluidDensity = 1000f;
        float displacedVolume = GetDisplacedVolume(waterHeight, position);
        return fluidDensity * Mathf.Abs(gravity) * displacedVolume;
    }

    float GetDisplacedVolume(float waterHeight, Vector3 position)
    {
        float height = waterHeight - (position.y - radius);
        height = Mathf.Clamp(height, 0f, 2f * radius);

        if (height >= 2f * radius)
            return 4f / 3f * Mathf.PI * Mathf.Pow(radius, 3);
        else
            return 1f / 3f * Mathf.PI * Mathf.Pow(height, 2) * (3 * radius - height);
    }

    void OnDrawGizmos()
    {
        Vector3 physicalPosition = transform.position - positionOffset;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(physicalPosition, radius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.1f);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, physicalPosition);
    }
}
