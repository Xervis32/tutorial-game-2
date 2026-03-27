using UnityEngine;

public class FaceMovementDirection : MonoBehaviour
{
    private Vector3 previousPosition;
    private Vector3 moveDirection;
    private Quaternion targetRotation;
    private float rotationSpeed = 200;

    void Start()
    {
        previousPosition = transform.position;
    }

    void Update()
    {
        previousPosition -= new Vector3(GameManager.Instance.adjustedWorldSpeed, 0);
        moveDirection = transform.position - previousPosition;

        targetRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        previousPosition = transform.position;
    }
}
