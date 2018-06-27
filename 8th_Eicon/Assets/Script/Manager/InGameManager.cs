using UnityEngine;
using System.Collections;

public class InGameManager : MonoBehaviour
{
    private static InGameManager instance;
    public static InGameManager Instance
    {
        get
        {
            if (!instance)
                return instance = new GameObject("InGameManager").AddComponent<InGameManager>();

            else
                return instance;
        }
    }

    private void Awake()
    {
        instance = this;

        MissingPlanetCount = 0;
    }

    public int MissingPlanetCount { get; set; }

    [SerializeField]
    private SolarSystem solarSystem;

    [SerializeField]
    private Transform missingPlanetPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            StartCoroutine(solarSystem.MoveArrivePoint(false));

        if (Input.GetKeyDown(KeyCode.B))
            solarSystem.MoveStartPoint(true);

        if (Input.GetKeyDown(KeyCode.C))
            StartCoroutine(MakeQuestion(3));
    }

    public void DoMakeQuestion(int questionCount)
    {
        StartCoroutine(MakeQuestion(questionCount));
    }

    public IEnumerator MakeQuestion(int missingCount)
    {
        solarSystem.MoveStartPoint(true);

        yield return YieldInstructionCache.WaitForSeconds(0.5f);

        for (int i = 0; i < missingCount; ++i)
            MissingPlanet();

        yield return StartCoroutine(solarSystem.MoveArrivePoint(true));
    }

    private void MissingPlanet()
    {
        int planetIndex;

        do
        {
            planetIndex = Random.Range(0, solarSystem.Planets.Length);
        }
        while (solarSystem.Planets[planetIndex].MissingModel.activeSelf);

        solarSystem.Planets[planetIndex].OriginalModel.SetActive(false);
        solarSystem.Planets[planetIndex].MissingModel.SetActive(true);

        Transform planet = solarSystem.Planets[planetIndex].transform;

        Transform missingPlanetTrans = solarSystem.Planets[planetIndex].MissingModel.transform;

        MissingPlanet missingPlanet = missingPlanetTrans.GetComponent<MissingPlanet>();

        missingPlanet.IndexNumber = planetIndex;

        missingPlanetTrans.position = planet.position;

        missingPlanetTrans.parent = planet;
    }

    public void FindPlanet(MissingPlanet missingPlanet)
    {
        Planet planet = solarSystem.Planets[missingPlanet.IndexNumber];

        planet.MissingModel.SetActive(false);

        planet.OriginalModel.SetActive(true);

        MissingPlanetCount -= 1;

        if (MissingPlanetCount <= 0)
            GameClear();
    }

    public void GameClear()
    {
        Debug.Log("Yeah!");
    }
}
