using UnityEngine;

public class FloatOnSinusoidal : MonoBehaviour
{
    public WavesController waveSystem;
    private Wave waveInfo;

    public float radius = 0.5f; // Supón que la boya es una esfera
    public float mass = 1f;     // Masa de la boya
    public float density = 1000f; // Densidad del agua

    private float velocity = 0f; // Solo eje Y
    private float gravity = -9.81f;

    void Start()
    {
        waveInfo = waveSystem.GetWaveInfo();
    }

    void Update()
    {
        if (waveSystem == null || waveSystem.GetCurrentMethod() != Method.SINUSOIDAL) return;

        float time = waveSystem.GetTime();
        Vector3 pos = transform.position;

        // Altura del agua en la posición de la boya
        float waterHeight = waveSystem.sinusoidal.GetWaveHeight(pos, waveInfo, time);

        // ¿Cuánto está sumergida la boya?
        float bottom = pos.y - radius;
        float submergedDepth = Mathf.Clamp(waterHeight - bottom, 0, 2 * radius);

        // Volumen desplazado (esfera parcialmente sumergida)
        float displacedVolume = (Mathf.PI * submergedDepth * submergedDepth * (3 * radius - submergedDepth)) / 3f;

        // Fuerza de flotación
        float buoyantForce = density * -gravity * displacedVolume;

        // Fuerza neta y aceleración
        float weight = mass * gravity;
        float netForce = buoyantForce + weight;
        float acceleration = netForce / mass;

        // Integrar velocidad y posición
        velocity += acceleration * Time.deltaTime;
        pos.y += velocity * Time.deltaTime;

        transform.position = pos;
    }
}
