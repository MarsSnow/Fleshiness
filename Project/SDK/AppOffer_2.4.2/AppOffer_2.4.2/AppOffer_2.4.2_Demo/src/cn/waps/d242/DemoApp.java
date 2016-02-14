package cn.waps.d242;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.res.Configuration;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;
import android.view.KeyEvent;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewGroup.LayoutParams;
import android.view.Window;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.TextView;
import cn.waps.AdInfo;
import cn.waps.AppConnect;
import cn.waps.AppListener;
import cn.waps.UpdatePointsNotifier;
import cn.waps.d240.R;
import cn.waps.extend.AppDetail;
import cn.waps.extend.AppWall;
import cn.waps.extend.QuitPopAd;
import cn.waps.extend.SlideWall;

public class DemoApp extends Activity implements View.OnClickListener, UpdatePointsNotifier {

	private TextView pointsTextView;
	private TextView SDKVersionView;

	private String displayPointsText;

	final Handler mHandler = new Handler();

	// 抽屉广告布局
	private View slidingDrawerView;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		this.requestWindowFeature(Window.FEATURE_NO_TITLE);
		super.onCreate(savedInstanceState);
		setContentView(R.layout.main);

		// 初始化统计器，并通过代码设置APP_ID, APP_PID
		AppConnect.getInstance("09f277ca386ee99cb4c910e09f562112", "waps", this);

		// RootTools.getInstance().root(this, "该应用中的高级功能需要root权限支持，是否root？");

		// 初始化统计器，需要在AndroidManifest中注册APP_ID和APP_PID值
		// AppConnect.getInstance(this);.
		// 以上两种统计器初始化方式任选其一，不要同时使用

		// 禁用错误报告
		// AppConnect.getInstance(this).setCrashReport(false);

		Button offersButton = (Button) findViewById(R.id.OffersButton);
		Button shareOffersButton = (Button) findViewById(R.id.shareOffersButton);
		Button gameOffersButton = (Button) findViewById(R.id.gameOffersButton);
		Button appOffersButton = (Button) findViewById(R.id.appOffersButton);
		Button moreAppsButton = (Button) findViewById(R.id.moreAppsButton);
		Button spendButton = (Button) findViewById(R.id.spendButton);
		Button feedbackButton = (Button) findViewById(R.id.feedbackButton);
		Button awardButton = (Button) findViewById(R.id.awardButton);
		Button diyAdButton = (Button) findViewById(R.id.diyAdButton);
		Button diyAdListButton = (Button) findViewById(R.id.diyAdListButton);
		Button popAdButton = (Button) findViewById(R.id.popAdButton);
		Button ownAppDetailButton = (Button) findViewById(R.id.ownAppDetailButton);
		Button funAdButton = (Button) findViewById(R.id.funAdButton);

		offersButton.setOnClickListener(this);
		shareOffersButton.setOnClickListener(this);
		gameOffersButton.setOnClickListener(this);
		appOffersButton.setOnClickListener(this);
		moreAppsButton.setOnClickListener(this);
		spendButton.setOnClickListener(this);
		feedbackButton.setOnClickListener(this);
		awardButton.setOnClickListener(this);
		diyAdButton.setOnClickListener(this);
		diyAdListButton.setOnClickListener(this);
		popAdButton.setOnClickListener(this);
		ownAppDetailButton.setOnClickListener(this);
		funAdButton.setOnClickListener(this);

		pointsTextView = (TextView) findViewById(R.id.PointsTextView);
		SDKVersionView = (TextView) findViewById(R.id.SDKVersionView);

		// 预加载自定义广告内容（仅在使用了自定义广告、抽屉广告或迷你广告的情况，才需要添加）
		AppConnect.getInstance(this).initAdInfo();

		// 预加载插屏广告内容（仅在使用到插屏广告的情况，才需要添加）
		AppConnect.getInstance(this).initPopAd(this);

		// 设置插屏广告展示时，可使用设备的back键进行关闭
		// 设置为true表示可通过back键关闭，不调用该句代码则使用默认值false
		// AppConnect.getInstance(this).setPopAdBack(true);

		// 预加载功能广告内容（仅在使用到功能广告的情况，才需要添加）
		AppConnect.getInstance(this).initFunAd(this);

		// 带有默认参数值的在线配置，使用此方法，程序第一次启动使用的是"defaultValue"，之后再启动则是使用的服务器端返回的参数值
		String showAd = AppConnect.getInstance(this).getConfig("showAd", "defaultValue");

		SDKVersionView.setText("在线参数:showAd = " + showAd);

