using System.Collections;
using System.Collections.Generic;
using Managers.GameManager;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField]
    private float scrollSpeed;
    [SerializeField]
    private float cornerRadius;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float zoomSpeed;
    private float _cornerRadiusDouble;
    private Camera _mainCamera;
    private Rigidbody cameraRigidBody;
    
    void Start()
    {
        _mainCamera = Camera.main;
        cameraRigidBody = GetComponent<Rigidbody>();
        _cornerRadiusDouble = cornerRadius / 2;
    }

    // Update is called once per frame
    void Update(){
        if (!GameManager.getInstance().canCameraMove) return;

        //_mainCameraTransform.Translate(0, Input.mouseScrollDelta.y * zoomSpeed, 0);
        cameraRigidBody.AddForce(0, Input.mouseScrollDelta.y * -zoomSpeed, 0);
        
        if (Input.GetKey(KeyCode.Mouse1)){
            if(Input.GetAxis("Mouse X")<0){
                transform.Rotate(0,-rotationSpeed,0);
            } else if(Input.GetAxis("Mouse X")>0){
                transform.Rotate(0,rotationSpeed,0);
            }
            
            if(Input.GetAxis("Mouse Y")<0){
                transform.Rotate(rotationSpeed,0,0);
            } else if(Input.GetAxis("Mouse Y")>0){
                transform.Rotate(-rotationSpeed,0,0);
            }

            var rotation = transform.rotation;
            rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, 0);
            transform.rotation = rotation;
            
            if ( Input.GetKey("d") ) {
                if ( Input.mousePosition.x >= Screen.width - _cornerRadiusDouble )
                    //transform.Translate(transform.right * (Time.deltaTime * scrollSpeed), Space.World);
                    cameraRigidBody.AddForce(transform.right * (Time.deltaTime * scrollSpeed));
                //transform.Translate(transform.right * (Time.deltaTime * scrollSpeed));
                cameraRigidBody.AddForce(transform.right * (Time.deltaTime * scrollSpeed));
            }else if ( Input.GetKey("a") ) {
                if ( Input.mousePosition.x <= _cornerRadiusDouble)
                    //transform.Translate(transform.right * (Time.deltaTime * scrollSpeed * -1), Space.World);
                    cameraRigidBody.AddForce(transform.right * (Time.deltaTime * scrollSpeed * -1));
                //transform.Translate(transform.right * (Time.deltaTime * scrollSpeed * -1), Space.World);
                cameraRigidBody.AddForce(transform.right * (Time.deltaTime * scrollSpeed * -1));
            }
            if ( Input.GetKey("w") ) {
                if ( Input.mousePosition.y >= Screen.height - _cornerRadiusDouble )
                    //transform.Translate(transform.forward * (Time.deltaTime * scrollSpeed), Space.World);
                    cameraRigidBody.AddForce(transform.forward * (Time.deltaTime * scrollSpeed));
                //transform.Translate(transform.forward * (Time.deltaTime * scrollSpeed), Space.World);
                cameraRigidBody.AddForce(transform.forward * (Time.deltaTime * scrollSpeed));
            }else if ( Input.GetKey("s")  ) {
                if ( Input.mousePosition.y <= _cornerRadiusDouble )
                    //transform.Translate(transform.forward * (Time.deltaTime * scrollSpeed * -1), Space.World);
                    cameraRigidBody.AddForce(transform.forward * (Time.deltaTime * scrollSpeed * -1));
                //transform.Translate(transform.forward * (Time.deltaTime * scrollSpeed * -1), Space.World);
                cameraRigidBody.AddForce(transform.forward * (Time.deltaTime * scrollSpeed * -1));
            }
            
        }else{
            if ( Input.mousePosition.x >= Screen.width - cornerRadius || Input.GetKey("d") ) {
                if ( Input.mousePosition.x >= Screen.width - _cornerRadiusDouble )
                    //transform.Translate(transform.right * (Time.deltaTime * scrollSpeed), Space.World);
                    cameraRigidBody.AddForce(transform.right * (Time.deltaTime * scrollSpeed));
                //transform.Translate(transform.right * (Time.deltaTime * scrollSpeed));
                cameraRigidBody.AddForce(transform.right * (Time.deltaTime * scrollSpeed));
            }else if ( Input.mousePosition.x <= cornerRadius || Input.GetKey("a") ) {
                if ( Input.mousePosition.x <= _cornerRadiusDouble)
                    //transform.Translate(transform.right * (Time.deltaTime * scrollSpeed * -1), Space.World);
                    cameraRigidBody.AddForce(transform.right * (Time.deltaTime * scrollSpeed * -1));
                //transform.Translate(transform.right * (Time.deltaTime * scrollSpeed * -1), Space.World);
                cameraRigidBody.AddForce(transform.right * (Time.deltaTime * scrollSpeed * -1));
            }
            if ( Input.mousePosition.y >= Screen.height - cornerRadius || Input.GetKey("w") ) {
                if ( Input.mousePosition.y >= Screen.height - _cornerRadiusDouble )
                    //transform.Translate(transform.forward * (Time.deltaTime * scrollSpeed), Space.World);
                    cameraRigidBody.AddForce(transform.forward * (Time.deltaTime * scrollSpeed));
                //transform.Translate(transform.forward * (Time.deltaTime * scrollSpeed), Space.World);
                cameraRigidBody.AddForce(transform.forward * (Time.deltaTime * scrollSpeed));
            }else if ( Input.mousePosition.y <= cornerRadius || Input.GetKey("s")  ) {
                if ( Input.mousePosition.y <= _cornerRadiusDouble )
                    //transform.Translate(transform.forward * (Time.deltaTime * scrollSpeed * -1), Space.World);
                    cameraRigidBody.AddForce(transform.forward * (Time.deltaTime * scrollSpeed * -1));
                //transform.Translate(transform.forward * (Time.deltaTime * scrollSpeed * -1), Space.World);
                cameraRigidBody.AddForce(transform.forward * (Time.deltaTime * scrollSpeed * -1));
            }
        }

        
    }
}
