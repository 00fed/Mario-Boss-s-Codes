using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SideScrolling : MonoBehaviour
{
    private new Camera camera;
    private Transform player;
    private Luigi luigi;
    public float groundHeight = -3f;
    public float height = 6.5f;
    public float undergroundHeight = -9.5f;
    public float undergroundThreshold = -2f;
    public bool ground;
    public bool underground;
    public bool boss;
    public BoxCollider2D bc;

   
   
    private void Awake()
    {
        camera = GetComponent<Camera>();
        player = GameObject.FindWithTag("Player").transform;
        luigi = GameObject.FindWithTag("Luigi").GetComponent<Luigi>();
        ground = true;
        underground = false;
         
        
    }

    private void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;
        if (!luigi.stage)
        {
            cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x);
        }
        else{
            return;
        }
        
        transform.position = cameraPosition;
        
        
        

    }

    public void SetUnderground()
    {
        underground = true;
        ground = false;
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = underground ? undergroundHeight : height;
        transform.position = cameraPosition;
        

        
    }
    public void SetGround()
    {
        ground = true;
        underground = false;
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = ground ? groundHeight : height;
        transform.position = cameraPosition;
       
        
    }
    public void SetBoss() {
        

        Camera.main.orthographicSize = 8f;
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = -13.23f;
        cameraPosition.x = 114.7f;
        transform.position = cameraPosition;

        
        
    }
    public void ResetBoss() {
        Camera.main.orthographicSize = 7f;
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = -14.2f;
        cameraPosition.x = player.position.x;
        transform.position = cameraPosition;
    }
    





}