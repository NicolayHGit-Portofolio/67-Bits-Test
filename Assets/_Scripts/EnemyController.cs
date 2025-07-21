using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private Rigidbody _rigidbody;
    private Collider _collider;

    public bool Alive = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    public void GetPunched()
    {
        Alive= false;
        _animator.enabled = false;
    }

    public void EnableKinematic()
    {
        _rigidbody.isKinematic = true;
        _collider.enabled = false;

        foreach (var rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }

    public void DisableKinematic()
    {
        _rigidbody.isKinematic = false;

        foreach (var rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
        }
    }

    public void DestroyBones()
    {
        foreach (var rb in GetComponentsInChildren<Rigidbody>())
        {
            if (rb == GetComponent<Rigidbody>()) continue;
            Destroy(rb);
        }
    }

    public IEnumerator MoveEnemyToLocation(Transform last, Vector3 targetMod, float duration, float arcHeight = 1.5f, bool destroyOnEnd = false)
    {
        float elapsed = 0f;
        Vector3 start = transform.position;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            Vector3 linearPos = Vector3.Lerp(start, last.transform.position + targetMod, t);

            float height = Mathf.Sin(t * Mathf.PI) * arcHeight;
            linearPos.y += height;

            transform.position = linearPos;

            yield return null;
        }

        transform.position = last.transform.position + targetMod;

        yield return null;
        if(destroyOnEnd) Destroy(gameObject);
    }
}
