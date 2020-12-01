using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    internal static PlayerController instance = null;
    [SerializeField] internal SkinnedMeshRenderer playerMeshRenderer = null;
    internal static Direction direction = Direction.None;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] internal TrackManager activeTrackManager = null;
    [SerializeField] private float scoreIncTime = 10f;
    private MeshRenderer activeGate = null;
    internal Animator animator;
    internal bool isGameOver = false;
    internal bool isGameStarted = false;

    // Start is called before the first frame update
    private void Awake()
    {
        
        animator = GetComponent<Animator> ();
        if(instance == null) {
            instance = this;
        }else{
            Destroy(this);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(isGameStarted) {
            if(direction == Direction.Left)
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            else if(direction == Direction.Right)
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Gate") {
            GameObject otherGameObject = other.gameObject;
            Material otherMaterial = otherGameObject.GetComponent<MeshRenderer>().material;
            Vector3 otherPosition = otherGameObject.transform.position;
            otherGameObject.transform.parent.transform.parent.gameObject.GetComponent<TrackManager>().BurstGate(otherPosition, otherMaterial);
            activeGate = otherGameObject.GetComponent<MeshRenderer>();
            activeGate.enabled = false;
            GameUIHandler.instance.gameScore += (int)scoreIncTime;
        } else if(other.gameObject.tag == "Track") {
            activeTrackManager = other.gameObject.GetComponent<TrackManager>();
            activeTrackManager.ColorChanger();
        } else if(other.gameObject.tag == "Wall" || other.gameObject.tag == "Border") {
            moveSpeed = 0f;
            animator.SetInteger("Index", Random.Range(1, 5));
            isGameOver = true;
            GameUIHandler.instance.GameOver();
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Track") {
            activeGate.enabled = true;
        }
    }
}