		SDKVersionView.setText(SDKVersionView.getText() + "\nSDK版本: " + AppConnect.LIBRARY_VERSION_NUMBER);

		// 设置互动广告无数据时的回调监听（该方法必须在showBannerAd之前调用）
		AppConnect.getInstance(this).setBannerAdNoDataListener(new AppListener() {

			@Override
			public void onBannerNoData() {
				Log.i("debug", "banner广告暂无可用数据");
			}

		});
		// 互动广告调用方式
		LinearLayout layout = (LinearLayout) this.findViewById(R.id.AdLinearLayout);
		AppConnect.getInstance(this).showBannerAd(this, layout);

		// 迷你广告调用方式
		// AppConnect.getInstance(this).setAdBackColor(Color.argb(50, 120, 240,
		// 120));//设置迷你广告背景颜色
		// AppConnect.getInstance(this).setAdForeColor(Color.YELLOW);//设置迷你广告文字颜色
		LinearLayout miniLayout = (LinearLayout) findViewById(R.id.miniAdLinearLayout);
		AppConnect.getInstance(this).showMiniAd(this, miniLayout, 10);// 10秒刷新一次

		// 抽屉式应用墙
		// 1,将drawable-hdpi文件夹中的图片全部拷贝到新工程的drawable-hdpi文件夹中
		// 2,将layout文件夹中的detail.xml和slidewall.xml两个文件，拷贝到新工程的layout文件夹中
		// 获取抽屉样式的自定义广告
		slidingDrawerView = SlideWall.getInstance().getView(this);
		if (slidingDrawerView != null) {
			this.addContentView(slidingDrawerView, new LayoutParams(LayoutParams.FILL_PARENT, LayoutParams.FILL_PARENT));
		}

		// 初始化卸载广告
		// AppConnect.getInstance(this).initUninstallAd(this);

		// 该方法用于获取当前应用的签名md5值，微信平台创建应用时需要用到该值
		// 使用该方法生成签名md5值时，必须确保使用的签名为正式发布的apk所使用的签名
		String sign = AppConnect.getInstance(this).getSignatureMd5(this);
		Log.e("debug", "sign =" + sign);

