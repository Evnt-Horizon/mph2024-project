using DefaultNamespace.models;
using UnityEngine;

public class SpaceshipEntity : Entity
{
    void Start()
    {
    }
    
    void Update()
    {
    }
    
    public void setScale(float scale)
    {
        transform.localScale = new Vector3(
            scale,
            scale,
            scale
        );
    }
}
