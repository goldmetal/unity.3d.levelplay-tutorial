using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField]
    Vector3 shotDir;

    Rigidbody body;
    Animator anim;
    AudioSource sfx;
    WaitForSeconds wait;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        sfx = GetComponent<AudioSource>();
        wait = new WaitForSeconds(2f);
    }

    public void Shot(UnityAction onCompleted)
    {
        anim.Play("surprise");
        body.isKinematic = false;
        body.AddForce(shotDir * 4f, ForceMode.Impulse);
        body.AddTorque(shotDir * -5f, ForceMode.Impulse);
        sfx.Play();

        StartCoroutine(BackRoutine(onCompleted));
    }

    IEnumerator BackRoutine(UnityAction onCompleted)
    {
        yield return wait;
        body.linearVelocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        body.isKinematic = true;
        
        yield return null;
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.right * -20f;
        anim.Play("idle");
        
        onCompleted?.Invoke();
    }

    public bool IsUsed()
    {
        return !body.isKinematic;
    }
}
