using UnityEngine;
using System.Collections;

public class MsgType 
{
    public class HideInActiveNode
    {
        public const string triggerHideItem = "Msg.HideInActiveNode.TriggerHideItem";
    }

    public class SelectedView
    {
        public const string showNode = "msg.SelectedView.showNode";
    }

    public class InfoView
    {
        public const string showInfoView = "msg.InfoView.showInfoView";
    }

    public const string c_operator1 = "msg.background.c_operator1";
    public const string c_operator2 = "msg.background.c_operator2";
    public const string c_operator3 = "msg.background.c_operator3";
    //弹窗
    public class View
    {
        /// <summary>
        /// 显示弹窗
        /// </summary>
        public const string show = "msg.background.show";

        /// <summary>
        /// 关闭弹窗
        /// </summary>
        public const string hide = "msg.background.hide";

        /// <summary>
        /// 在弹窗从打开其它弹窗
        /// </summary>
        public const string showChild = "msg.background.showChild";

        /// <summary>
        /// 从当前弹窗返回到上一弹窗
        /// </summary>
        public const string showParent = "msg.background.showParent";

        /// <summary>
        /// show or hide the black mask
        /// </summary>
        //public const string showBlackMask = "msg.background.blackMask";
        //active mask
        public const string activeMask = "msg.background.mask";

        public const string hideSelf = "msg.background.hideSelf";
        public const string hideAllView = "msg.background.hideAllView";
        public const string hideAllViews = "msg.background.hideAllViews";

        public const string showPopView = "msg.background.showPopView";
        public const string hidePopView = "msg.background.hidePopView";
    }

    //简单消息
    public class Tips
    {
        /// <summary>
        /// 显示提示消息,参数为消息字符串
        /// </summary>
        public const string showTip = "msg.tips.showTip";
        public const string showLoding = "msg.tips.showLoding";
        public const string closeLoding = "msg.tips.closeLoding";
        /// <summary>
        /// 显示错误提示，参数为错误码（message.xlsx的id列）将显示message.xlsx的message列
        /// </summary>
        public const string showErrorTip = "msg.tips.showErrorTip";
    }

    //大厅
    public class Lobby
    {
        /// <summary>
        /// 召唤师数据发生变化
        /// </summary>
        public const string dataChange = "msg.lobby.data_change";
        //
        public const string playerIconChange = "msg.lobby.playerIconChanged";
        //
        public const string updateMarkState = "msg.lobby.updateMarkState";
        public const string buyOnClick = "msg.lobby.buyOnClick";
        public const string ChestsBuyClick = "msg.lobby.ChestsBuyOneOnClick";
        //Disconnect message listener
        public const string disconnectChargeListener = "msg.lobby.disconnectChargeListener";
        //Restorage message listener
        public const string connectBtnListener = "msg.lobby.connectBtnListener";
        //show task tips
        public const string showTaskTips = "msg.lobby.showTaskTips";
    }
    //好友
    public class Friend
    {
        /// <summary>
        /// 好友发生变化
        /// </summary>
        public const string gameFriend = "msg.friend.gameFriend";
        public const string systemFriend = "msg.friend.systemFriend";
        public const string visitFriend = "msg.friend.visitFriend";
        public const string deleteFriend = "msg.friend.deleteFriend";
        public const string showDeleteBtn = "msg.friend.showDeleteBtn";
    }
    //战斗
    public class Fight
    {
        /// <summary>
        /// 战斗中召唤师数据发生变化
        /// </summary>
        public const string updataCoin = "msg.fight.updataCoin";
        /// <summary>
        /// 战斗中装备获取
        /// </summary>
        public const string updateEquipInfo = "msg.fight.EquipInfo";

    }

    //Role information view
    public class RoleInfoView
    {
        //Add Exp
        public const string addExp = "msg.roleInfo.addExp";
        //Wear equipment
        public const string wearEquip = "msg.roleInfo.wearEquip";
        //Wear weapon
        public const string levelUp = "msg.roleInfo.levelUp";
        //hero quality level up
        public const string qualityLevelUp = "msg.roleInfo.qualityLevelUp";
        public const string mainInfoPanel = "msg.roleInfo.mainInfoPanel";
        public const string starLevelUp = "msg.roleInfo.starLevelUp";
        public const string summonHero = "msg.roleInfo.summonHeroResort";
        public const string updateMainInfo = "msg.roleInfo.updateMainInfo";
    }

