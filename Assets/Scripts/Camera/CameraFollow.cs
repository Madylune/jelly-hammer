using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private float xMax;

    [SerializeField]
    private float xMin;

    [SerializeField]
    private float yMax;

    [SerializeField]
    private float yMin;

    private void LateUpdate()
    {
        if (Player.MyInstance != null)
        {
            transform.position = new Vector3(Mathf.Clamp(Player.MyInstance.transform.position.x, xMin, xMax), Mathf.Clamp(Player.MyInstance.transform.position.y, yMin, yMax), transform.position.z);
        }
    }
}
