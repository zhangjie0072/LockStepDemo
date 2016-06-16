using UnityEngine;
using System.Collections;

public class CaptainInfoDisplay : MonoBehaviour {


    public float _angleZ = -72.0f;


    public float _rotX = 0.0f;
    public float _rotY = 89.0f;
    public float _rotZ = 0.0f;
    Mesh _mesh;


    private Vector3[] _pointArray = new Vector3[6] 
    {
        //new Vector3(0,-19,0),
        //new Vector3(0,76,0),
        //new Vector3(90,12,0),
        //new Vector3(54,-93,0),
        //new Vector3(-53,-93,0),
        //new Vector3(-86,12,0)


        //new Vector3(0,-12,0),
        //new Vector3(0,71,0),
        //new Vector3(75,12,0),
        //new Vector3(47,-76,0),
        //new Vector3(-47,-78,0),
        //new Vector3(-78,13,0)


        new Vector3(0,-10),
        new Vector3(0,96,0),
        new Vector3(100,23,0),
        new Vector3(61,-96,0),
        new Vector3(-62,-96,0),
        new Vector3(-100,23,0)


    };



    float[] _scaleL = new float[5]
    {
             1.0f,
            1.0f,
            1.0f,
            1.0f,
            1.0f
        };
        
   public void setScale( float f0, float f1, float f2, float f3, float f4)
    {
        _scaleL[0] = f0;
        _scaleL[1] = f1;
        _scaleL[2] = f2;
        _scaleL[3] = f3;
        _scaleL[4] = f4;
        Show();
    }


    void Show()
    {

        Vector3[] vertices = new Vector3[6];
        vertices[0] = _pointArray[0];


        Vector3 Dir = _pointArray[1] - _pointArray[0];
        float distane = Dir.magnitude;
        Vector3 D = Dir.normalized;
        vertices[1] = _pointArray[0] + D * distane * _scaleL[0];

        Dir = _pointArray[2] - _pointArray[0];
        distane = Dir.magnitude;
        D = Dir.normalized;
        vertices[2] = _pointArray[0] + D * distane * _scaleL[1];

        Dir = _pointArray[3] - _pointArray[0];
        distane = Dir.magnitude;
        D = Dir.normalized;
        vertices[3] = _pointArray[0] + D * distane * _scaleL[2];

        Dir = _pointArray[4] - _pointArray[0];
        distane = Dir.magnitude;
        D = Dir.normalized;
        vertices[4] = _pointArray[0] + D * distane * _scaleL[3];

        Dir = _pointArray[5] - _pointArray[0];
        distane = Dir.magnitude;
        D = Dir.normalized;
        vertices[5] = _pointArray[0] + D * distane * _scaleL[4];

        int[] triangles = new int[15];

        for (int i = 0; i < 15; i++)
        {
            triangles[i] = i;

        }

        Vector3[] allVertices = new Vector3[15];
        uint[] vMap = new uint[15] 
        {
            0,1,2,0,2,3,0,3,4,0,4,5,0,5,1
        };
        for (int i = 0; i < 15; i++)
        {
            allVertices[i]=vertices[vMap[i]];
        }

        _mesh.vertices = allVertices;
        _mesh.triangles = triangles;
        Color32[] colors32 = new Color32[15];
        for (int i = 0; i < 15; i++)
        {
            colors32[i] = new Color32(41, 53, 165, 255);
        }

        _mesh.colors32 = colors32;
    
    }



    void Awake()
    {
        MeshFilter meshFilter = transform.GetComponent<MeshFilter>();
        _mesh = meshFilter.mesh;


        MeshRenderer meshRender = transform.GetComponent<MeshRenderer>();

        meshRender.material = new Material(Shader.Find("Toon/Advanced Outline")); 
        meshRender.material.color = new Color32(20, 0, 255, 255);

    }
	// Use this for initialization
	void Start () {



           Show();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
