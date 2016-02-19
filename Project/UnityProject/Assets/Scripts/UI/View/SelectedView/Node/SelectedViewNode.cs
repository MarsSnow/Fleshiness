/*
* 　　　　　　　　┏┓　　　┏┓+ +
*　　　　　　　┏┛┻━━━┛┻┓ + +
*　　　　　　　┃　　　　　　　┃ 　
*　　　　　　　┃　　　━　　　┃ ++ + + +
*　　　　　　 ████━████ ┃+
*　　　　　　　┃　　　　　　　┃ +
*　　　　　　　┃　　　┻　　　┃
*　　　　　　　┃　　　　　　　┃ + +
*　　　　　　　┗━┓　　　┏━┛
*　　　　　　　　　┃　　　┃　　　　　　　　　
*　　　　　　　　　┃　　　┃ + + + +
*　　　　　　　　　┃　　　┃　
*　　　　　　　　　┃　　　┃ + 　　　　
*　　　　　　　　　┃　　　┃
*　　　　　　　　　┃　　　┃　　+　　　　　　　　　
*　　　　　　　　　┃　 　　┗━━━┓ + +
*　　　　　　　　　┃ 　　　　　　　┣┓
*　　　　　　　　　┃ 　　　　　　　┏┛
*　　　　　　　　　┗┓┓┏━┳┓┏┛ + + + +
*　　　　　　　　　　┃┫┫　┃┫┫
*　　　　　　　　　　┗┻┛　┗┻┛+ + + +
*/

using UnityEngine;

/// <summary>
/// 类名：ListNode
/// @author 彭博
/// </summary>
public class SelectedViewNode : MonoBehaviour 
{
    public Transform m_iconTf = null;
    public UILabel m_nameLabel = null;
    public UILabel m_indexLabel = null;
    public GameObject m_marObj = null;

    public void Init(int curFamily, int curSpeciesIndex, int species)
    {
        UIEventListener.Get(this.gameObject).onClick += (g) => 
        {
            ItemRecordMng.SetCurItemState(curFamily, species, "true");
            SetMark(curFamily, species);

            // Show Info View
            Globals.instance.m_selectedView.gameObject.SetActive(false);
            Globals.instance.m_selectedViewTabel.gameObject.SetActive(false);       
            Globals.instance.m_infoView.gameObject.SetActive(true);
            Globals.instance.m_helpView.gameObject.SetActive(false);
            Globals.instance.m_infoView.InitInfoView(species);

        };

        SpeciesConfig speciesConfig = ConfigManager.instance.GetConfig<SpeciesConfig>(ConfigTypeEnum.Species, species);
        GameObject obj = Utility.DynamicCreatImageObject(UnityConfig.kPicIconPath + speciesConfig.icon, m_iconTf, UnityConfig.kNodeIconDepth, 1, 1, 220, 300, "Icon");
        m_nameLabel.text = speciesConfig.name;
        m_indexLabel.text = curSpeciesIndex.ToString();
        SetMark(curFamily, species);
    }

    public void SetMark(int curFamily, int species)
    {
        string state = ItemRecordMng.GetCurItemState(curFamily, species);
        if (state == "true")
        {
            m_marObj.SetActive(true);
        }
        else
        {
            m_marObj.SetActive(false);
        }
    }
}
