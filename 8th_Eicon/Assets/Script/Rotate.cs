using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed;

    [SerializeField]
    private bool isLocal;

    private void FixedUpdate()
    {
        if(isLocal)
        {
            Vector3 rotation = transform.localRotation.eulerAngles;

            transform.localRotation = Quaternion.Euler(rotation.x,
                                                       rotation.y + rotateSpeed,
                                                       rotation.z);
        }

        else
            transform.Rotate(transform.up, rotateSpeed, Space.Self);
    }
}
