using UnityEngine;

public class PropSelection : MonoBehaviour
{
    public int categoryIndex = 0;
    public int propIndex = 0;
    public BuildingMain building;

    void Update()
    {
        HandleCategoryInput();
        HandlePropInput();
    }
    void HandleCategoryInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SetCategory(0);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SetCategory(1);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            SetCategory(2);
    }
    void HandlePropInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            propIndex--;
            WrapIndex();
            building.UpdatePreviewObject();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            propIndex++;
            WrapIndex();
            building.UpdatePreviewObject();
        }
    }

    void SetCategory(int newCategory)
    {
        categoryIndex = newCategory;
        propIndex = 0;
        building.UpdatePreviewObject();
    }

    void WrapIndex()
    {
        int length = building.GetCurrentSelection().Length;

        if (propIndex < 0)
            propIndex = length - 1;

        if (propIndex >= length)
            propIndex = 0;
    }
}
