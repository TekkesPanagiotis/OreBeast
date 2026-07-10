using UnityEngine;

public class PlayerTarget : MonoBehaviour
{
    public static Transform Instance {  get; private set; }

    private void Awake()
    {
        Instance = this.transform;
    }
}