    public class ModelViewMsg
    {
        public const string destroyLobbyTeamModel = "msg.modelViewMsg.destroyLobbyTeamModel";
        public const string setLobbyTeamModel = "msg.modelViewMsg.setLobbyTeamModel";
        public const string resetShowEquipment = "msg.modelViewMsg.resetShowEquipment";
        public const string resetShowFriendEquipment = "msg.modelViewMsg.resetShowFriendEquipment";
    }

    public class TeamViewMsg
    {
        public const string showCaptainSpellInfo = "msg.teamViewMsg.showCaptainSpellInfo";
        public const string hideCaptainSpellInfo = "msg.teamViewMsg.hideCaptainSpellInfo";

        public const string deleteAll = "msg.teamViewMsg.deleteAll";
        public const string iconClick = "msg.teamViewMsg.iconClick";
        public const string changeSolution = "msg.teamViewMsg.changeSolution";
        public const string removeHeroOperation = "msg.teamViewMsg.removeHeroOperation";
        public const string changeTeamAppearanceList = "msg.teamViewMsg.changeTeamAppearanceList";
        public const string initHeroIconPanel = "msg.teamViewMsg.initHeroIconPanel";
        public const string onCombinationViewEnableClick = "msg.teamViewMsg.onCombinationViewEnableClick";
        public const string showCurrentTeam = "msg.teamViewMsg.showCurrentTeam";
        public const string createSelectFrame = "msg.teamViewMsg.createSelectFrame";
        public const string setHeroSelectIndex = "msg.teamViewMsg.setHeroSelectIndex";
        public const string onRemoveHero = "msg.teamViewMsg.onRemoveHero";
    }

    public class GameSceneMsg
    {
        private const string head = "msg.gameSceneMsg.";
        public const string addHpBar = head + "addHpBar";
        public const string addBossUI = head + "addBossUI";
        public const string hideHpBar = head + "hideHpBar";
        public const string showHpBarById = head + "showHpBarById";
        public const string findWorldBoss = head + "findWorldBoss";
        public const string triggerWorldBoss = head + "triggerWorldBoss";
        public const string updateTime = head + "updateTime";
        public const string updateList = head + "updateList";
        public const string triggerRandomEnemy = head + "triggerRandomEnemy";
        public const string triggerBoss = head + "triggerBoss";

        public const string InitStoryStar = head + "InitStoryStar";
        public const string UpdateStoryStar = head + "UpdateStoryStar";
    }

    public class MopUpPopView
    {
        public const string showMopUpInfo = "msg.mopUpPopViewMsg.showMopUpInfo";
        public const string setStoryData = "msg.mopUpPopViewMsg.setStoryData";
        public const string resetStoryData = "msg.mopUpPopViewMsg.resetStoryData";
        public const string showHintText = "msg.mopUpPopViewMsg.showHintText";
        public const string goVipView = "msg.mopUpPopViewMsg.goVipView";
        public const string judegeMopUpOneDiamond = "msg.mopUpPopViewMsg.judegeMopUpOneDiamond";
    }

    public class TaskViewMsgType
    {
        public const string updateTask = "msg.taskViewMsgType.updateTask";
    }

    public class BagViewMsgType
    {
        public const string updateItemList = "msg.bagViewMsgType.updateItemList";
        public const string itemChanged = "msg.bagViewMsgType.itemChanged";
        public const string heroPanelClose = "msg.bagViewMsgType.heroPanelClose";
        public const string heroPanelShow = "msg.bagViewMsgType.heroPanelShow";
        public const string bagInfoClose = "msg.bagViewMsgType.bagInfoClose";
    }

    public class ChargeBtnLayerMsgType
    {
        public const string updateItemList = "msg.chargeBtnLayerMsgType.updatePlayerLv";
    }

    public class AudioManagerMsgType
    {
        public const string audioButtonPress = "msg.adioManagerMsgType.audioButtonPress";
    }

    public class DailySignInView
    {
        public const string goCharge = "msg.dailySignInView.goCharge";
    }

    public class DreamView
    {
        public const string setRevordInfo = "msg.dreamView.setRevordInfo";
        public const string refreshInfo = "msg.dreamView.refreshInfo";
        public const string refreshBurningInfo = "msg.dreamView.refreshBurningInfo";
        //public const string setBurningInfo = "msg.dreamView.setBurningInfo";
    }

    public class RewardTownLayer
    {
        public const string setWave = "msg.rewardTownLayer.setWave";
        public const string setTime = "msg.rewardTownLayer.setTime";
    }

    //PenSprite
    public class GameScene
    {
        public const string SetPosition = "msg.gameScene.setPosition";
        public const string SetX = "msg.gameScene.setX";
        public const string SetY = "msg.gameScene.setY";
        public const string UnBindJoyStick = "msg.gameScene.unBindJoyStick";
    }

}
