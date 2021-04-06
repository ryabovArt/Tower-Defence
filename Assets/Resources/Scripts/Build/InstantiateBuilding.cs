using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateBuilding : MonoBehaviour
{
    [SerializeField] private List<AssetItem> Items; // список построек
    [SerializeField] private BuildingsCell buildingsCell; // ячейка с постройкой
    [SerializeField] private Transform container; // место для расположения построек(UI)

    public void OnEnable()
    {
        Render(Items);
    }

    /// <summary>
    /// Создаем новые айтемы с постройками и удаляем старые
    /// </summary>
    /// <param name="items"> список построек </param>
    public void Render(List<AssetItem> items)
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }

        items.ForEach(item =>
        {
            var cell = Instantiate(buildingsCell, container);
            cell.Render(item);
            cell.GetComponent<Button>().onClick.AddListener(() => cell.TowerChoose());
        });
    }
}
