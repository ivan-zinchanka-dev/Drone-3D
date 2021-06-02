using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DroneSkin : MonoBehaviour
{
    [SerializeField] private string _name = null;
    public string Name { get { return _name; } private set { _name = value; } }

    [SerializeField] private bool _isAvailable = false;

    public bool IsAvailable { get { return _isAvailable; } set { _isAvailable = value; } }

}
