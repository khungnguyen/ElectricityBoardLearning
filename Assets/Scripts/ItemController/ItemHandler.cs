using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemHandler : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Transform mainModel;

    [SerializeField]
    private TMP_Text itemTitle;

    [SerializeField]
    private TMP_Text itemDescription;

    public string objectTag="UI_MouseDetect";

    private float rotationSpeed = 1000f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && Utils.IsPointerOverUI(objectTag))
        {
            Vector3 direction = new Vector3(Input.GetAxisRaw("Mouse Y"), -Input.GetAxisRaw("Mouse X"), 0);
            Vector3 velocity = direction * rotationSpeed * Time.deltaTime;
            mainModel.Rotate(velocity);
           
        }
    }

}
