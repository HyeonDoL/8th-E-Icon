using UnityEngine;

public enum PlanetType
{
    Mercury = 0,
    Venus,
    Earth,
    Luna,
    Mars,
    Jupiter,
    Saturn,
    Uranus,
    Neptune
}

public class Planet : MonoBehaviour
{
    [SerializeField]
    private PlanetType planetType;

    [SerializeField]
    private float cycleDay;

    [SerializeField]
    private float rotateSpeed;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private GameObject originalModel;

    [SerializeField]
    private GameObject missingModel;

    private float moveSpeed;

    private float distance;

    public float Theta { get; set; }

    public PlanetType PlanetType { get { return planetType; } }

    public GameObject OriginalModel { get { return originalModel; } }

    public GameObject MissingModel { get { return missingModel;} }

    // 6.28f = 2Pie
    private const float limit = 6.28f;

    private void Awake()
    {
        moveSpeed = limit / cycleDay;

        distance = Vector3.Distance(target.position, transform.position);

        missingModel.GetComponent<MissingPlanet>().MissingPlanetType = planetType;

        Theta = 0;
    }

    private void FixedUpdate()
    {
        Theta += Time.deltaTime;

        transform.position = GetPosition(Theta);

        transform.Rotate(transform.up, rotateSpeed, Space.Self);
    }

    public Vector3 GetPosition(float theta)
    {
        Vector3 movement = new Vector3(distance * Mathf.Cos(theta * moveSpeed),
                                       0,
                                       distance * Mathf.Sin(theta * moveSpeed));

        return target.position + movement;
    }
}
