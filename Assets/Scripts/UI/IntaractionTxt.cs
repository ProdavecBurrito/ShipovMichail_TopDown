using UnityEngine;
using UnityEngine.UI;


public class IntaractionTxt : MonoBehaviour
{
    #region Fields

    [SerializeField] private Text _text;

    private string _keyTxt = "Нажмите F, что бы подобрать ключ";
    private string _doorTxt = "Нажмите F, что бы открыть дверь";
    private string _doorTxtDeny = "У вас нет нужного ключа";
    private string _levetTxt = "Нажмите F, что бы потянуть за рычаг";
    private string _ammoBoxTxt = "Нажмите F, что бы восполнить патроны";
    private string _ammoBoxMaxTxt = "У вас максимум патронов";
    private string _healthBoxTxt = "Нажмите F, что бы восполнить жизни";
    private string _healthBoxMaxHPTxt = "У вас полное здоровье";

    #endregion


    #region Properties

    public string KeyTxt => _keyTxt;
    public string DoorText => _doorTxt;
    public string DoorTextDeny => _doorTxtDeny;
    public string LeverTxt => _levetTxt;
    public string HealthBoxTxt => _healthBoxTxt;
    public string HealthBoxMaxHPTxt => _healthBoxMaxHPTxt;
    public string AmmoBoxTxt => _ammoBoxTxt;
    public string AmmoBoxMaxTxt => _ammoBoxMaxTxt;

    #endregion


    #region Methods

    public void SetText(string text)
    {
        _text.text = text;
    }

    public void SetEmptyTxt()
    {
        _text.text = string.Empty;
    }

    #endregion
}
