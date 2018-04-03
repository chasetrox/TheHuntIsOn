using UnityEngine;
using UnityEngine.Networking;

public class GunPivotSync : NetworkBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform gunPivot;

	// Add RPC to move other players' guns
    void Start()
   {
       Debug.Log("Script exists");
       if (isLocalPlayer) {
           gunPivot.parent = cameraTransform;
       }
   }
}
