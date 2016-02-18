using UnityEngine;
using System.Collections;

/// <summary>
/// TabNode
/// </summary>
public class SelectedViewTabelNode : MonoBehaviour 
{
    public UILabel m_nameLabel = null;

    public void Init(int nodeIndex)
    {
        UIEventListener.Get(this.gameObject).onClick += (g) =>
        {
            MessageManager.Dispatch(MsgType.SelectedView.showNode, nodeIndex);
        };

        FamilyConfig config = ConfigManager.instance.GetConfig<FamilyConfig>(ConfigTypeEnum.Family, nodeIndex);

        m_nameLabel.text = config.name + "";
    }
}
