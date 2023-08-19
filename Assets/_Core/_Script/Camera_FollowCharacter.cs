using UnityEngine;

public class Camera_FollowCharacter : MonoBehaviour
{
    public Transform target; // Referência ao Transform do personagem
    public Vector3 offset = new Vector3(0, 0, -10); // Deslocamento da câmera em relação ao personagem
    public float smoothSpeed = 0.125f; // Velocidade de suavização do movimento da câmera

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}