using UnityEngine;
using System.Collections;

public class ExtendShape 
{
	public class Arc 
	{
		public Vector3	forward;
		public Material material;
		
		public float angle = 40.0f;
		public float radius_min = 4.0f;
	    public float radius_max = 7.0f;
		public Vector3 center = Vector3.zero;
		
	    public int quality = 15;
	    
		
		private Mesh mesh;
	
	    public Arc()
	    {
	        mesh = new Mesh();
	        mesh.vertices = new Vector3[4 * quality];   // Could be of size [2 * quality + 2] if circle segment is continuous
	        mesh.triangles = new int[3 * 2 * quality];
	
	        Vector3[] normals = new Vector3[4 * quality];
	        Vector2[] uv = new Vector2[4 * quality];
	
	        for (int i = 0; i < uv.Length; i++)
	            uv[i] = new Vector2(0, 0);
	        for (int i = 0; i < normals.Length; i++)
	            normals[i] = new Vector3(0, 1, 0);
	
	        mesh.uv = uv;
	        mesh.normals = normals;
			
			Build();
	    }
	
		public void Build()
		{
			float angle_start 	= -angle * 0.5f;
	        float angle_end 	= angle * 0.5f;
	        float angle_delta 	= (angle_end - angle_start) / quality;
	
	        float angle_curr = angle_start;
	        float angle_next = angle_start + angle_delta;
	
	        Vector3 pos_curr_min = Vector3.zero;
	        Vector3 pos_curr_max = Vector3.zero;
	
	        Vector3 pos_next_min = Vector3.zero;
	        Vector3 pos_next_max = Vector3.zero;
	
	        Vector3[] vertices = new Vector3[4 * quality];   // Could be of size [2 * quality + 2] if circle segment is continuous
	        int[] triangles = new int[3 * 2 * quality];
	
	        for (int i = 0; i < quality; i++)
	        {
	            Vector3 sphere_curr = new Vector3(
	            Mathf.Sin(Mathf.Deg2Rad * (angle_curr)), 0,   // Left handed CW
	            Mathf.Cos(Mathf.Deg2Rad * (angle_curr)));
	
	            Vector3 sphere_next = new Vector3(
	            Mathf.Sin(Mathf.Deg2Rad * (angle_next)), 0,
	            Mathf.Cos(Mathf.Deg2Rad * (angle_next)));
	
	            pos_curr_min = sphere_curr * radius_min;
	            pos_curr_max = sphere_curr * radius_max;
	
	            pos_next_min = sphere_next * radius_min;
	            pos_next_max = sphere_next * radius_max;
	
	            int a = 4 * i;
	            int b = 4 * i + 1;
	            int c = 4 * i + 2;
	            int d = 4 * i + 3;
	
	            vertices[a] = pos_curr_min; 
	            vertices[b] = pos_curr_max;
	            vertices[c] = pos_next_max;
	            vertices[d] = pos_next_min;
	
	            triangles[6 * i] = a;       // Triangle1: abc
	            triangles[6 * i + 1] = b;  
	            triangles[6 * i + 2] = c;
	            triangles[6 * i + 3] = c;   // Triangle2: cda
	            triangles[6 * i + 4] = d;
	            triangles[6 * i + 5] = a;
	
	            angle_curr += angle_delta;
	            angle_next += angle_delta;
	
	        }
	
	        mesh.vertices = vertices;
	        mesh.triangles = triangles;
		}
		
	    public void Draw()
	    {
			Build();
			
			float fAngle = Vector3.Angle(forward, Vector3.forward);
			float cw = Vector3.Cross( Vector3.forward, forward ).normalized.y;
			Quaternion rotation = Quaternion.AngleAxis( fAngle * cw, Vector3.up );
			
	        //Graphics.DrawMeshNow(mesh, center, rotation);
			Graphics.DrawMesh(mesh, center, rotation, material, 0);
	    }
	}
}