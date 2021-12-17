using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour {

    [Header("Grab Interactable")]
    [SerializeField] private XRGrabInteractable grabInteractable;

    [Header("Raycasting info")]
    [SerializeField] private Transform raycastOrigin;

    [SerializeField] private LayerMask targetLayer;

    [Space(10f)]
    [Header("Audio SFX")]
    [SerializeField] private AudioClip gunClipFSX;

    private AudioSource gunAudioSourse;

    [Header("Target hit grapfhic")]
    [SerializeField] private GameObject hitGraphic;

    private void OnEnable() => grabInteractable.activated.AddListener(TriggerPulled);

    private void OnDisable() => grabInteractable.activated.RemoveListener(TriggerPulled);

    private void TriggerPulled(ActivateEventArgs arg0) {
        arg0.interactor.GetComponent<XRBaseController>().SendHapticImpulse(.5f, .25f);
        gunAudioSourse.PlayOneShot(gunClipFSX);
        FireRaycastIntoScene();
    }

    private void FireRaycastIntoScene() {
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, targetLayer))
        {
            if (hit.transform.GetComponent<ItargetInterface>() != null)
            {
                hit.transform.GetComponent<ItargetInterface>().TargetShot();
                CreateHitTargetOnTarget(hit.point);
            } else
            {
                Debug.Log("Not Interheriting from interface");
            }
        }
    }

    private void CreateHitTargetOnTarget(Vector3 hitLocation) {
        GameObject hitMaker = Instantiate(hitGraphic, hitLocation, Quaternion.identity);
    }

    // Start is called before the first frame update
    private void Start() {
    }

    // Update is called once per frame
    private void Update() {
    }

    private void Awake() {
        if (TryGetComponent(out AudioSource audio))
            gunAudioSourse = audio;
        else
            gunAudioSourse = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
    }
}