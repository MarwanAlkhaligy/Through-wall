using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    [SerializeField] private Transform trackStart = null;
    [SerializeField] private Transform trackEnd = null;
    [SerializeField] private float trackMoveSpeed = 0f;
    [SerializeField] private GameObject wallHolder = null;
    [SerializeField] private ParticleSystem wallBurst = null;
    [SerializeField] private List<Material> materials = new List<Material>();
    [SerializeField] internal TrackManager nextTrackManager = null;

    private Vector3 track_End = Vector3.zero;
    private Vector3 track_Pos = Vector3.zero;
    

    private List<MeshRenderer> wallMeshRenderer = new List<MeshRenderer>();
    // Start is called before the first frame update
    void Start()
    {
        track_End = trackEnd.position;
        track_Pos = transform.position;
        for( int i = 0; i < wallHolder.transform.childCount; i++) {
            wallMeshRenderer.Add(wallHolder.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>());
        }
        if(PlayerController.instance.activeTrackManager == this)
            ColorChanger();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!PlayerController.instance.isGameOver && PlayerController.instance.isGameStarted) {
            transform.Translate(-Vector3.forward * trackMoveSpeed * Time.deltaTime);
            track_Pos = transform.position;
            if(track_End.z >= track_Pos.z) {
                transform.position = new Vector3(0, 0, trackStart.position.z ); //+ Mathf.Abs(track_End.z - track_Pos.z)
            }
        }
    }
    internal void ColorChanger()
    {
        List<Material> temp = new List<Material>();
        foreach(Material m in materials) {
            temp.Add(m);
        }
        int index;
        foreach (MeshRenderer item in wallMeshRenderer)
        {
            index = Random.Range(0, temp.Count);
            item.material = temp[index];
            item.gameObject.tag = "Wall";
            temp.RemoveAt(index);
        }
        GameObject gate = wallMeshRenderer[Random.Range(0, wallMeshRenderer.Count)].gameObject;
        gate.tag = "Gate";
        PlayerController.instance.playerMeshRenderer.material = gate.GetComponent<MeshRenderer>().material;
    }
    internal void BurstGate(Vector3 pos, Material mat)
    {
        wallBurst.transform.position = pos;
         wallBurst.GetComponent<ParticleSystem>().Play();
        wallBurst.GetComponent<ParticleSystemRenderer>().material = mat;
    }
}
 