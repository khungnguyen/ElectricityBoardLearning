using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemHandler : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Transform modelParentHolder;

    [SerializeField]
    private TMP_Text itemTitle;

    [SerializeField]
    private TMP_Text itemDescription;

    [SerializeField]
    private Transform instantiatePoint;

    [SerializeField]
    private LayerMask layerRenderModel;

    public string objectTag = "UI_MouseDetect";

    public float rotationSpeed = 500f;

    public float zoomSpeed = 40;
    private Transform mainModel;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Utils.IsPointerOverUI(objectTag) && mainModel != null)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 direction = new Vector3(Input.GetAxisRaw("Mouse Y"), -Input.GetAxisRaw("Mouse X"), 0);
                Vector3 velocity = direction * rotationSpeed * Time.deltaTime;
                mainModel.Rotate(velocity,Space.World);

            }
            float mouseWheelDirection = Input.mouseScrollDelta.y * zoomSpeed;
            Vector3 target = mainModel.localScale + new Vector3(mouseWheelDirection, mouseWheelDirection, mouseWheelDirection);
            mainModel.localScale = Vector3.Lerp(mainModel.localScale, target, 0.05f);
        }

    }
    public ItemHandler Init(string title, string des, EElectricItem type)
    {
        if (itemTitle != null)
        {
            itemTitle.SetText(title);
        }
        if (itemDescription != null)
        {
            itemDescription.SetText(des);
        }
        GameObject model = ResourceManager.instance.GetElectricItemByType(type);
        if (model != null)
        {
            mainModel = Instantiate(model, instantiatePoint.position, instantiatePoint.rotation).transform;
            mainModel.transform.SetParent(modelParentHolder);
            mainModel.transform.localScale = instantiatePoint.localScale;
            mainModel.gameObject.layer = Mathf.RoundToInt(Mathf.Log(layerRenderModel.value, 2));
        }
        return this;
    }
    public void Show()
    {
        gameObject.SetActive(true);
        GetComponent<BoundInAndOut>()?.PlayBoundEffect();
    }
    public void Hide()
    {
        GetComponent<BoundInAndOut>()?.PlayBoundOutEffect(() =>
        {
            Utils.Log(GetType().Name,"Complete Animation");
            gameObject.SetActive(false);
            Destroy(mainModel?.gameObject);
            mainModel = null;
        });

    }
}

