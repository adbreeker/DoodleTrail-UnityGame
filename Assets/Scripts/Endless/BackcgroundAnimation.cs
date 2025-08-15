using UnityEngine;

public class BackcgroundAnimation : MonoBehaviour
{
    [SerializeField] Transform _player;
    public float multiplier = 1.0f;

    Vector3 _startingPos;
    Material _backgroundMaterial;
    private void Start()
    {
        _startingPos = _player.position;
        _backgroundMaterial = GetComponent<Renderer>().material;
    }
    void Update()
    {
        if(_player != null)
        {
            Vector3 offset = _player.position - _startingPos;
            offset *= multiplier;
            _backgroundMaterial.mainTextureOffset = new Vector2(offset.x, offset.z);
        }
    }
}
