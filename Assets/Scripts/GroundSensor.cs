using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    public GroundSensor groundSensor;
    public Transform _sensorPosition;
    public Vector2 _sensorSize = new Vector2(0.5f, 0.5f);
    public bool _alreaduLanded = true;

    void Awake()
    {
        groundSensor = GetComponentInChildren<GroundSensor>();
    }
    
    void Update()
    {
        isGrounded(); 
    }

    bool isGrounded()
    {
        Collider2D[] ground = Physics2D.OverlapBoxAll(_sensorPosition.position, _sensorSize, 0);
        foreach (Collider2D item in ground)
        {
            if (item.gameObject.layer == 3)
            {
                return true;
            }
        }
        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_sensorPosition.position, _sensorSize);
    }
}
