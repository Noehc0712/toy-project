using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerInteraction : MonoBehaviour
{
    [Header("상호작용 설정")]
    public float interactRange = 3f;
    public LayerMask interactLayer;
    public Transform holdPoint;
    [SerializeField] private Camera cam;

    [Header("획득 여부")]
    public bool hasFirstItem = false;
    public bool hasSecondItem = false;

    private GameObject heldItem;

    private bool gunAcquired = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
            
        }

        if(Input.GetButtonDown("Fire1") && gunAcquired)
        {
            Fire();
            Debug.Log("총 발사");
        }
    }

    void Fire()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            Debug.Log("3D에서 닿은 물체: " + hit.collider.name);
            if (hit.collider.CompareTag("Enemy"))
            {
                Destroy(hit.collider.gameObject);
                Debug.Log("적이 파괴되었습니다.");

            }
        }
    }
    void TryInteract()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        // 레이캐스트에 무언가 감지되었다면
        if (Physics.Raycast(ray, out hit, interactRange, interactLayer))
        {
            GameObject target = hit.collider.gameObject;

            // 아이템 잡기
            if (heldItem == null && target.CompareTag("Item"))
            {
                GrabItem(target);
                CheckItem(target);
                return;
            }

            // 문 텔레포트
            if (target.CompareTag("Door"))
            {
                DoorTeleport door = target.GetComponent<DoorTeleport>();
                if (door != null)
                {
                    Transform tpTarget = door.GetTarget(transform.position);
                    if (tpTarget != null)
                    {
                        transform.position = tpTarget.position;
                    }
                }
                return;
            }
        }
        // 레이캐스트에 아무것도 감지되지 않았고, 손에 아이템을 들고 있다면
        else
        {
            if (heldItem != null)
            {
                DropItem();
                CheckItem();
            }
        }
    }

    void CheckItem(GameObject target)
    {
        if (target == GameObject.Find("GUN")) 
        {
            gunAcquired = true;
            Debug.Log("총 획득");
        }
      
    }
    void CheckItem()
    {
        gunAcquired = false;
        Debug.Log("총 없음");
    }

    void GrabItem(GameObject item)
    {
        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        item.transform.SetParent(holdPoint);
        item.transform.localPosition = Vector3.zero;

        heldItem = item;

        if (item.name == "firstItem")
        {
            hasFirstItem = true;
        }
        else if (item.name == "secondItem")
        {
            hasSecondItem = true;
        }
    }
    void DropItem()
    {
        // 들고 있던 아이템의 상태 플래그를 false로 변경
        if (heldItem.name == "firstItem") hasFirstItem = false;
        else if (heldItem.name == "secondItem") hasSecondItem = false;

        // 부모-자식 관계를 해제해서 플레이어를 따라다니지 않게 함
        heldItem.transform.SetParent(null);
        
        // 물리 효과를 다시 활성화
        Rigidbody rb = heldItem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            // (선택) 살짝 앞으로 던지는 효과
            rb.AddForce(cam.transform.forward * 2f, ForceMode.Impulse);
        }

        // '들고 있는 아이템' 변수를 비워서 다른 물건을 들 수 있게 함
        heldItem = null;
        Debug.Log("아이템을 내려놓았습니다.");
    }
}