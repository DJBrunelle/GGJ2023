using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{

    public List<GameObject> prefabs;
    public GameObject cancelBuildImage;

    private Ray ray;
    private RaycastHit hit;
    private bool building;
    private GameObject newFacility;
    private Vector3 pos;
    private bool validSpot;
    private int prefabIndex;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!building)
        {
            return;
        }
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        pos.z = -5f;
        if(newFacility is not null)
        {
            newFacility.transform.position = pos;
        }
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        validSpot = false;
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.gameObject.tag == "Node")
            {
                //Input.mousePosition = Camera.main.WorldToScreenPoint(hit.collider.transform.position);
                pos = hit.collider.transform.position;
                pos.z = -5f;
                newFacility.transform.position = pos;
                validSpot = true;
            }
        }
        if(validSpot && Input.GetMouseButtonDown(0))
        {
            Debug.Log("BUILD IT" + hit);
            pos = hit.collider.transform.position;
            pos.z = -5f;
            newFacility.transform.position = pos;
            newFacility.GetComponent<Facility>().Build(PathType.PERSONAL, 1);
            building = false;
            validSpot = false;
            cancelBuildImage.SetActive(false);
        }
    }

    public void SelectFacility(int selectionIndex)
    {
        prefabIndex = selectionIndex;
    }

    public void StartBuild()
    {
        Facility nf;
        if(building)
        {

            nf = newFacility.GetComponent<Facility>();
            Stats.ReturnFacility(nf.personalEnergyCost, nf.personalMotivationCost, nf.personalTimeCost);
            if(newFacility is not null)
            {
                Destroy(newFacility);
            }
            building = false;
            cancelBuildImage.SetActive(false);
            return;
        }

        nf = prefabs[prefabIndex].GetComponent<Facility>();
        if(Stats.PurchaseFacility(nf.personalEnergyCost, nf.personalMotivationCost, nf.personalTimeCost))
        {
            Debug.Log("Start Build");
            building = true;
            newFacility = Instantiate(prefabs[prefabIndex]);
            cancelBuildImage.SetActive(true);
        }
    }
}
