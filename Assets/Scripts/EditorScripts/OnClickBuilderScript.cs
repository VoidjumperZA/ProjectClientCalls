using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnClickBuilderScript : MonoBehaviour {

    private GameObject[] _prefabs;
    private Dropdown _dropdown;
    public InputField rotationInput;
    public InputField scaleInput;
    private Vector3 _rotation = new Vector3(0f, 0f, 0f);
    private Vector3 _scale = new Vector3(1f, 1f, 1f);
    private GameObject _previousObject;

    private void Start()
    {
        _prefabs = Resources.LoadAll<GameObject>("Prefabs");
        _dropdown = FindObjectOfType<Dropdown>();
        DropDownListFill();
    }

    private void DropDownListFill()
    {
        _dropdown.options.Clear();

        for (int i = 0; i < _prefabs.Length; i++)
        {
            _dropdown.options.Add(new Dropdown.OptionData(_prefabs[i].name));
        }
        _dropdown.captionText.text = _prefabs[_dropdown.value].name;
    }

    private void Update()
    {
        Build();
        DeleteLastSpawned();
    }

    private void UpdateScaleInputs()
    {
        string[] scales = scaleInput.text.Split(' ');
        _scale = new Vector3(1f, 1f, 1f);
        if (scales.Length < 3) return;

        float[] values = new float[] { 1f, 1f, 1f };

        for (int i = 0; i < scales.Length; i++)
        {
            float.TryParse(scales[i], out values[i]);
        }
        _scale = new Vector3(values[0], values[1], values[2]);
    }

    private void DeleteLastSpawned()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Z))
        {
            if (_previousObject != null)
            {
                Destroy(_previousObject);
            }
        }
    }

    private void UpdateRotationInputs()
    {
        string[] rotations = rotationInput.text.Split(' ');
        _rotation = new Vector3(0f, 0f, 0f);
        if (rotations.Length < 3) return;

        float[] values = new float[] { 1f, 1f, 1f };

        for (int i = 0; i < rotations.Length; i++)
        {
            if (rotations[i] == "random") rotations[i] = Random.Range(0f, 90f).ToString();
            float.TryParse(rotations[i], out values[i]);
        }
        _rotation = new Vector3(values[0], values[1], values[2]);
    }

    private void Build()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateScaleInputs();
            UpdateRotationInputs();
            RaycastHit hitInfo;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)), out hitInfo))
            {
                GameObject go = Instantiate(_prefabs[_dropdown.value], hitInfo.point, Quaternion.Euler(_rotation)) as GameObject;
                _scale = new Vector3(go.transform.localScale.x * _scale.x, go.transform.localScale.y * _scale.y, go.transform.localScale.z * _scale.z);
                go.transform.localScale = _scale;
                _previousObject = go;
            }
        }
    }
}
