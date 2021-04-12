using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    // 밑에 3조건이 모두 만족해야 Collision 확인 가능
    // 1) 나한테 RigidBody 있어야 한다. (IsKinematic: off)
    // 2) 나한테 Collider 있어야 한다. (IsTrigger: off)
    // 3) 상대한테 Collider 있어야 한다. (IsTrigger: off)
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision!!");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger!!");
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
