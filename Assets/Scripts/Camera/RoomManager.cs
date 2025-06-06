using UnityEngine;
using Unity.Cinemachine;
public class RoomManager : MonoBehaviour
{
    public CinemachineCamera virtualCam;
    public CinemachineConfiner2D confiner;
    public Transform roomTarg;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {

            virtualCam.Follow = roomTarg;


        }
    }
}
