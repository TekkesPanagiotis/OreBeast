using UnityEngine;

public class GrappleOrbSpin : MonoBehaviour
{
   
    public float spinSpeed = 45f; 

  
    public float hoverHeight = 0.15f; 
    public float hoverSpeed = 2f;     

    private Vector3 startPosition;

    void Start()
    {
        
        startPosition = transform.localPosition;
    }

    void Update()
    {
        
        transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime, Space.World);

       
        Vector3 newPosition = startPosition;
        newPosition.y += Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.localPosition = newPosition;
    }
}
