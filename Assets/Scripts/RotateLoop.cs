using UnityEngine;

public class RotateLoop : MonoBehaviour
{
    // Initialise variables.
    public float speed;
    public bool isClockwise;
    private float rotationSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotationSpeed = Time.deltaTime * speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed));
    }
}
