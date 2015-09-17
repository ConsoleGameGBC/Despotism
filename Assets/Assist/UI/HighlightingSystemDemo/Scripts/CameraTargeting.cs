using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(Animator))]

public class CameraTargeting : MonoBehaviour
{
	private float startTime;
	private float journeyLength;
	private bool zoomIn = false;
	// Which layers targeting ray must hit (-1 = everything)
	public LayerMask targetingLayerMask = -1;
	public Animator animator;
	// Targeting ray length
	private float targetingRayLength = Mathf.Infinity;
	
	// Camera component reference
	private Camera cam;

	void Start()
	{
		startTime = Time.time;
		animator = GetComponent<Animator>();
	}

	void Awake()
	{
		cam = GetComponent<Camera>();
	}
	
	void Update()
	{
		TargetingRaycast();
	}
	
	public void TargetingRaycast()
	{
		// Current mouse position on screen
		Vector3 mp = Input.mousePosition;
		
		// Current target object transform component
		Transform targetTransform = null;
		
		// If camera component is available
		if (cam != null)
		{
			RaycastHit hitInfo;
			
			// Create a ray from mouse coords
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			
			// Targeting raycast
			if (Physics.Raycast(ray.origin, ray.direction, out hitInfo, targetingRayLength, targetingLayerMask.value))
			{
				// Cache what we've hit
				targetTransform = hitInfo.collider.transform;
			}
		}
		
		// If we've hit an object during raycast
		if (targetTransform != null)
		{

			// And this object has HighlightableObject component
			HighlightableObject ho = targetTransform.root.GetComponentInChildren<HighlightableObject>();
			if (ho != null)
			{
				if (Input.GetButtonDown("Fire1"))
				{
					if(zoomIn != true)
					{
						zoomIn = true;
						animator.SetBool ("zoomIn MainMenu" ,zoomIn);
					}
					else
					{
						zoomIn = false;
						animator.SetBool ("zoomIn MainMenu" ,zoomIn);
					}

				}
				ho.On(Color.yellow);
			}
		}
	}
	
	void OnGUI()
	{

	}
}
