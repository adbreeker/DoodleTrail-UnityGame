using UnityEngine;

public class ScissorsBehavior : Obstacle
{
    [Header("Scissors settings")]
    [SerializeField] float _speed = 1f;

    [SerializeField] GameObject _scissorsTop;
    [SerializeField] GameObject _scissorsBot;

    bool _isClosing = true;

    private void Start()
    {
        SoundManager.Instance.PlaySound3D(SoundEnum.EFFECT_SCISSORS, transform.position, true);
    }

    private void FixedUpdate()
    {
        if(_isClosing)
        {
            float currentRotation = _scissorsTop.transform.localRotation.eulerAngles.z;
            currentRotation = Mathf.MoveTowards(currentRotation, 5f, _speed * Time.fixedDeltaTime);

            _scissorsTop.transform.localRotation = Quaternion.Euler(0f, 0f, currentRotation);
            _scissorsBot.transform.localRotation = Quaternion.Euler(0f, 0f, -currentRotation);

            if(currentRotation <= 5f)
            {
                _isClosing = false;
            }
        }
        else
        {
            float currentRotation = _scissorsTop.transform.localRotation.eulerAngles.z;
            currentRotation = Mathf.MoveTowards(currentRotation, 30f, _speed * Time.fixedDeltaTime);

            _scissorsTop.transform.localRotation = Quaternion.Euler(0f, 0f, currentRotation);
            _scissorsBot.transform.localRotation = Quaternion.Euler(0f, 0f, -currentRotation);

            if(currentRotation >= 30f)
            {
                _isClosing = true;
                SoundManager.Instance.PlaySound3D(SoundEnum.EFFECT_SCISSORS, transform.position, true);
            }
        }
    }
}
