//����activity
// Decompiled by Jad v1.5.8e2. Copyright 2001 Pavel K
// Jad home page: http://kpdus.tripod.com/jad.html
// Decompiler options: packimports(3) fieldsfirst ansi space 
// Source File Name:   SetActivity.java

//����ʱ���activity

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
	static String host = "bcs.duapp.com";// Ĭ�ϲ���
	// static String accessKey = "2f4c285cff80949341438a10dbb774c9";
	// static String secretKey = "06db3c0343c1caa9094f75dc4c374611  ";

	static String accessKey = "";
	static String secretKey = " ";

	static String bucket = "finger-dota-tips";

	static String object = "/Text1001.txt";// �ļ�����

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
	private Dialog mProgressDialogcircle;// �Զ���Բ�ν���Ȧ
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
	public static String hostip; // ����IP
	public static String hostmac; // ����MAC
	private TextView textname = null;

	public void onCreate(Bundle bundle) {

		super.onCreate(bundle);

		// ǿ��ȫ��
		getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
				WindowManager.LayoutParams.FLAG_FULLSCREEN);
		// ��������ģʽ
		setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_BEHIND);
		setContentView(R.layout.bbs);

		Intent localIntent = getIntent();// ���Intent

		String fileName = localIntent.getStringExtra("ObjectName");

		 object ="/" + fileName+".txt";

		//Toast.makeText(ThisContext, object, Toast.LENGTH_LONG).show();
		JugeWeb();
	}

	private void JugeWeb() {

		int i = CheckNetworkState();

		switch (i) {

		case 0: {
			Toast.makeText(ThisContext, "��������û�κ����ӣ���鿴���ֻ����������ӣ�",
					Toast.LENGTH_LONG).show();
			break;
		}
		// Wifi
		case 1: {

			// Toast.makeText(ThisContext, "�ף����Եȣ�",
			// Toast.LENGTH_LONG).show();
			// Intent intent = new Intent(ThisContext,
			// _GetWebResoureActivity.class);
			// startActivity(intent);
			// ͨ��AndroidManifest�ļ���ȡWAPS_ID��WAPS_PID
			// AppConnect.getInstance(ThisContext); //
			// ����ȷ��AndroidManifest�ļ���������WAPS_ID
			// findView();
			// SetListener();
			// GetIpAndMAC();// ��ȡip
			// JudgeIpBlackList();
			// GetPhoneInfo();

			// GetBBSContent();// �ӷ������˻�ȡ����
			// textname.setText(objectMetadataString);// ��������ʾ��textView��
			// �½�һ������
			PageTask task = new PageTask(ThisContext);
			// ��������

			task.execute();
			break;
		}
		// 3G
		case 2: {

			// Toast.makeText(ThisContext, "�ף���ʹ�õ���3G���磬�����ٶȻ�������������ʹ��wifi���磡��",
			// Toast.LENGTH_LONG).show();
			// Intent intent = new Intent(ThisContext,
			// _GetWebResoureActivitySeach.class);
			// startActivity(intent);
			// ͨ��AndroidManifest�ļ���ȡWAPS_ID��WAPS_PID
			// AppConnect.getInstance(ThisContext); //
			// ����ȷ��AndroidManifest�ļ���������WAPS_ID
			// findView();
			// SetListener();
			// JudgeIpBlackList();
			//
			// GetIpAndMAC();// ��ȡip
			// GetPhoneInfo();
			// GetBBSContent();// �ӷ������˻�ȡ����
			// textname.setText(objectMetadataString);// ��������ʾ��textView��
			// �½�һ������
			PageTask task = new PageTask(ThisContext);
			// ��������
			task.execute();
			break;
		}
		}
	}

	// �������״̬
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

		// ���3G��2G������״̬�����ӵģ��򷵻�2
		if (mobile == State.CONNECTED || mobile == State.CONNECTING) {

			return 2;
		}
		//û������
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
		// bucket = "pluto2003ub";// �����ַ��0�ĳ���O
		//
		// } else {
		// for (int i = 0; i < sArray.length; i++) {
		// tempblackiplist = sArray[i];
		// if (tempblackiplist.equals(hostip)) {
		// bucket = "pluto2OO3ub";// �����ַ��0�ĳ���O
		// // Toast.makeText(ThisContext, bucket,
		// // Toast.LENGTH_LONG).show();
		//
		// // object = "/object_hero_102.txt";
		// }
		// }
		// }

	}

	private void GetPhoneInfo() {
		// �ֻ��ͺźͰ汾�ţ�
		PhoneVersionString = android.os.Build.MODEL + "��" + "v_"
				+ android.os.Build.VERSION.RELEASE;

		// �ֻ��ͺ�
		PhoneVersionString = android.os.Build.MODEL + "";

	}

	private void SetListener() {
		// ȷ����ť
		localButton1.setOnClickListener(new View.OnClickListener() {
			public void onClick(View paramView) {
				FirstString = "no_first";
				GetDate();// ��ȡ��������

				shoumingtemp = editname.getText().toString();
				if (shoumingtemp.equals("")) {
					Toast.makeText(getApplicationContext(), "���벻��Ϊ��",
							Toast.LENGTH_SHORT).show();

				} else {

					// �½�һ������
					PageTask task = new PageTask(ThisContext);
					// ��������
					task.execute();
				}

			}
		});
		// ���ذ�ť
		localButton2.setOnClickListener(new View.OnClickListener() {
			public void onClick(View paramView) {
				BBSActivity.this.finish();

			}
		});

	}

	// �����ϴ�������
	private static File createSampleFile() {
		try {

			File file = File.createTempFile("java-sdk-", ".txt");
			file.deleteOnExit();

			Writer writer = new OutputStreamWriter(new FileOutputStream(file));
			// writer.write(shoumingtemp);

			writer.append("��" + PhoneVersionString + "��" + hostip + "��" + "\n"
					+ "" + "        " + shoumingtemp + "\n" + "��" + datetimestr
					+ "��" + "\n" + "\n" + "\n");
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
		Date curDate = new Date(System.currentTimeMillis());// ��ȡ��ǰʱ��
		datetimestr = formatter.format(curDate);

	}

	private void GetIpAndMAC() {
		hostip = GetNetIp(); // ��ȡ����IP
		// hostip = getLocalIpAddress(); // ��ȡ����IP
		hostmac = getLocalMacAddress();// ��ȡ����MAC

	}

	// ��������
	private void SetBBSContent() {

		BCSCredentials credentials = new BCSCredentials(accessKey, secretKey);
		BaiduBCS baiduBCS = new BaiduBCS(credentials, host);
		// baiduBCS.setDefaultEncoding("GBK");
		baiduBCS.setDefaultEncoding("UTF-8"); // Default UTF-8

		try {

			putObjectByFile(baiduBCS);// ������з�������

		} catch (BCSServiceException e) {
			// log.warn("Bcs return:" + e.getBcsErrorCode() + ", " +
			// e.getBcsErrorMessage() + ", RequestId=" + e.getRequestId());
		} catch (BCSClientException e) {
			e.printStackTrace();
		}

	}

	public static void putObjectByFile(BaiduBCS baiduBCS) {
		// λ�ã�СͰ����������
		PutObjectRequest request = new PutObjectRequest(bucket, object,
				createSampleFile());
		ObjectMetadata metadata = new ObjectMetadata();
		// metadata.setContentType("text/html");
		request.setMetadata(metadata);

		// ����������Ϊʹ�ã���ᱨ��
		try {

			BaiduBCSResponse<ObjectMetadata> response = baiduBCS// ��������ݷŽ���ض����У�����ֵû��ʹ��
					.putObject(request);
			// ObjectMetadata objectMetadata = response.getResult();// û�ðɣ�

		} catch (Error e) {

		} catch (Exception e) {

		}
		// �ύ���ݺ�����Ҫ������ʾ����Ҫ�ٴӷ������˻�ȡ����
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
				// �ӷ����Ľ������ȡ��IP��ַ
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

	// Բ�ι���������
	// �Զ����첽�������
	class PageTask extends
			AsyncTask<String, Integer, List<Map<String, Object>>> {

		// �ɱ䳤�������������AsyncTask.exucute()��Ӧ
		ProgressDialog pdialog;

		// �ɱ䳤�������������AsyncTask.exucute()��Ӧ

		// �Զ���ķ���
		// ��Ҫ��ɽ�����������
		// ���๹�캯��
		public PageTask(final Context ThisContext) {

		}

		// ��̨������һ���߳���ִ�У�����ʱ
		@Override
		protected List<Map<String, Object>> doInBackground(String... params) {

			if (FirstString.equals("first")) {
				findView();
				SetListener();
				GetIpAndMAC();// ��ȡip
				JudgeIpBlackList();
				GetPhoneInfo();
				GetBBSContent();// �ӷ������˻�ȡ����

			} else {

				JudgeIpBlackList();
				SetBBSContent();
				GetBBSContent();// �ӷ������˻�ȡ����
			}

			return null;
		}

		// �����߳��У�����ִ��UI��ز���
		@Override
		protected void onPostExecute(List<Map<String, Object>> result) {

			textname.setText(objectMetadataString);
			editname.setText("");// ��EditText���
			if (pdialog != null) {
				pdialog.dismiss();
			}
		}

		// ����ִ��
		@Override
		protected void onPreExecute() {
			pdialog = new ProgressDialog(ThisContext, 0);
			pdialog.setProgressStyle(ProgressDialog.STYLE_SPINNER);
			pdialog.setTitle("������");
			pdialog.setMessage("���Ժ�...");
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
		// ���·����������ͷ�SDKռ�õ�ϵͳ��Դ
		AppConnect.getInstance(this).close();
	}

}
