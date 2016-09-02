using UnityEngine;
using System.Collections;
using System;


[Serializable]
[ExecuteInEditMode]
public class ObjectData : MonoBehaviour
{

    public int id;
    [SerializeField]
    private string _tag;
    [SerializeField]
    private string _name;
    [SerializeField]
    private Vector3 _position;
    [SerializeField]
    private Vector3 _localscale;
    [SerializeField]
    private Quaternion _rotation;

    public enum Type { Activatable, Interactable, Jumpable, Static }

    public Type _type;

    public void Awake()
    {
        _tag = tag;
        _name = gameObject.name;
        _position = transform.position;
        _localscale = transform.localScale;
        _rotation = transform.rotation;
    }

    private void Update()
    {
        _tag = tag;
        _name = gameObject.name;
        _position = transform.position;
        _localscale = transform.localScale;
        _rotation = transform.rotation;
    }
}
