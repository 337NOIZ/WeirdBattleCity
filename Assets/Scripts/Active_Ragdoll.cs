using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ĳ���� ������Ʈ�� Enemy������Ʈ�� ���۳�Ʈ�� �ٿ��� ���
// Rigidbody�� Freeze����� ����Ұ��� �߰������� ������ �ʿ���
// �ʿ��� ��� ��ũ��Ʈ�� �����Ͽ� ��밡��
public class Active_Ragdoll : MonoBehaviour
{
    private void Start()
    {
        setRigidbodyState(true);
        setColliderState(false);
    }

    private void setRigidbodyState(bool State)
    {
        // �ڽ��� ������ �ڽİ�ä���� ��ä�� iskinematic�� ���¸� State�� ���·� �����Ѵ�.
        Rigidbody[] rigidbodies = GetComponentInChildren<Rigidbody[]>();

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = State;
        }

        GetComponent<Rigidbody>().isKinematic = !State;
    }

    private void setColliderState(bool State)
    {
        // �ڽ��� ������ �ڽİ�ä���� �ݶ��̴� ���۳�Ʈ�� ���¸� State�� ���·� �����Ѵ�.(�ѱ�/����)
        Collider[] colliders = GetComponent<Collider[]>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = State;
        }
        GetComponent<Collider>().enabled = !State;
    }

    // �� �Լ��� ������Ʈ�� ������ ȣ���Ͽ� ����ϸ� �������·� ������ִ�.
    public void Actove_Ragdoll()
    {
        // �Լ��� ȣ��Ǹ� �ִϸ����� ���۳�Ʈ�� False�� �����ϰ� �ڽİ�ä�� ��ä�� �ݶ��̴��� ���¸� �����Ѵ�.
        GetComponent<Animator>().enabled = false;
        setColliderState(true);
        setRigidbodyState(false);

        // ������Ʈ�� 5�ʵ� �����Ѵ�.(������ ��Ȱ��ȭ.)
        // Ȱ��ȭ �Ͽ� ��밡��
        //Destroy(gameObject, 5f);
    }
}
