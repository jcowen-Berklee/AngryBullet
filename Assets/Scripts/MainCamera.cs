//from Github/Ranux

using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform Target;
    private float _smooth = 5f;

    private const float Y = 3;
    private const float Z = -10;

    void Update()
    {
        // camera follows the Target
        float x = Mathf.Lerp(transform.position.x, Target.position.x, Time.deltaTime * _smooth);
        transform.position = new Vector3(x, Y, Z);
    }
}