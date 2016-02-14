using UnityEngine;
using System.Collections;

public class SelectedViewNode : MonoBehaviour 
{
    public Transform m_iconTf = null;
    public UILabel m_nameLabel = null;
    public UILabel m_indexLabel = null;

    public void Init(int curFamily, int curSpeciesIndex, int species)
    {
        UIEventListener.Get(this.gameObject).onClick += (g) => 
        { 
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
    }
}
