using UnityEngine;

public class AddForceBomb : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] float _xPower;
    [SerializeField] float _yPower;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(new  Vector2(_xPower, _yPower));
        Destroy(gameObject, 3f);
    }
}
