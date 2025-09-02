using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpenerBehavior : Obstacle
{
    [Header("Sharpener settings:")]
    public float pullingRange = 2.1f;
    public float pullingForce = 5f;
    public float rotatingAngle = -45f;
    public float rotatingAnimationSpeed= 3f;
    public float pullingDuration = 3f;
    public float pullingCooldown = 5f;

    [Header("References:")]
    [SerializeField] GameObject _VFX;
    [SerializeField] CircleCollider2D _pullingCollider;
    [SerializeField] SharpenerGravitationArea _gravitationArea;

    void Start()
    {
        _VFX.transform.localScale = new Vector3(pullingRange, pullingRange, 1);
        _VFX.SetActive(false);

        StartCoroutine(Pulling());
    }

    IEnumerator Pulling()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            float timeElapsed = 0f;
            _VFX.SetActive(true);

            AudioSourceController sound = SoundManager.Instance.PlaySound3D(SoundEnum.EFFECT_VACUUM, transform.position, SoundType.GetType_LoopedSingleScene());
            while (timeElapsed < pullingDuration)
            {
                float effectiveRange = pullingRange * transform.lossyScale.x * 2f;

                foreach (Collider2D collider in _gravitationArea.affectedObjects)
                {
                    Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                    if (rb == null) continue;

                    Vector2 toCenter = (Vector2)transform.position - (Vector2)rb.transform.position;
                    float dist = toCenter.magnitude;

                    float currentRotatingAngel = rotatingAngle * Mathf.Clamp01(1f - (dist/effectiveRange) + 0.5f);
                    currentRotatingAngel *= Mathf.Cos(transform.eulerAngles.y * Mathf.Deg2Rad);


                    Vector2 direction = (Quaternion.Euler(0, 0, currentRotatingAngel) * toCenter.normalized);

                    float pullStrength = Random.Range(0.9f * pullingForce, 1.1f * pullingForce);
                    rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, direction * pullStrength, Mathf.Clamp01(1f - (dist / effectiveRange) + 0.2f));
                }

                _VFX.transform.Rotate(0, 0, rotatingAnimationSpeed * Time.fixedDeltaTime);

                timeElapsed += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            SoundManager.Instance.DestroyAudioSource(sound);

            _VFX.SetActive(false);
            yield return new WaitForSeconds(pullingCooldown);
        }
    }

}
