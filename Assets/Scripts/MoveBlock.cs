using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    public float speed = 5f;
    public Transform move;

    
    // Update is called once per frame
    void Update()
    {
        move.Translate((Vector3.forward * speed) * Time.deltaTime);
    }
}
