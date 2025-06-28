using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private void Awake()
    {
        transform.position = new Vector3((AreaManager.width + 1) *6, (AreaManager.height+AreaManager.height - 2) * 6, (AreaManager.height - 1) *6);
        transform.rotation = Quaternion.Euler(75, -90, 0); // 카메라 각도 설정
    }
}
