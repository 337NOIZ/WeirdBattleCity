using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 캐릭터 오브젝트나 Enemy오브젝트에 컴퍼넌트로 붙여서 사용
// Rigidbody의 Freeze기능을 사용할경우는 추가적으로 수정이 필요함
// 필요한 경우 스크립트를 수정하여 사용가능
public class Active_Ragdoll : MonoBehaviour
{
    private void Start()
    {
        setRigidbodyState(true);
        setColliderState(false);
    }

    private void setRigidbodyState(bool State)
    {
        // 자신을 제외한 자식객채들의 강채의 iskinematic의 상태를 State의 상태로 변경한다.
        Rigidbody[] rigidbodies = GetComponentInChildren<Rigidbody[]>();

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = State;
        }

        GetComponent<Rigidbody>().isKinematic = !State;
    }

    private void setColliderState(bool State)
    {
        // 자신을 제외한 자식객채들의 콜라이더 컴퍼넌트의 상태를 State의 상태로 변경한다.(켜기/끄기)
        Collider[] colliders = GetComponent<Collider[]>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = State;
        }
        GetComponent<Collider>().enabled = !State;
    }

    // 이 함수를 오브젝트가 죽을때 호출하여 사용하면 랙돌상태로 만들수있다.
    public void Actove_Ragdoll()
    {
        // 함수가 호출되면 애니메이터 컴퍼넌트를 False로 변경하고 자식객채의 강채와 콜라이더의 상태를 변경한다.
        GetComponent<Animator>().enabled = false;
        setColliderState(true);
        setRigidbodyState(false);

        // 오브젝트를 5초뒤 삭재한다.(현제는 비활성화.)
        // 활성화 하여 사용가능
        //Destroy(gameObject, 5f);
    }
}