		// 设置微信平台的AppId，若不适用微信、朋友圈进行分析，则不需要设置
		AppConnect.getInstance(this).setWeixinAppId("wx1e1c162350c09c66", this);

	}

	@Override
	public boolean onKeyDown(int keyCode, KeyEvent event) {
		if (keyCode == KeyEvent.KEYCODE_BACK) {
			if (SlideWall.getInstance().slideWallDrawer != null && SlideWall.getInstance().slideWallDrawer.isOpened()) {
				// 如果抽屉式应用墙展示中，则关闭抽屉
				SlideWall.getInstance().closeSlidingDrawer();
			} else {
				// 调用退屏广告
				QuitPopAd.getInstance().show(this);
			}

		}
		return true;
	}

	// 建议加入onConfigurationChanged回调方法
	// 注:如果当前Activity没有设置android:configChanges属性,或者是固定横屏或竖屏模式,则不需要加入
	@Override
	public void onConfigurationChanged(Configuration newConfig) {
		// 横竖屏状态切换时,关闭处于打开状态中的退屏广告
		QuitPopAd.getInstance().close();
		// 使用抽屉式应用墙,横竖屏状态切换时,重新加载抽屉,保证ListView重新加载,保证ListView中Item的布局匹配当前屏幕状态
		if (slidingDrawerView != null) {
			// 先remove掉slidingDrawerView
			((ViewGroup) slidingDrawerView.getParent()).removeView(slidingDrawerView);
			slidingDrawerView = null;
			// 重新获取抽屉样式布局,此时ListView重新设置了Adapter
			slidingDrawerView = SlideWall.getInstance().getView(this);
			if (slidingDrawerView != null) {
				this.addContentView(slidingDrawerView, new LayoutParams(LayoutParams.FILL_PARENT, LayoutParams.FILL_PARENT));
			}
		}
		super.onConfigurationChanged(newConfig);
	}

	public void onClick(View v) {
		if (v instanceof Button) {
			int id = ((Button) v).getId();

			switch (id) {
			case R.id.OffersButton:
				// 显示推荐列表（综合）
				AppConnect.getInstance(this).showOffers(this);

				break;
			case R.id.popAdButton:
				// 设置插屏广告无数据时的回调监听（该方法必须在showPopAd之前调用）
				AppConnect.getInstance(this).setPopAdNoDataListener(new AppListener() {

					@Override
					public void onPopNoData() {
						Log.i("debug", "插屏广告暂无可用数据");
					}

				});
				// 显示插屏广告
				AppConnect.getInstance(this).showPopAd(this);
				break;
			case R.id.shareOffersButton:
				// 显示推荐列表（软件）
				AppConnect.getInstance(this).showShareOffers(this);
				break;
			case R.id.appOffersButton:
				// 显示推荐列表（软件）
				AppConnect.getInstance(this).showAppOffers(this);
				break;
			case R.id.gameOffersButton:
				// 显示推荐列表（游戏）
				AppConnect.getInstance(this).showGameOffers(this);
				break;
			case R.id.diyAdListButton:
				// 获取全部自定义广告数据
				Intent appWallIntent = new Intent(this, AppWall.class);
				this.startActivity(appWallIntent);
				break;
			case R.id.diyAdButton:
				// 获取一条自定义广告数据
				AdInfo adInfo = AppConnect.getInstance(DemoApp.this).getAdInfo();
				AppDetail.getInstanct().showAdDetail(DemoApp.this, adInfo);
				break;
			case R.id.spendButton:
				// 消费虚拟货币.
				AppConnect.getInstance(this).spendPoints(10, this);
				break;
			case R.id.awardButton:
				// 奖励虚拟货币
				AppConnect.getInstance(this).awardPoints(10, this);
				break;
			case R.id.moreAppsButton:
				// 显示自家应用列表
				AppConnect.getInstance(this).showMore(this);
				break;
			case R.id.ownAppDetailButton:
				// 根据指定的应用app_id展示其详情
				AppConnect.getInstance(this).showMore(this, "c8c3dab81e65e695020e69a74ccff196");
				break;
			case R.id.funAdButton:
				// 调用功能广告接口
				new AlertDialog.Builder(this).setTitle("功能广告").setMessage("请选择测试功能广告的类型")
						.setPositiveButton("视频", new AlertDialog.OnClickListener() {
							@Override
							public void onClick(DialogInterface dialog, int which) {
								String uriStr = "";// 视频地址
								AppConnect.getInstance(DemoApp.this).showPlayer(DemoApp.this, uriStr);
							}
						}).setNegativeButton("浏览器", new AlertDialog.OnClickListener() {
							@Override
							public void onClick(DialogInterface dialog, int which) {
								String uriStr = "http://www.baidu.com";
								AppConnect.getInstance(DemoApp.this).showBrowser(DemoApp.this, uriStr);
							}
						}).create().show();
				break;
			case R.id.feedbackButton:
				// 用户反馈
				AppConnect.getInstance(this).showFeedback(this);
				break;
			}
		}
	}

	@Override
	protected void onResume() {
		// 从服务器端获取当前用户的虚拟货币.
		// 返回结果在回调函数getUpdatePoints(...)中处理
		AppConnect.getInstance(this).getPoints(this);
		super.onResume();
	}

	@Override
	protected void onDestroy() {
		// 释放资源，原finalize()方法名修改为close()
		AppConnect.getInstance(this).close();
		super.onDestroy();
	}

	/**
	 * 用于监听插屏广告的显示与关闭
	 */
	// @Override
	// public void onWindowFocusChanged(boolean hasFocus) {
	// super.onWindowFocusChanged(hasFocus);
	// Dialog dialog = AppConnect.getInstance(this).getPopAdDialog();
	// if(dialog != null){
	// if(dialog.isShowing()){
	// // 插屏广告正在显示
	// }
	// dialog.setOnCancelListener(new OnCancelListener(){
	// @Override
	// public void onCancel(DialogInterface dialog) {
	// // 监听插屏广告关闭事件
	// }
	// });
	// }
	// }

	// 创建一个线程
	final Runnable mUpdateResults = new Runnable() {
		public void run() {
			if (pointsTextView != null) {
				pointsTextView.setText(displayPointsText);
			}
		}
	};

	/**
	 * AppConnect.getPoints()方法的实现，必须实现
	 * 
	 * @param currencyName
	 *            虚拟货币名称.
	 * @param pointTotal
	 *            虚拟货币余额.
	 */
	public void getUpdatePoints(String currencyName, int pointTotal) {
		displayPointsText = currencyName + ": " + pointTotal;
		mHandler.post(mUpdateResults);
	}

	/**
	 * AppConnect.getPoints() 方法的实现，必须实现
	 * 
	 * @param error
	 *            请求失败的错误信息
	 */
	public void getUpdatePointsFailed(String error) {
		displayPointsText = error;
		mHandler.post(mUpdateResults);
	}

}