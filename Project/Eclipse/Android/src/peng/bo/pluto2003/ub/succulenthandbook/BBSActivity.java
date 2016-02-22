//设置时间的activity

package peng.bo.pluto2003.ub.succulenthandbook;

import java.io.BufferedReader;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.io.Writer;
import java.net.HttpURLConnection;
import java.net.InetAddress;
import java.net.MalformedURLException;
import java.net.NetworkInterface;
import java.net.SocketException;
import java.net.URL;
import java.net.URLConnection;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Enumeration;
import java.util.List;
import java.util.Map;

import cn.waps.AppConnect;

import com.baidu.inf.iis.bcs.BaiduBCS;
import com.baidu.inf.iis.bcs.auth.BCSCredentials;
import com.baidu.inf.iis.bcs.model.BCSClientException;
import com.baidu.inf.iis.bcs.model.BCSServiceException;
import com.baidu.inf.iis.bcs.model.DownloadObject;
import com.baidu.inf.iis.bcs.model.ObjectMetadata;
import com.baidu.inf.iis.bcs.policy.Policy;
import com.baidu.inf.iis.bcs.policy.PolicyAction;
import com.baidu.inf.iis.bcs.policy.PolicyEffect;
import com.baidu.inf.iis.bcs.policy.Statement;
import com.baidu.inf.iis.bcs.request.GetObjectRequest;
import com.baidu.inf.iis.bcs.request.PutObjectRequest;
import com.baidu.inf.iis.bcs.response.BaiduBCSResponse;

import android.app.Activity;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.graphics.drawable.Drawable;
import android.net.ConnectivityManager;
import android.net.NetworkInfo.State;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.os.AsyncTask;
import android.os.Bundle;
import android.text.Editable;
import android.text.Html;
import android.view.Gravity;
import android.view.View;
import android.view.WindowManager;
import android.widget.*;
import android.widget.RadioGroup.OnCheckedChangeListener;

public class BBSActivity extends Activity {

	String FirstString = "first";
	Context ThisContext = BBSActivity.this;
	static String host = "bcs.duapp.com";// 默认不变
	// static String accessKey = "2f4c285cff80949341438a10dbb774c9";
	// static String secretKey = "06db3c0343c1caa9094f75dc4c374611  ";

	static String accessKey = "";
	static String secretKey = " ";

	static String bucket = "finger-dota-tips";

	static String object = "/Text1001.txt";// 文件名字

	// Context ThisContext = BBSActivity.this;
	static String objectMetadataString;
	static ObjectMetadata objectMetadata;

	static File destFile = new File("test");
	static String PhoneVersionString;
	static String NativePhoneNumberString;
	static String ProvidersNameString;

	static int ipAddress;
	String birthday;
	String[] sArray;
	String tempblackiplist;
	private Dialog mProgressDialogcircle;// 自定义圆形进度圈
	static String datetimestr;
	String name;
	static String shoumingtemp;
	String temp;
	String savefile;
	String sex;
	String info;

	Button localButton1;
	Button localButton2;
	EditText editname;
	public static String hostip; // 本机IP
	public static String hostmac; // 本机MAC
	private TextView textname = null;

