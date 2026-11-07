using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

// Attach this to the empty "BladeBase" object (the parent of the Blade cylinder).
// Press Space to collapse / extend the blade.
[RequireComponent(typeof(AudioSource))]
public class SaberScript : MonoBehaviour
{
    // 1. Extend / collapse speed (smaller = faster)
    [Range(0.01f, 1f)]
    public float extendSpeed = 0.1f;

    // Reference to the blade cylinder (drag "Blade" here in the Inspector)
    public GameObject blade;

    // Part 5: sound effects (optional; leave empty to skip)
    public AudioClip powerOnSound;
    public AudioClip powerOffSound;
    public AudioClip humSound;          // looping hum while the blade is on
    [Range(0f, 1f)] public float humVolume = 0.4f;

    // 2. On/off state
    private bool weaponActive;

    // 3. Minimum scale
    private const float SCALE_MIN = 0f;

    // 4. Maximum scale (set from the starting scale)
    private float scaleMax;

    // 5. Interpolation value: how much to scale per second
    private float extendDelta;

    // 6. Current scale along local y
    private float scaleCurrent;

    // 7. Original local x and z scale
    private float localScaleX;
    private float localScaleZ;

    private AudioSource sfxSource;      // one-shot sounds (on/off)
    private AudioSource humSource;      // looping hum

    void Start()
    {
        localScaleX = transform.localScale.x;
        localScaleZ = transform.localScale.z;

        scaleMax = transform.localScale.y;
        scaleCurrent = scaleMax;              // start fully extended

        extendDelta = scaleMax / extendSpeed;
        weaponActive = true;                  // start in the "on" state

        // Audio setup
        sfxSource = GetComponent<AudioSource>();
        sfxSource.playOnAwake = false;

        humSource = gameObject.AddComponent<AudioSource>();
        humSource.clip = humSound;
        humSource.loop = true;
        humSource.playOnAwake = false;
        humSource.volume = humVolume;
        if (humSound != null) humSource.Play();
    }

    void Update()
    {
        if (SpacePressed())
        {
            // Play the matching sound
            AudioClip clip = weaponActive ? powerOffSound : powerOnSound;
            if (clip != null) sfxSource.PlayOneShot(clip);

            // Flip direction: if on, shrink (negative); if off, grow (positive)
            extendDelta = weaponActive ? -Mathf.Abs(extendDelta)
                                       : Mathf.Abs(extendDelta);
        }

        // Apply the interpolation value over time
        scaleCurrent += extendDelta * Time.deltaTime;
        scaleCurrent = Mathf.Clamp(scaleCurrent, SCALE_MIN, scaleMax);

        // Scale only along the local y axis
        transform.localScale = new Vector3(localScaleX, scaleCurrent, localScaleZ);

        // Update state
        weaponActive = scaleCurrent > 0;

        // Hum volume follows blade length (fades out as it collapses)
        if (humSound != null)
            humSource.volume = humVolume * (scaleCurrent / scaleMax);

        // Hide the blade completely when collapsed (removes leftover artifacts)
        if (weaponActive && !blade.activeSelf)
        {
            blade.SetActive(true);
        }
        else if (!weaponActive && blade.activeSelf)
        {
            blade.SetActive(false);
        }
    }

    // Works with both Unity's new Input System and the old Input Manager
    private bool SpacePressed()
    {
#if ENABLE_INPUT_SYSTEM
        return Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame;
#else
        return Input.GetKeyDown(KeyCode.Space);
#endif
    }
}