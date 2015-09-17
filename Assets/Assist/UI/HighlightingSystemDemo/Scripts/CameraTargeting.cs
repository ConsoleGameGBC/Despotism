using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraTargeting : MonoBehaviour
{
	public Vector3 defaultPosition;
	public Vector3 zoomInPosition;
	public float speed = 1.0F;
	private float startTime;
	private float journeyLength;
	private bool zoomIn = false;
	// Which layers targeting ray must hit (-1 = everything)
	public LayerMask targetingLayerMask = -1;
	
	// Targeting ray length
	private float targetingRayLength = Mathf.Infinity;
	
	// Camera component reference
	private Camera cam;

	void Start()
	{
		startTime = Time.time;
		journeyLength = Vector3.Distance(defaultPosition, zoomInPosition);
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
			Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
			
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
					Debug.Log (targetTransform.name);
					if(zoomIn != true)
						zoomIn = true;
					else
						zoomIn = false;
					while(zoomIn == true && transform.position != endMarker.position)
					{
						float distCovered = (Time.time - startTime) * speed;
						float fracJourney = distCovered / journeyLength;
						transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
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
