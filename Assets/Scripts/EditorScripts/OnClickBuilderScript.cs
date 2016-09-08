using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnClickBuilderScript : MonoBehaviour {

    private GameObject[] prefabs;
    private Dropdown dropdown;

    private void Start()
    {
        prefabs = Resources.LoadAll<GameObject>("Prefabs");
        dropdown = FindObjectOfType<Dropdown>();
        dropdown.options.Clear();

        for (int i = 0; i < prefabs.Length; i++)
        {
            dropdown.options.Add(new Dropdown.OptionData(prefabs[i].name));
        }
        dropdown.captionText.text = prefabs[dropdown.value].name;
    }

    private void Update()
    {
        Build();
    }

    private void Build()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit hitInfo;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)), out hitInfo))
            {
                Instantiate(prefabs[dropdown.value], hitInfo.point, Quaternion.identity);
            }
        }
    }
}
