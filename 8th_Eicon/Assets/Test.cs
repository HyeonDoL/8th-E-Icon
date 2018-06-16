using UnityEngine;

public class Test : MonoBehaviour
{
    private MeshRenderer mr;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            mr.material.color = Random.ColorHSV();
        }
    }
}
