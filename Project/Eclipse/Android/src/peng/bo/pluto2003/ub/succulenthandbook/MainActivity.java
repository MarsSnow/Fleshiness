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

		// ��ʼ��ͳ����
		AppConnect.getInstance(MainActivity.this);
		// ��ʼ�����֧��
		PayConnect.getInstance("681259ee7f6f07887a27d8e3253e9255", "mumayi",
				MainActivity.this);
		//�ӷ������˻�ȡ������:
		AppConnect.getInstance(this).getPoints(this);
	}

	//����C#����
	//ȥ֧������
	protected void GoToPayView(String string) {	
		Intent intent = new Intent();
		intent.setClass(MainActivity.this, MainPayViewActivity.class);
		MainActivity.this.startActivity(intent);

	}
	//ȥBBS����

	protected void GoToBBSView(String string) {	
//		Intent intent = new Intent();
//		intent.setClass(MainActivity.this, MainPayViewActivity.class);
//		MainActivity.this.startActivity(intent);
		
		Intent intent = new Intent(MainActivity.this, BBSActivity.class);
	
		intent.putExtra("ObjectName", string);

		startActivity(intent);


	}
	
	
	// ȥ����ǽ����
	protected void GoToCreditsView(String string) {	
		
		AppConnect.getInstance(this).showOffers(this);

	}

	//����AppConnect.getInstance(this).getPoints(this);��ص��ķ���
	@Override
	public void getUpdatePoints(String arg0, int arg1) {
		UnityPlayer.UnitySendMessage("UnityPayManager",
				"SetMyUserPoint", "" + arg1);

	}
	//��ȡ����ʧ��ʱ�Զ�����
	@Override
	public void getUpdatePointsFailed(String arg0) {
	
	}
	//����ǽ����ɹ���ȡ���ֺ�ģ�
	@Override
	protected void onResume() {
		super.onResume();
		//�ӷ������˻�ȡ������
		AppConnect.getInstance(this).getPoints(this);
	}
	
}
