using UnityEngine;

public class MissingPlanet : MonoBehaviour
{
    public PlanetType MissingPlanetType { get; set; }

    public int IndexNumber { get; set; }

    private void Start()
    {
        Debug.Log(MissingPlanetType);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("AnswerPlanet"))
        {
            Debug.Log(other.GetComponent<AnswerPlanet>().PlanetType + " : " + MissingPlanetType);

            if(other.GetComponent<AnswerPlanet>().PlanetType == MissingPlanetType)
            {
                Debug.Log("Enter!");

                InGameManager.Instance.FindPlanet(this);

                //Destroy(other.gameObject);
            }
        }
    }
}
