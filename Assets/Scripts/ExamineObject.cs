using UnityEngine;
using System.Collections;

public class ExamineObject : MonoBehaviour {

	private bool isExamining = false;
	private bool endingExamine = false;
	private bool startingExamine = false;
	private Vector3 originalPosition;
	private Quaternion originalRotation;
	private Vector3 newPosition;
	private Quaternion newRotation;
	private Vector3 originalMousePosition;
	private Vector3 velocity = Vector3.zero;
	private float startTime = 0f;
	//private Quaternion originalFacingX;
	//private Quaternion originalFacingY;

	private MouseLook mouseLookX;
	private MouseLook mouseLookY;
	private CharacterMotor playerMovement;
	private GameObject examinedObject;

	void Awake()
	{
		//originalPosition = new Vector3(0,0,0);
		//originalRotation = new Quaternion(0,0,0,0);
		mouseLookY = transform.GetComponent<MouseLook>();
		mouseLookX = transform.parent.GetComponent<MouseLook>();
		playerMovement = transform.parent.GetComponent<CharacterMotor>();
	}

	void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			if(!isExamining && !startingExamine)
			{
				TryExamining();
			}
			else if(!endingExamine)
			{
				endingExamine = true;
				startTime = Time.time;
				newPosition = examinedObject.transform.position;
				newRotation = examinedObject.transform.rotation;
				mouseLookX.enabled = false;
				mouseLookY.enabled = false;
				//playerMovement.enabled = true;
			}
		}

		if(endingExamine)
		{
			if (Time.time - startTime <= 1.0f)
			{
				//transform.LookAt(examinedObject.transform);
				//transform.parent.LookAt(examinedObject.transform);
				examinedObject.transform.rotation = Quaternion.Slerp(newRotation, originalRotation, Time.time - startTime);
				//examinedObject.transform.position = Vector3.Lerp(examinedObject.transform.position, originalPosition, Time.time * 0.1f);
				examinedObject.transform.position = Vector3.Slerp(newPosition, originalPosition, Time.time - startTime);
				//examinedObject.transform.rotation = originalRotation;
				//mouseLookY.transform.rotation = originalFacingY;
				//mouseLookX.transform.rotation = originalFacingX;
			}
			else
			{
				endingExamine = false;
				isExamining = false;
				examinedObject.transform.rigidbody.useGravity = true;
				playerMovement.enabled = true;
				mouseLookX.enabled = true;
				mouseLookY.enabled = true;
				mouseLookX.objectToRotate = transform.parent;
				mouseLookY.objectToRotate = transform;
			}
		}

		if(startingExamine)
		{
			if (Time.time - startTime <= 1.0f)
			{
				examinedObject.transform.position = Vector3.Slerp(originalPosition, originalPosition + (examinedObject.transform.up/2), Time.time - startTime);
				//transform.LookAt(examinedObject.transform.position);
				mouseLookX.enabled = false;
				mouseLookY.enabled = false;
			}
			else
			{
				isExamining = true;
				startingExamine = false;
				mouseLookX.enabled = true;
				mouseLookY.enabled = true;
				mouseLookY.objectToRotate = examinedObject.transform;
				mouseLookX.objectToRotate = examinedObject.transform;
			}
		}
	}

	void TryExamining()
	{
		//int layerMask = 1 << 8;
		//layerMask = ~layerMask;
		RaycastHit hit;
		if (Physics.Raycast(transform.position + transform.forward/2, transform.TransformDirection (Vector3.forward), out hit, 2))
		{
			if(hit.transform.tag == "Examinable")
			{
				startingExamine = true;
				playerMovement.enabled = false;
				examinedObject = hit.transform.gameObject;
				originalPosition = examinedObject.transform.position;
				originalRotation = examinedObject.transform.rotation;
				examinedObject.transform.rigidbody.useGravity = false;
				startTime = Time.time;

				//originalFacingX = mouseLookX.transform.rotation;
				//originalFacingY = mouseLookY.transform.rotation;
			}
		}
	}

	void StopExamining()
	{
		playerMovement.enabled = true;
		examinedObject.transform.rigidbody.useGravity = true;
		isExamining = false;
	}
}
