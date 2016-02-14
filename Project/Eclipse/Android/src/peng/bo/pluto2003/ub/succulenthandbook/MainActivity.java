package peng.bo.pluto2003.ub.succulenthandbook;

import android.os.Bundle;
import android.widget.Toast;
import android.content.Intent;
import cn.waps.AppConnect;
import cn.waps.UpdatePointsNotifier;

import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;
import com.wanpu.pay.PayConnect;

public class MainActivity extends UnityPlayerActivity implements
		UpdatePointsNotifier {
	
	@Override
	protected void onCreate(Bundle arg0) {
		super.onCreate(arg0);

		// 初始化统计器
		AppConnect.getInstance(MainActivity.this);
		// 初始化快捷支付
		PayConnect.getInstance("681259ee7f6f07887a27d8e3253e9255", "mumayi",
				MainActivity.this);
		//从服务器端获取户积分:
		AppConnect.getInstance(this).getPoints(this);
	}

	//留给C#调用
	//去支付界面
	protected void GoToPayView(String string) {	
		Intent intent = new Intent();
		intent.setClass(MainActivity.this, MainPayViewActivity.class);
		MainActivity.this.startActivity(intent);

	}
	//去BBS界面

	protected void GoToBBSView(String string) {	
//		Intent intent = new Intent();
//		intent.setClass(MainActivity.this, MainPayViewActivity.class);
//		MainActivity.this.startActivity(intent);
		
		Intent intent = new Intent(MainActivity.this, BBSActivity.class);
	
		intent.putExtra("ObjectName", string);

		startActivity(intent);


	}
	
	
	// 去积分墙界面
	protected void GoToCreditsView(String string) {	
		
		AppConnect.getInstance(this).showOffers(this);

	}

	//调用AppConnect.getInstance(this).getPoints(this);会回调的方法
	@Override
	public void getUpdatePoints(String arg0, int arg1) {
		UnityPlayer.UnitySendMessage("UnityPayManager",
				"SetMyUserPoint", "" + arg1);

	}
	//获取积分失败时自动调用
	@Override
	public void getUpdatePointsFailed(String arg0) {
	
	}
	//积分墙界面成功获取积分后的：
	@Override
	protected void onResume() {
		super.onResume();
		//从服务器端获取户积分
		AppConnect.getInstance(this).getPoints(this);
	}
	
}
