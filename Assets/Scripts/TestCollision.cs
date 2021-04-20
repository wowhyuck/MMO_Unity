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
        // Local <-> World <-> (Viewport <-> Screen) (화면)

        // Screen
        //Debug.Log(Input.mousePosition);         

        // Viewport
        //Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition));

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
            }
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        //    Vector3 dir = mousePos - Camera.main.transform.position;
        //    dir = dir.normalized;

        //    Debug.DrawRay(Camera.main.transform.position, dir * 100.0f, Color.red, 1.0f);

        //    RaycastHit hit;
        //    if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 100.0f))
        //    {
        //        Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
        //    }
        //}
    }
}
