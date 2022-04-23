using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    public GameObject mainCamera;

    public float Speed = 30f;

    int activeCamera;
    int rotateValueOnLeft;
    int rotateValueOnRight;

    // Start is called before the first frame update
    void Start()
    {
        rotateValueOnLeft = 0;
        rotateValueOnRight = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Z))
        {
            mainCamera.transform.Translate(Vector3.forward * Speed * Time.deltaTime, Space.Self);
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, 10, mainCamera.transform.position.z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            mainCamera.transform.Translate(Vector3.back * Speed * Time.deltaTime, Space.Self);
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, 10, mainCamera.transform.position.z);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            mainCamera.transform.Translate(Vector3.left * Speed * Time.deltaTime, Space.Self);
        }

        if (Input.GetKey(KeyCode.D))
        {
            mainCamera.transform.Translate(Vector3.right * Speed * Time.deltaTime, Space.Self);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            rotateValueOnLeft++;
            rotateValueOnRight++;
            switch (rotateValueOnLeft)
            {
                case 1:
                    mainCamera.transform.eulerAngles = new Vector3(60, 90, 0);
                    break;
                case 2:
                    mainCamera.transform.eulerAngles = new Vector3(60, 180, 0);
                    break;
                case 3:
                    mainCamera.transform.eulerAngles = new Vector3(60, 270, 0);
                    break;
                case 4:
                    mainCamera.transform.eulerAngles = new Vector3(60, 0, 0);
                    rotateValueOnLeft = 0;
                    rotateValueOnRight = 0;
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            rotateValueOnRight++;
            rotateValueOnLeft++;
            switch (rotateValueOnRight)
            {
                case 1:
                    mainCamera.transform.eulerAngles = new Vector3(60, -90, 0);
                    break;
                case 2:
                    mainCamera.transform.eulerAngles = new Vector3(60, -180, 0);
                    break;
                case 3:
                    mainCamera.transform.eulerAngles = new Vector3(60, -270, 0);
                    break;
                case 4:
                    mainCamera.transform.eulerAngles = new Vector3(60, 0, 0);
                    rotateValueOnLeft = 0;
                    rotateValueOnRight = 0;
                    break;
            }
        }


    }

}
