using UnityEngine;

public class FloatInSpace : MonoBehaviour
{
    private void Update()
    {
        float moveX = GameManager.Instance.adjustedWorldSpeed;
        transform.position += new Vector3(-moveX, 0);
        if (transform.position.x < -11)
        {
            gameObject.SetActive(false);
        }
    }
}
