using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UiMilitaryBase : MonoBehaviour
{
    [SerializeField] private GameObject prefabUnitComponent;
    [SerializeField] private Transform panelContentUnit;

    [Space(15)] 
    [SerializeField] private List<Button> mainCategories = new List<Button>();
    [SerializeField] private List<Button> subCategories = new List<Button>();
    [SerializeField] private List<UnitMilitaryComponent> units = new List<UnitMilitaryComponent>();

    public int mainCategorySelect;
    public int subCategorySelect;

    public UpgradeProgression upgradeProgression;
    [SerializeField] private List<UpgradeBase> buttonsUpgrades = new List<UpgradeBase>();
    [SerializeField] private List<UnitData> unitsFiltered;
    [SerializeField] private List<GameObject> modelsUnits = new List<GameObject>();

    private void Start ()
    {
        for (int i = 0; i < buttonsUpgrades.Count; i++)
        {
            buttonsUpgrades[i].Initialize(this);
            buttonsUpgrades[i].OnUpdateUpgrade += UpdateUnitsFiltered;
        }

        mainCategories[0].Select();
        subCategories[0].Select();

        for (int i = 0; i < mainCategories.Count; i++)
        {
            int index = i;
            mainCategories[i].onClick.AddListener(() => OnPressMainCategory(index));
        }

        for (int i = 0; i < subCategories.Count; i++)
        {
            int index = i;
            subCategories[i].onClick.AddListener(() => OnPressSubCategories(index));
        }

        //btnCategoryUpgrade.onClick.AddListener(OnPressUpgradeCategory);

        SetSelectable();
        UpdateUnitsFiltered();
        UpdateUpgrades();
    }

    private void OnPressMainCategory (int index)
    {
        mainCategorySelect = index;
        SetSelectable();
        UpdateUnitsFiltered();
        UpdateUpgrades();
    }

    private void OnPressSubCategories (int index)
    {
        subCategorySelect = index;
        SetSelectable();
        UpdateUnitsFiltered();
        UpdateUpgrades();
    }

    private void SetSelectable ()
    {
        for (int i = 0; i < mainCategories.Count; i++)
        {
            mainCategories[i].interactable = (i != mainCategorySelect);
        }

        for (int i = 0; i < subCategories.Count; i++)
        {
            subCategories[i].interactable = (i != subCategorySelect);
        }
    }

    private void UpdateUnitsFiltered ()
    {
        switch ((MilitaryType) mainCategorySelect)
        {
            case MilitaryType.Army:
                unitsFiltered = GameManager.Get().GetUnitsArmy().ToList();
                break;
            case MilitaryType.Mercenary:
                unitsFiltered = GameManager.Get().GetUnitsMercenary().ToList();
                break;
            default:
                unitsFiltered = null;
                break;
        }

        for (int i = 0; i < units.Count; i++)
        {
            units[i].gameObject.SetActive(false);
        }

        if (unitsFiltered == null || unitsFiltered.Count == 0)
            return;

        for (int i = unitsFiltered.Count - 1; i >= 0; i--)
        {
            if (unitsFiltered[i].IdUnit != subCategorySelect)
                unitsFiltered.Remove(unitsFiltered[i]);
        }

        while (units.Count < unitsFiltered.Count)
        {
            UnitMilitaryComponent unit = Instantiate(prefabUnitComponent, panelContentUnit).GetComponent<UnitMilitaryComponent>();
            units.Add(unit);
        }

        int maxUnits = GameManager.Get().GetMaxUnits(subCategorySelect, (MilitaryType) mainCategorySelect);

        for (int i = 0; i < unitsFiltered.Count; i++)
        {
            units[i].gameObject.SetActive(true);
            units[i].imageUnit.sprite = GameManager.Get().GetCurrentSprite(unitsFiltered[i].IdUnit, (MilitaryType) mainCategorySelect);
            units[i].UpdateFillLife(unitsFiltered[i].IdUnit, unitsFiltered[i].Life);
        }

        int lenghtUnits = GameManager.Get().unitsStatsLoaded.Count;
        for (int i = unitsFiltered.Count; i < maxUnits; i++)
        {
            units[i].gameObject.SetActive(true);
            units[i].imageUnit.sprite = GameManager.Get().GetCurrentSprite(lenghtUnits, (MilitaryType) mainCategorySelect);
        }

        for (int i = 0; i < buttonsUpgrades.Count; i++)
        {
            buttonsUpgrades[i].SetImage(GameManager.Get().GetCurrentSprite(subCategorySelect, (MilitaryType) mainCategorySelect));
        }

        for (int i = 0; i < modelsUnits.Count; i++)
        {
            modelsUnits[i].SetActive(i == subCategorySelect);
        }
    }

    void UpdateUpgrades ()
    {
        for (int i = 0; i < buttonsUpgrades.Count; i++)
        {
            buttonsUpgrades[i].UpdateCost();
        }
    }

    public List<UnitData> GetUnitsFiltered () => unitsFiltered;
}