using UnityEngine;

public class KarateManManager : MonoBehaviour
{
    private Animator _animator;

    public Transform rightHandTransform;
    public Transform rightFootTransform;
    public GameObject hitVFXPrefab;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // left-click
            _animator.SetTrigger("Punch");
        }
        else if (Input.GetMouseButtonDown(1))
        {
            // right-click
            _animator.SetTrigger("Kick");
        }
    }

    public void ShowHandVFX()
    {
        Instantiate(hitVFXPrefab, rightHandTransform);
    }

    public void ShowFootVFX()
    {
        Instantiate(hitVFXPrefab, rightFootTransform);
    }
}
