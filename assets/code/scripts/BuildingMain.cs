using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingMain : MonoBehaviour
{
    //public float map[,] = new float[0, 0] 
    public GameObject[] walls;
    public GameObject[] traps;
    public GameObject[] misc;
    public static PropSelection selection;
    private int index1 = selection.categoryIndex;
    private int index2 = selection.propIndex;
    public float rotationAmount = 90f;
    private float currentRotation = 0f;
    public float gridSize = 1f;

    private GameObject previewObject;

    private bool deleteMode = false;

    void Start()
    {
        UpdatePreviewObject();
        previewObject.name = "preview";
    }

    void Update()
    {
        ToggleDeleteMode();

        if (!deleteMode)
        {
            RotationThingy();
            UpdatePreview();

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                PlaceWall();
            }
        }
        else
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                DeleteWall();
            }
        }
    }
    
    void ToggleDeleteMode()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            deleteMode = !deleteMode;
            previewObject.SetActive(!deleteMode);
        }
    }

    void RotationThingy()
    {
        if (Input.GetKeyDown(KeyCode.R))
            currentRotation += 90;

        if (Input.GetKeyDown(KeyCode.T))
            currentRotation -= 90;

        previewObject.transform.rotation =
            Quaternion.Euler(0f, currentRotation, 0f);
    }

    void UpdatePreview()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 snappedPosition = SnapToGrid(hit.point);
            previewObject.transform.position = snappedPosition;
        }
    }

    void PlaceWall()
    {
        GameObject[] currentArray = GetCurrentSelection();
        GameObject newWall = Instantiate(currentArray[selection.propIndex], 
            previewObject.transform.position,
            previewObject.transform.rotation);

        newWall.tag = "Wall";
    }

    void DeleteWall()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }

    Vector3 SnapToGrid(Vector3 position)
    {
        float cellSize = gridSize;

        bool isVertical = Mathf.Abs(currentRotation%180) == 90;

        float x, z;

        if (isVertical)
        {
            x = Mathf.Round(position.x/cellSize)*cellSize;
            z = Mathf.Floor(position.z/cellSize)*cellSize+cellSize/2f;
        }
        else
        {
            x = Mathf.Floor(position.x/cellSize)*cellSize+cellSize/2f;
            z = Mathf.Round(position.z/cellSize)*cellSize;
        }

        return new Vector3(x, 0f, z);
    }

    //void 2DArrayMap
    //{
    //  int pos = transform.position;
      
    //}
    
    public GameObject[] GetCurrentSelection()
    {
      if (selection.categoryIndex == 0)
      {
        return walls;
      } else if (selection.categoryIndex == 1)
      {
        return traps;
      } else if (selection.categoryIndex == 2)
      {
        return misc;
      } else {
        return walls;
      }
    }
   public void UpdatePreviewObject()
    {
      if (previewObject != null)
        Destroy(previewObject);
      GameObject[] currentArray = GetCurrentSelection();
    }
}
