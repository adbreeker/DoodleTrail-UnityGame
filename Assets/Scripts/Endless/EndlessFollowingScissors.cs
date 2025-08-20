using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessFollowingScissors : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] float followingSpeed = 5f;

    float _offsetX;
    float _stayBoundaryX;

    void Start()
    {
        _offsetX = -(_camera.orthographicSize * _camera.aspect);
        _stayBoundaryX = _offsetX;
        transform.position = new Vector3(_offsetX, 0, 0);

        StartCoroutine(HuntPlayer());
    }

    private void Update()
    {
        _stayBoundaryX = _offsetX + _camera.transform.position.x;

        if (transform.position.x < _stayBoundaryX)
        {
            transform.position = new Vector3(_stayBoundaryX, 0, 0);
        }
    }

    IEnumerator HuntPlayer()
    {
        yield return new WaitForSeconds(5f);

        while(true)
        {
            yield return new WaitForFixedUpdate();
            transform.position += Vector3.right * followingSpeed * Time.fixedDeltaTime;
        }
    }
}
