using UnityEngine;
using System.Collections;

using Vuforia;


// FIXME : this cord is so hard coding and this is super class
public class SolarSystem : MonoBehaviour, ITrackableEventHandler
{
    [SerializeField]
    private Transform solarArrivePoint;

    [SerializeField]
    private Planet[] planets;

    [SerializeField]
    private AnimationCurve moveCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [SerializeField]
    private TrackableBehaviour trackableBehaviour;

    private const float moveSpeed = 0.5f;

    public Planet[] Planets { get { return planets; } }

    private void Start()
    {
        trackableBehaviour.RegisterTrackableEventHandler(this);
    }

    private void Initialize()
    {
        transform.localPosition = Vector3.zero;

        for (int i = 0; i < planets.Length; ++i)
        {
            planets[i].transform.localPosition = Vector3.zero;

            planets[i].enabled = false;
        }
    }

    public IEnumerator MoveArrivePoint(bool isStop)
    {
        //StopAllCoroutines();

        yield return StartCoroutine(Tween.TweenTransform.Position(this.transform, solarArrivePoint.position, 0.25f, moveCurve));

        for (int i = 0; i < planets.Length; ++i)
        {
            float theta = Random.Range(0f, 10000f);

            planets[i].Theta = theta;

            Vector3 arrivePoint = planets[i].GetPosition(theta);

            StartCoroutine(MovePlanets(planets[i], arrivePoint, isStop));
        }
    }

    public void MoveStartPoint(bool isStop)
    {
        StopAllCoroutines();

        for (int i = 0; i < planets.Length; ++i)
            StartCoroutine(MovePlanets(planets[i], solarArrivePoint.position, isStop));
    }

    private IEnumerator MovePlanets(Planet planet, Vector3 arrivePoint, bool isStop)
    {
        if (isStop)
            planet.enabled = false;

        yield return StartCoroutine(Tween.TweenTransform.Position(planet.transform, arrivePoint, moveSpeed, moveCurve));

        if (!isStop)
            planet.enabled = true;
    }

    public void Stop(bool isStop)
    {
        for (int i = 0; i < planets.Length; ++i)
            planets[i].enabled = !isStop;
    }

    void ITrackableEventHandler.OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else
        {
            OnTrackingLost();
        }
    }

    private void OnTrackingFound()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Enable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = true;
        }

        // Enable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = true;
        }

        Initialize();

        StartCoroutine(MoveArrivePoint(false));
    }

    private void OnTrackingLost()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Disable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = false;
        }

        // Disable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = false;
        }

        
    }

}