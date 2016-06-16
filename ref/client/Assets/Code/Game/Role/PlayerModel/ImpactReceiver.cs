using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ImpactReceiver : MonoBehaviour 
{
	public float mass = 3.0f;

	private Vector3 impact = Vector3.zero;
	private CharacterController character;

	void Start () 
	{
		character = GetComponent<CharacterController>();
	}
	
	void Update () 
	{
		if (impact.magnitude > 0.2f)
			character.Move(impact * Time.deltaTime);
		impact = Vector3.Lerp(impact, Vector3.zero, 5*Time.deltaTime);
	}

	public void AddImpact(Vector3 dir, float force)
	{
		if( impact.magnitude > 0.2f )
			return;

		dir.Normalize();
		if (dir.y < 0) 
			dir.y = -dir.y; // reflect down force on the ground
		impact += dir.normalized * force / mass;
	}

}