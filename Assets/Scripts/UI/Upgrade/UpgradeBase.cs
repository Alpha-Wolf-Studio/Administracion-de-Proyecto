using System.Collections.Generic;
using PrivateClassUnits;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UpgradeBase : MonoBehaviour, IPointerClickHandler
{
    public event System.Action OnUpdateUpgrade;
    public bool isInited;
    public bool canUse = true;
    private Image image;
    [SerializeField] private Image imageCurrencyType;
    protected UiMilitaryBase uiMilitaryBase;
    [SerializeField] protected TMP_Text textCost;
    protected int cost;
    public CurrencyType currencyType = CurrencyType.Gold;
    private Animator animator;
    private static readonly int Successful = Animator.StringToHash("Successful");
    private static readonly int Fail = Animator.StringToHash("Fail");
    private AudioSource audioSource;
    [SerializeField] private List<AudioPerUnits> unitsAudios = new List<AudioPerUnits>();

    private void Awake ()
    {
        audioSource = GetComponent<AudioSource>();
        image = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }

    public void Initialize (UiMilitaryBase uiMilitaryBase)
    {
        isInited = true;
        canUse = true;
        this.uiMilitaryBase = uiMilitaryBase;
        UpdateCost();
    }

    public void SetCurrencyType (CurrencyType currencyType)
    {
        this.currencyType = currencyType;
        imageCurrencyType.sprite = GameManager.Get().GetSprite(currencyType);
    }

    public abstract void UpdateCost ();

    public void OnPointerClick (PointerEventData eventData)
    {
        if (!canUse)
            return;
        BuyUpgrade();
        OnUpdateUpgrade?.Invoke();
    }

    public void SetImage (Sprite sprite) => image.sprite = sprite;

    public abstract void BuyUpgrade ();

    protected void SetTryBuy (bool wasSuccessfulBuy)
    {
        if (wasSuccessfulBuy)
        {
            List<AudioClip> audios = unitsAudios[uiMilitaryBase.subCategorySelect].audiosBuySuccessful;
            if (audios != null && audios.Count > 0)
                PlayAudio(audios[Random.Range(0, audios.Count)]);

            animator.SetTrigger(Successful);
        }
        else
        {
            List<AudioClip> audios = unitsAudios[uiMilitaryBase.subCategorySelect].audiosBuyFail;
            if (audios != null && audios.Count > 0)
                PlayAudio(audios[Random.Range(0, audios.Count)]);

            animator.SetTrigger(Fail);
        }
    }

    private void PlayAudio (AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}

namespace PrivateClassUnits
{
    [System.Serializable]
    public class AudioPerUnits
    {
        public List<AudioClip> audiosBuySuccessful = new List<AudioClip>();
        public List<AudioClip> audiosBuyFail = new List<AudioClip>();
    }
}