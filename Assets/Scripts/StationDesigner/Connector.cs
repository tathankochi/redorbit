using Unity.VisualScripting;
using UnityEngine;

public class Connector : MonoBehaviour
{
    private Vector3 localOffset;

    void Start()
    {
        localOffset = transform.localPosition;
    }

    void Update()
    {
        
        Module parentModule = GetComponentInParent<Module>();
        if (parentModule == null) return;
        if (parentModule.isKinematic) return;
        
        Connector otherConnector = ConnectionManager.Instance.IsColliding(this);
        if (otherConnector == null) return;
        Debug.Log("Connector collision detected with " + otherConnector.name);
        Debug.Log("Snapping to other connector");
        Debug.Log("This connector position: " + transform.position);
        Debug.Log("Other connector position: " + otherConnector.transform.position);
        
        // move the parent module by the difference
        Vector3 difference = transform.position - otherConnector.transform.position;
        parentModule.transform.position += difference;
        parentModule.moveTilemap(-difference);

        // calculate the target position of this connector's parent module


        Vector3 worldOffset = transform.parent.TransformVector(localOffset);
        Vector3 targetPosition = otherConnector.transform.position - worldOffset;
        transform.parent.position = targetPosition;

        parentModule.isDragged = false;
        parentModule.isKinematic = true;

        Debug.Log("Parent module found and set to kinematic: " + parentModule.name);
        ConnectionManager.Instance.MakeConnectorTransparent(otherConnector);
        ConnectionManager.Instance.MakeConnectorTransparent(this);
    }
}
