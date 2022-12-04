using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    public int levelUnitSelected;
    public int maxLifeUnitsSelected;

    public UpgradeProgression upgradeProgression;
    [SerializeField] private List<UpgradeBase> buttonsUpgrades = new List<UpgradeBase>();
    [SerializeField] private List<UnitData> unitsFiltered;
    [SerializeField] private List<GameObject> modelsUnits = new List<GameObject>();
    [SerializeField] private List<CanvasGroup> upgradesHideMercenary = new List<CanvasGroup>();

    [SerializeField] private List<TMP_Text> textSubCategory = new List<TMP_Text>();
    private List<string> savedTextSubCategory = new List<string>();

    private void Awake ()
    {
        for (int i = 0; i < textSubCategory.Count; i++)
            savedTextSubCategory.Add(textSubCategory[i].text);

        HealthRecoverCalculator.OnUnitsHeal += UpdateHealsUnits;
    }

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

    private void OnDestroy ()
    {
        HealthRecoverCalculator.OnUnitsHeal -= UpdateHealsUnits;
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

    void UpdateHealsUnits ()
    {
        for (int i = 0; i < unitsFiltered.Count; i++)
        {
            units[i].UpdateFillLife(maxLifeUnitsSelected, unitsFiltered[i].Life);
        }
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
        UpdateUnits();
        UpdateUpgrades();
    }

    private void UpdateUnits ()
    {
        int maxUnits = GameManager.Get().GetMaxUnits(subCategorySelect, (MilitaryType) mainCategorySelect);
        levelUnitSelected = GameManager.Get().GetlevelUnit(subCategorySelect, (MilitaryType) mainCategorySelect);
        maxLifeUnitsSelected = (int) GameManager.Get().GetUnitStats(subCategorySelect).GetLifeLevel(levelUnitSelected, subCategorySelect);

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
            units[i].gameObject.SetActive(false);

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

        for (int i = 0; i < unitsFiltered.Count; i++)
        {
            units[i].gameObject.SetActive(true);
            units[i].imageUnit.sprite = GameManager.Get().GetCurrentSprite(unitsFiltered[i].IdUnit, (MilitaryType) mainCategorySelect);
            units[i].UpdateFillLife(maxLifeUnitsSelected, unitsFiltered[i].Life);
        }

        int lenghtUnits = GameManager.Get().unitsStatsLoaded.Count;
        for (int i = unitsFiltered.Count; i < maxUnits; i++)
        {
            units[i].gameObject.SetActive(true);
            units[i].imageUnit.sprite = GameManager.Get().GetCurrentSprite(lenghtUnits, (MilitaryType) mainCategorySelect);
            units[i].UpdateFillLife(0, 0);
        }

        for (int i = 0; i < modelsUnits.Count; i++)
        {
            modelsUnits[i].SetActive(i == subCategorySelect);
        }

        for (int i = 0; i < textSubCategory.Count; i++)
        {
            int level = GameManager.Get().GetlevelUnit(i, (MilitaryType) mainCategorySelect);
            textSubCategory[i].text = string.Format(savedTextSubCategory[i], (level + 1).ToString("F0"));
        }
    }

    private void UpdateUpgrades ()
    {
        for (int i = 0; i < buttonsUpgrades.Count; i++)
        {
            buttonsUpgrades[i].SetImage(GameManager.Get().GetCurrentSprite(subCategorySelect, (MilitaryType)mainCategorySelect));
        }

        for (int i = 0; i < buttonsUpgrades.Count; i++)
        {
            if (buttonsUpgrades[i].isInited)
                buttonsUpgrades[i].UpdateCost();
        }

        if (mainCategorySelect == (int)MilitaryType.Mercenary)
        {
            buttonsUpgrades[1].canUse = false;
            buttonsUpgrades[2].canUse = false;
            buttonsUpgrades[3].canUse = false;

            for (int i = 0; i < upgradesHideMercenary.Count; i++)
            {
                upgradesHideMercenary[i].interactable = false;
                upgradesHideMercenary[i].alpha = 0;
                buttonsUpgrades[i].SetCurrencyType(CurrencyType.Diamond);
            }
        }
        else
        {
            buttonsUpgrades[0].currencyType = CurrencyType.Gold;
            buttonsUpgrades[1].canUse = true;
            buttonsUpgrades[2].canUse = true;
            buttonsUpgrades[3].canUse = true;

            for (int i = 0; i < upgradesHideMercenary.Count; i++)
            {
                upgradesHideMercenary[i].interactable = true;
                upgradesHideMercenary[i].alpha = 1;
                buttonsUpgrades[i].SetCurrencyType(CurrencyType.Gold);
            }
        }
    }

    public List<UnitData> GetUnitsFiltered () => unitsFiltered;
}