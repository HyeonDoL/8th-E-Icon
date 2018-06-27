using UnityEngine;

public class AnswerPlanet : MonoBehaviour
{
    [SerializeField]
    private PlanetType planetType;

    public PlanetType PlanetType { get { return planetType; } }
}
