using UnityEngine;

public class PistolLine : MonoBehaviour
{
    public Transform lineOrigin;
    public LineRenderer pistolLine;
    public float distance = 2f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pistolLine.SetPosition(0, lineOrigin.position);
        Vector3 endPoint = lineOrigin.position + lineOrigin.forward * distance;
        pistolLine.SetPosition(1, endPoint);
    }
}
