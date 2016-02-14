package peng.bo.pluto2003.ub.succulenthandbook;

import android.app.Activity;
import android.content.Context;
import android.content.pm.ActivityInfo;
import android.os.Bundle;
import android.view.View;
import android.view.WindowManager;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import cn.waps.AppConnect;
import cn.waps.UpdatePointsNotifier;

import com.unity3d.player.UnityPlayer;
import com.wanpu.pay.PayConnect;
import com.wanpu.pay.PayResultListener;

public class MainPayViewActivity extends Activity implements
UpdatePointsNotifier{
	//若成功支付，奖励给用户的积分
	public int PayPointsAmount = 100; 
	// 应用或游戏商自定义的支付订单(每条支付订单数据不可相同)
	String orderId = "";
	// 用户标识
	String userId = "";
	// 支付商品名称
	
	String goodsName = "";
	// 支付金额
	float price = 2f;
	// 支付时间
	String time = "";
	// 支付描述
	String goodsDesc = "欢迎使用本软件，软件由作者在业余时间制作和维护�?如果方便，请我喝�?��啤酒吧�?";
	// 应用或游戏商服务器端回调接口（无服务器可不填写）
	String notifyUrl = "";

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		//从服务器端获取用户积
		AppConnect.getInstance(this).getPoints(this);
		// 强制全屏
		getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
				WindowManager.LayoutParams.FLAG_FULLSCREEN);
		// 设置竖屏模式
		setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_BEHIND);
		// 无标题栏
		// this.requestWindowFeature(Window.FEATURE_NO_TITLE);
	
		this.setContentView(R.layout.pay_main_view);
		 goodsName =getResources().getString(R.string.app_name);
		// 商品名称控件
		TextView goodsNameView = (TextView) this.findViewById(R.id.goodsName);
		goodsNameView.setText(goodsName);
		final EditText amountView = (EditText) this.findViewById(R.id.amount);
		
		// 商品详细描述
		TextView detailsView = (TextView) this.findViewById(R.id.details);

		detailsView.setText(goodsDesc);

		// 用户ID（使用用户设备ID代替
		userId = PayConnect.getInstance(MainPayViewActivity.this).getDeviceId(
				MainPayViewActivity.this);
		// 提交按钮
		Button submitButton = (Button) this.findViewById(R.id.submitBtn);

		// 支付SDK版本
		TextView sdkVersionView = (TextView) this.findViewById(R.id.sdkVersion);
		sdkVersionView
				.setText("支付系统SDK版本" + PayConnect.LIBRARY_VERSION_NUMBER);

		submitButton.setOnClickListener(new OnClickListener() {

			public void onClick(View v) {
				try {
					
					//游戏商自定义支付订单号，保证该订单号的唯�??，建议在执行支付操作时才进行该订单号的生�?
					orderId = System.currentTimeMillis() + "";
			
					String amountStr = amountView.getText().toString();
					if(!"".equals(amountStr)){
						price = Float.valueOf(amountStr);
					}else{
						price = 0.0f;
					}
					
					PayConnect.getInstance(MainPayViewActivity.this).pay(
							MainPayViewActivity.this, orderId, userId, price,
							goodsName, goodsDesc, notifyUrl,
							new MyPayResultListener());

				} catch (NumberFormatException e) {
					e.printStackTrace();
				}
			}
		});

	}

	@Override
	protected void onDestroy() {
		// 以前版本的finalize()方法作废
		PayConnect.getInstance(this).close();
		super.onDestroy();
	}

	/**
	 * 自定义Listener实现PaySuccessListener，用于监听支付成
	 * 
	 * @author Administrator
	 * 
	 */
	private class MyPayResultListener implements PayResultListener {
		// 支付完成

		public void onPayFinish(Context payViewContext, String orderId,
				int resultCode, String resultString, int payType, float amount,
				String goodsName) {

			// 可根据resultCode自行判断
			if (resultCode == 0) {
				// 提示成功支付
				Toast.makeText(
						getApplicationContext(),
						resultString + "：" + amount + "元" + "。" + "\n"
								+ "谢谢您的支持！", Toast.LENGTH_LONG).show();

				//奖励用户100积分
				AddPay100Points();
				// TODO 在客户端处理支付成功的操�?
				// 未指定notifyUrl的情况下，交易成功后，必须发送回�?
				PayConnect.getInstance(MainPayViewActivity.this).confirm(orderId,
						payType);
			} else {
				Toast.makeText(getApplicationContext(), resultString,
						Toast.LENGTH_LONG).show();
			}
		}
	}

	// 更新积分方法：支�?元，增加积分
	public void AddPay100Points() {
		//奖励的积分传入服务器
		//奖励PayPointsAmout数量的积分，该方法会自动回调getUpdatePoints方法
		AppConnect.getInstance(this).awardPoints(PayPointsAmount, this);
		
	}

	//调用AppConnect.getInstance(this).awardPoints(int amount, this);会回调的方法
	public void UpdatePointsNotifier(String arg0, int arg1) {
		//取出服务器的积分，传递给Unity
		UnityPlayer.UnitySendMessage("UnityPayManager",
				"SetMyUserPoint", "" + arg1);	
	}
	
	//调用AppConnect.getInstance(this).getPoints(this);会回调的方法
	public void getUpdatePoints(String arg0, int arg1) {
		//取出服务器的积分，传递给Unity
		UnityPlayer.UnitySendMessage("UnityPayManager",
				"SetMyUserPoint", "" + arg1);
	
	}

	public void getUpdatePointsFailed(String arg0) {
		// TODO Auto-generated method stub
		
	}

}
