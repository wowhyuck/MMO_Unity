using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    // collision과 trigger에 대해 확실한 건 아래 주소에서 collision action matrix 참조 (세로: 나, 가로: 상대)
    // https://docs.unity3d.com/2019.4/Documentation/Manual/CollidersOverview.html

    // 밑에 3조건이 모두 만족해야 Collision 확인 가능
    // 1) 나한테 RigidBody 있어야 한다. (IsKinematic: off)
    // 2) 나한테 Collider 있어야 한다. (IsTrigger: off)
    // 3) 상대한테 Collider 있어야 한다. (IsTrigger: off)
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision @ {collision.gameObject.name} !!");
    }

    // 밑에 3조건이 모두 만족해야 Trigger 확인 가능
    // 1) 둘다 collider가 있어야 한다
    // 2) 둘 중 하나는 IsTrigger : On
    // 3) 둘 중 하나는 RigidBody가 있어야 한다.
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger @ {other.gameObject.name} !!");
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
