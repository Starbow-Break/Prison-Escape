using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerItemPickUp : MonoBehaviour
{
    [SerializeField] private LayerMask pickableLayerMask;
    [SerializeField] private TextMeshProUGUI pickUpUIText;
    [SerializeField] private AimUI aimUI;
    [SerializeField, Min(1)] private float hitRange = 3.0f;
    [SerializeField] private Transform pickUpParent;
    [SerializeField] private InputActionReference interactionInput, dropInput;

    private RaycastHit hit;
    public GameObject inHandItem { get; private set; }

    private void Start()
    {
        interactionInput.action.performed += PickUp;
        dropInput.action.performed += Drop;
    }

    private void Update()
    {
        // UI 및 하이라이트 초기화
        if(hit.collider != null)
        {
            hit.collider.GetComponent<Highlight>()?.SetHighlight(false);
            pickUpUIText.color = new Color(
                pickUpUIText.color.r,
                pickUpUIText.color.g,
                pickUpUIText.color.b,
                0.0f
            );
            aimUI.SetBigger(false);
        }

        // 이미 아이템이 손에 들려 있다면 아이템을 감지하는 로직은 건너 뛴다.
        if(inHandItem != null)
        {
            return;
        }

        // 에임에 들어온 아이템 감지
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        Debug.DrawRay(ray.origin, ray.direction * hitRange, Color.red);
        
        if(Physics.Raycast(
            ray,
            out hit, 
            hitRange, 
            pickableLayerMask
        ))
        {
            hit.collider.GetComponent<Highlight>()?.SetHighlight(true);
            pickUpUIText.color = new Color(
                pickUpUIText.color.r,
                pickUpUIText.color.g,
                pickUpUIText.color.b,
                1.0f
            );
            aimUI.SetBigger(true);
        }
    }
    
    // 아이템 집기
    private void PickUp(InputAction.CallbackContext context)
    {
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            IPickable pickable = hit.collider.GetComponent<IPickable>();
            if(pickable != null)
            {
                inHandItem = pickable.PickUp();
                inHandItem.transform.SetParent(pickUpParent.transform, false);
            }
        }
    }

    private void Drop(InputAction.CallbackContext context)
    {
        if (inHandItem != null)
        {
            Rigidbody rb = inHandItem.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.isKinematic = false;
            }

            inHandItem.transform.SetParent(null);
            inHandItem = null;
        }
    }
}
