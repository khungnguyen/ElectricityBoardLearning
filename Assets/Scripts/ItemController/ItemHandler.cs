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

    private float rotationSpeed = 500f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = new Vector3(Input.GetAxisRaw("Mouse Y"), -Input.GetAxisRaw("Mouse X"), 0);
            Vector3 velocity = direction * rotationSpeed * Time.deltaTime;
            Vector3 newRotation = mainModel.rotation.eulerAngles + velocity;
            Debug.Log(velocity);
            mainModel.rotation = Quaternion.Euler(newRotation);

        }


    }
}
