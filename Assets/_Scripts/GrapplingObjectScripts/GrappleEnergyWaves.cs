using UnityEngine;

public class GrappleEnergyWaves : MonoBehaviour
{
    
    public Transform targetCore;

   
    public int segments = 5; 
    public float jitterAmount = 0.1f; 

    private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = segments;

       
        line.startWidth = 0.05f;
        line.endWidth = 0.01f;
    }

    void Update()
    {
        if (targetCore == null) return;

        
        line.SetPosition(0, transform.position);

        
        for (int i = 1; i < segments - 1; i++)
        {
            float t = i / (float)(segments - 1);
            Vector3 pointOnLine = Vector3.Lerp(transform.position, targetCore.position, t);

            
            pointOnLine += Random.insideUnitSphere * jitterAmount;

            line.SetPosition(i, pointOnLine);
        }

       
        line.SetPosition(segments - 1, targetCore.position);
    }
}