	public void onCreate(Bundle bundle) {

		super.onCreate(bundle);

		// 强制全屏
		getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
				WindowManager.LayoutParams.FLAG_FULLSCREEN);
		// 设置竖屏模式
		setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_BEHIND);
		setContentView(R.layout.bbs);

		Intent localIntent = getIntent();// 获得Intent

		String fileName = localIntent.getStringExtra("ObjectName");

		 object ="/" + fileName+".txt";

		//Toast.makeText(ThisContext, object, Toast.LENGTH_LONG).show();
		JugeWeb();
	}

	private void JugeWeb() {

		int i = CheckNetworkState();

		switch (i) {

		case 0: {
			Toast.makeText(ThisContext, "您的网络没任何连接，请查看您手机的网络连接！",
					Toast.LENGTH_LONG).show();
			break;
		}
		// Wifi
		case 1: {

			// Toast.makeText(ThisContext, "亲！请稍等！",
			// Toast.LENGTH_LONG).show();
			// Intent intent = new Intent(ThisContext,
			// _GetWebResoureActivity.class);
			// startActivity(intent);
			// 通过AndroidManifest文件读取WAPS_ID和WAPS_PID
			// AppConnect.getInstance(ThisContext); //
			// 必须确保AndroidManifest文件内配置了WAPS_ID
			// findView();
			// SetListener();
			// GetIpAndMAC();// 获取ip
			// JudgeIpBlackList();
			// GetPhoneInfo();

			// GetBBSContent();// 从服务器端获取数据
			// textname.setText(objectMetadataString);// 把数据显示在textView中
			// 新建一个任务
			PageTask task = new PageTask(ThisContext);
			// 启动任务

			task.execute();
			break;
		}
		// 3G
		case 2: {

			// Toast.makeText(ThisContext, "亲！您使用的是3G网络，加载速度会稍慢！建议您使用wifi网络！！",
			// Toast.LENGTH_LONG).show();
			// Intent intent = new Intent(ThisContext,
			// _GetWebResoureActivitySeach.class);
			// startActivity(intent);
			// 通过AndroidManifest文件读取WAPS_ID和WAPS_PID
			// AppConnect.getInstance(ThisContext); //
			// 必须确保AndroidManifest文件内配置了WAPS_ID
			// findView();
			// SetListener();
			// JudgeIpBlackList();
			//
			// GetIpAndMAC();// 获取ip
			// GetPhoneInfo();
			// GetBBSContent();// 从服务器端获取数据
			// textname.setText(objectMetadataString);// 把数据显示在textView中
			// 新建一个任务
			PageTask task = new PageTask(ThisContext);
			// 启动任务
			task.execute();
			break;
		}
		}
	}

	// 检查网络状态
	public int CheckNetworkState() {

		boolean flag = false;
		ConnectivityManager manager = (ConnectivityManager) getSystemService(Context.CONNECTIVITY_SERVICE);
		State mobile = manager.getNetworkInfo(ConnectivityManager.TYPE_MOBILE)
				.getState();
		State wifi = manager.getNetworkInfo(ConnectivityManager.TYPE_WIFI)
				.getState();
		// wifi
		if (wifi == State.CONNECTED || wifi == State.CONNECTING) {
			return 1;
		}

		// 如果3G、2G等网络状态是连接的，则返回2
		if (mobile == State.CONNECTED || mobile == State.CONNECTING) {

			return 2;
		}
		//没有连接
		return 0;
	}

	private void JudgeIpBlackList() {

		// // String value=AppConnect.getInstance(this).getConfig("ipblacklist",
		// // "199.165.2.32,259.215.155.45");
		// String value = AppConnect.getInstance(this).getConfig_Sync(
		// "ipblacklist");
		// // String s = new String("a,b,c,d");
		// sArray = value.split(",");
		//
		// // Toast.makeText(ThisContext, hostip, Toast.LENGTH_LONG).show();
		// if (sArray.equals("")) {
		// bucket = "pluto2003ub";// 错误地址，0改成了O
		//
		// } else {
		// for (int i = 0; i < sArray.length; i++) {
		// tempblackiplist = sArray[i];
		// if (tempblackiplist.equals(hostip)) {
		// bucket = "pluto2OO3ub";// 错误地址，0改成了O
		// // Toast.makeText(ThisContext, bucket,
		// // Toast.LENGTH_LONG).show();
		//
		// // object = "/object_hero_102.txt";
		// }
		// }
		// }

	}

	private void GetPhoneInfo() {
		// 手机型号和版本号：
		PhoneVersionString = android.os.Build.MODEL + "，" + "v_"
				+ android.os.Build.VERSION.RELEASE;

		// 手机型号
		PhoneVersionString = android.os.Build.MODEL + "";

	}

	private void SetListener() {
		// 确定按钮
		localButton1.setOnClickListener(new View.OnClickListener() {
			public void onClick(View paramView) {
				FirstString = "no_first";
				GetDate();// 获取发言日期

				shoumingtemp = editname.getText().toString();
				if (shoumingtemp.equals("")) {
					Toast.makeText(getApplicationContext(), "输入不能为空",
							Toast.LENGTH_SHORT).show();

				} else {

					// 新建一个任务
					PageTask task = new PageTask(ThisContext);
					// 启动任务
					task.execute();
				}

			}
		});
		// 返回按钮
		localButton2.setOnClickListener(new View.OnClickListener() {
			public void onClick(View paramView) {
				BBSActivity.this.finish();

			}
		});

	}

	// 生成上传的数据
	private static File createSampleFile() {
		try {

			File file = File.createTempFile("java-sdk-", ".txt");
			file.deleteOnExit();

			Writer writer = new OutputStreamWriter(new FileOutputStream(file));
			// writer.write(shoumingtemp);

			writer.append("【" + PhoneVersionString + "，" + hostip + "】" + "\n"
					+ "" + "        " + shoumingtemp + "\n" + "【" + datetimestr
					+ "】" + "\n" + "\n" + "\n");
			writer.write(objectMetadataString);
			writer.close();

			return file;
		} catch (IOException e) {
			// log.error("tmp file create failed.");
			return null;
		}
	}

	private void GetDate() {
		SimpleDateFormat formatter = new SimpleDateFormat(
				"yyyy-MM-dd   HH:mm:ss");
		Date curDate = new Date(System.currentTimeMillis());// 获取当前时间
		datetimestr = formatter.format(curDate);

	}

	private void GetIpAndMAC() {
		hostip = GetNetIp(); // 获取外网IP
		// hostip = getLocalIpAddress(); // 获取内网IP
		hostmac = getLocalMacAddress();// 获取本机MAC

	}

	// 设置内容
	private void SetBBSContent() {

		BCSCredentials credentials = new BCSCredentials(accessKey, secretKey);
		BaiduBCS baiduBCS = new BaiduBCS(credentials, host);
		// baiduBCS.setDefaultEncoding("GBK");
		baiduBCS.setDefaultEncoding("UTF-8"); // Default UTF-8

		try {

			putObjectByFile(baiduBCS);// 向对象中放入内容

		} catch (BCSServiceException e) {
			// log.warn("Bcs return:" + e.getBcsErrorCode() + ", " +
			// e.getBcsErrorMessage() + ", RequestId=" + e.getRequestId());
		} catch (BCSClientException e) {
			e.printStackTrace();
		}

	}

	public static void putObjectByFile(BaiduBCS baiduBCS) {
		// 位置：小桶、对象、内容
		PutObjectRequest request = new PutObjectRequest(bucket, object,
				createSampleFile());
		ObjectMetadata metadata = new ObjectMetadata();
		// metadata.setContentType("text/html");
		request.setMetadata(metadata);

		// 若这里设置为使用，则会报错！
		try {

			BaiduBCSResponse<ObjectMetadata> response = baiduBCS// 这里把内容放进相关对象中，返回值没有使用
					.putObject(request);
			// ObjectMetadata objectMetadata = response.getResult();// 没用吧？

		} catch (Error e) {

		} catch (Exception e) {

		}
		// 提交内容后，由于要重新显示，需要再从服务器端获取数据
		try {

			getObjectMetadata(baiduBCS);

		} catch (BCSServiceException e) {

		} catch (BCSClientException e) {
			e.printStackTrace();
		}

	}

	public static String GetNetIp() {
		URL infoUrl = null;
		InputStream inStream = null;
		try {
			// http://iframe.ip138.com/ic.asp
			// infoUrl = new URL("http://city.ip138.com/city0.asp");
			infoUrl = new URL("http://iframe.ip138.com/ic.asp");
			URLConnection connection = infoUrl.openConnection();
			HttpURLConnection httpConnection = (HttpURLConnection) connection;
			int responseCode = httpConnection.getResponseCode();
			if (responseCode == HttpURLConnection.HTTP_OK) {
				inStream = httpConnection.getInputStream();
				BufferedReader reader = new BufferedReader(
						new InputStreamReader(inStream, "utf-8"));
				StringBuilder strber = new StringBuilder();
				String line = null;
				while ((line = reader.readLine()) != null)
					strber.append(line + "\n");
				inStream.close();
				// 从反馈的结果中提取出IP地址
				int start = strber.indexOf("[");
				int end = strber.indexOf("]", start + 1);
				line = strber.substring(start + 1, end);
				return line;
			}
		} catch (MalformedURLException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
		return null;
	}

	private void GetBBSContent() {
		BCSCredentials credentials = new BCSCredentials(accessKey, secretKey);
		BaiduBCS baiduBCS = new BaiduBCS(credentials, host);
		// baiduBCS.setDefaultEncoding("GBK");
		baiduBCS.setDefaultEncoding("UTF-8"); // Default UTF-8

		try {

			getObjectMetadata(baiduBCS);

		} catch (BCSServiceException e) {

		} catch (BCSClientException e) {
			e.printStackTrace();
		}

	}

	public static void getObjectMetadata(BaiduBCS baiduBCS) {

		GetObjectRequest request = new GetObjectRequest(bucket, object);

		BaiduBCSResponse<DownloadObject> response = baiduBCS.getObject(request);
		DownloadObject objectMetadata = response.getResult();
		InputStream temp = objectMetadata.getContent();

		ByteArrayOutputStream baos = new ByteArrayOutputStream();
		int i = -1;
		try {
			while ((i = temp.read()) != -1) {
				baos.write(i);
			}
		} catch (IOException e) {

			e.printStackTrace();
		}

		objectMetadataString = baos.toString();
	}

	public String getLocalIpAddress() {
		try {
			for (Enumeration<NetworkInterface> en = NetworkInterface
					.getNetworkInterfaces(); en.hasMoreElements();) {
				NetworkInterface intf = en.nextElement();
				for (Enumeration<InetAddress> enumIpAddr = intf
						.getInetAddresses(); enumIpAddr.hasMoreElements();) {
					InetAddress inetAddress = enumIpAddr.nextElement();
					if (!inetAddress.isLoopbackAddress()) {
						return inetAddress.getHostAddress().toString();
					}
				}
			}
		} catch (SocketException ex) {
			// Log.e("WifiPreference IpAddress", ex.toString());
		}
		return null;
	}

	public String getLocalMacAddress() {
		WifiManager wifi = (WifiManager) getSystemService(Context.WIFI_SERVICE);
		WifiInfo info = wifi.getConnectionInfo();
		return info.getMacAddress();
	}

	private void findView() {

		editname = (EditText) findViewById(R.id.editname);
		textname = (TextView) findViewById(R.id.textname);
		localButton1 = (Button) findViewById(R.id.buttonsave);
		localButton2 = (Button) findViewById(R.id.buttonback);
	}

	// public void onPause() {
	// super.onPause();
	// // overridePendingTransition(R.anim.slide_in_right,
	// R.anim.slide_out_left);
	// }

	// 圆形滚动进度条
	// 自定义异步任务的类
	class PageTask extends
			AsyncTask<String, Integer, List<Map<String, Object>>> {

		// 可变长的输入参数，与AsyncTask.exucute()对应
		ProgressDialog pdialog;

		// 可变长的输入参数，与AsyncTask.exucute()对应

		// 自定义的方法
		// 主要完成进度条的任务
		// 本类构造函数
		public PageTask(final Context ThisContext) {

		}

		// 后台，另外一个线程中执行，不耗时
		@Override
		protected List<Map<String, Object>> doInBackground(String... params) {

			if (FirstString.equals("first")) {
				findView();
				SetListener();
				GetIpAndMAC();// 获取ip
				JudgeIpBlackList();
				GetPhoneInfo();
				GetBBSContent();// 从服务器端获取数据

			} else {

				JudgeIpBlackList();
				SetBBSContent();
				GetBBSContent();// 从服务器端获取数据
			}

			return null;
		}

		// 在主线程中，可以执行UI相关操作
		@Override
		protected void onPostExecute(List<Map<String, Object>> result) {

			textname.setText(objectMetadataString);
			editname.setText("");// 把EditText清空
			if (pdialog != null) {
				pdialog.dismiss();
			}
		}

		// 首先执行
		@Override
		protected void onPreExecute() {
			pdialog = new ProgressDialog(ThisContext, 0);
			pdialog.setProgressStyle(ProgressDialog.STYLE_SPINNER);
			pdialog.setTitle("加载中");
			pdialog.setMessage("请稍后...");
			pdialog.setIndeterminate(false);
			pdialog.show();
		}

		@Override
		protected void onProgressUpdate(Integer... values) {

		}
	}

	@Override
	protected void onDestroy() {

		super.onDestroy();
		// 以下方法将用于释放SDK占用的系统资源
		AppConnect.getInstance(this).close();
	}

}
