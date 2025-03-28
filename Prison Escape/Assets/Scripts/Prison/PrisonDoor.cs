using UnityEngine;

public class PrisonDoor : MonoBehaviour, IItemInteractable
{
    // 상호작용을 위해 필요한 아이템
    [SerializeField] private GameObject needItem;
    [SerializeField] private AudioClip failClip;
    [SerializeField] private AudioClip successClip;
    
    private Animator animator;
    private AudioSource audioSource;
    void Awake()
    {
        animator = GetComponentInParent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    
    public void InteractUseItem(GameObject actor, GameObject useItem)
    {
        // 사용한 아이템과 필요한 아이템이 일치하면
        if (needItem != null && useItem == needItem)
        {
            audioSource.PlayOneShot(successClip);
            animator.SetTrigger("Open");
        }
        else
        {
            audioSource.PlayOneShot(failClip);
        }
    }
}
