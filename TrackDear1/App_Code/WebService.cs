using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.UI.HtmlControls;


/// <summary>
/// WebService 的摘要描述
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WebService : System.Web.Services.WebService
{

    [WebMethod]
    public string select_test1(string name)
    {
        string num = "", tx1 = "", tx2 = "", tx3 = "";  //測試App所需的變數
        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        s_com.CommandText = "SELECT * FROM userinfo where username='" + name + "'";
        s_com.Connection = conn;
        SqlDataReader s_read = s_com.ExecuteReader();

        while (s_read.Read())
        {
            if (s_read["id"].ToString() != "")
            {
                num = s_read["id"].ToString();
                tx1 = s_read["gender"].ToString();
                tx2 = s_read["birth"].ToString();
                if (s_read["account introduction"].ToString() != "")
                {
                    tx3 = s_read["account introduction"].ToString();
                }
                else
                {
                    tx3 = "無";
                }
                fg = true;
            }
        }
        conn.Close(); //關閉連線
        if (fg == true)
            return (num + "%" + tx1 + "%" + tx2 + "%" + tx3); //加上%只是為了傳送到android後用split分割字串用
        else
            return ("error");
    }
    [WebMethod]
    public int insert_test1(string UserName, string Email, string Gender, string Birth, string Word)
    {
        //string num = "", tx1 = "", tx2 = "", tx3 = "", name="";  //測試App所需的變數
        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();
        string q = "INSERT INTO 用戶(用戶名稱, 帳號,性別,生日,帳號簡介) VALUES('" + UserName + "', '" + Email + "', '" + Gender + "', '" + Birth + "', '" + Word + "')";
        SqlCommand s_com = new SqlCommand(q,conn);
        
        //s_com.Connection = conn;
        //SqlDataReader s_read = s_com.ExecuteReader();

        int row= s_com.ExecuteNonQuery();
        conn.Close(); //關閉連線
        return 45;
        //if (fg == true)
            //return (row); //加上%只是為了傳送到android後用split分割字串用
        //else
            //return 88;
    }
    [WebMethod]
    public string insert_test3(string UserName, string Email, string Gender, string Birth, string Word)
    {
        string num = "", tx1 = "", tx2 = "", tx3 = "";  //測試App所需的變數
        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        s_com.CommandText = "INSERT INTO 用戶(用戶名稱, 帳號, 性別, 生日, 帳號簡介) VALUES('" + UserName + "', '" + Email + "', '" + Gender + "', '" + Birth + "', '" + Word + "')";
        s_com.Connection = conn;
        string ans=s_com.ExecuteNonQuery().ToString();
      
        conn.Close(); //關閉連線
        
        return (ans); //加上%只是為了傳送到android後用split分割字串用
    }
    [WebMethod]
    public string insert_gpslocation(string GNRMC)
    {
        string date = "", time = "", ns = "", we = "", deviceid = "";
        double lat1 = 0, lat2 = 0, lng1 = 0, lng2 = 0;
        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        string[] words = GNRMC.Split(',');
        deviceid = words[0].Substring(0, 12);
        date = "20" + words[9].Substring(4, 2) + "/" + words[9].Substring(2,2)+"/"+words[9].Substring(0,2);
        ns = words[4];
        lat1 = Convert.ToDouble(words[3].Substring(0,2));
        lat2 = lat1+(Convert.ToDouble(words[3].Substring(2))/60);
        we = words[6];
        lng1 = Convert.ToDouble(words[5].Substring(0, 3));
        lng2 = lng1 + (Convert.ToDouble(words[5].Substring(3)) / 60);
        time = ((int)(lng1 / 15) + Convert.ToDouble(words[1].Substring(0, 2))).ToString() + ":" + words[1].Substring(2, 2) + ":" + words[1].Substring(4, 2);
        s_com.CommandText = "INSERT INTO devicelocation(裝置編號,日期,時間,南北緯,緯度,東西經,經度) VALUES('"+deviceid+"','" + date + "', '" + time + "', '" + ns + "', " + lat2 + ", '" + we + "',"+lng2+")";
        s_com.Connection = conn;
        string ans = s_com.ExecuteNonQuery().ToString();
        conn.Close(); //關閉連線

        return (ans+words[0].Substring(0,12)); //加上%只是為了傳送到android後用split分割字串用
    }
    [WebMethod]
    public string personinfoselect(string id)
    {
        string email = "", phone = "", birth = "", name = "", gender="", intro="", photo="", qr="" ;  //測試App所需的變數
        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        s_com.CommandText = "SELECT * FROM userinfo where id='" + id + "'";
        s_com.Connection = conn;
        SqlDataReader s_read = s_com.ExecuteReader();

        while (s_read.Read())
        {
            if (s_read["id"].ToString() != "")
            {
                name = s_read["username"].ToString();
                email = s_read["id"].ToString();
                phone = s_read["phone"].ToString();
                birth = s_read["birth"].ToString();
                photo = s_read["photo"].ToString();
                gender = s_read["gender"].ToString();
                if (s_read["account introduction"].ToString() != "")
                { intro = s_read["account introduction"].ToString(); }
                else
                { intro = "無"; }
                qr = s_read["qrcode"].ToString();
                fg = true;
            }
        }
        conn.Close(); //關閉連線
        if (fg == true)
            return (name + "%" + email + "%" + phone + "%" + birth + "%" + gender + "%" + intro + "%" + photo + "%" + qr); //加上%只是為了傳送到android後用split分割字串用
        else
            return ("error");
    }
    [WebMethod]
    public string select_devicelocation(string id)
    {
        string date = "", time = "", ns = "", we = "", lat = "", lng = "", uuid="";  //lat緯度 lng經度
        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        s_com.CommandText = "SELECT TOP 1 * FROM devicelocation where 裝置編號 = '" + id + "' order by 裝置位置流水號 desc";
        
        s_com.Connection = conn;
        SqlDataReader s_read = s_com.ExecuteReader();

        while (s_read.Read())
        {
            if (s_read["裝置編號"].ToString() != "")
            {
                date = Convert.ToDateTime(s_read["日期"]).ToString("yyyy/MM/dd");
                time = s_read["時間"].ToString();
                ns = s_read["南北緯"].ToString();
                we = s_read["東西經"].ToString();
                lat = s_read["緯度"].ToString();
                lng = s_read["經度"].ToString();
                uuid = s_read["裝置編號"].ToString();
                fg = true;
            }
        }
        conn.Close(); //關閉連線
        if (fg == true)
            return (date + "%" + time + "%" + ns + "%" + we + "%" + lat + "%" + lng + "%" + uuid); //加上%只是為了傳送到android後用split分割字串用
        else
            return ("error");
    }
    [WebMethod]
    public string personinfoupdate(string UserName, string Email, string Phone, string Birth, string Gender, string Word, string imgurl)
    {
        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        s_com.CommandText = "UPDATE userinfo SET username ='" + UserName + "', phone='" +Phone+"', birth='"+ Birth + "', gender='" + Gender + "', [account introduction]='" + Word + "', photo='" + imgurl + "' WHERE(id='"+Email+"')";
        s_com.Connection = conn;
        string ans = s_com.ExecuteNonQuery().ToString();

        conn.Close(); //關閉連線
        return (ans); //加上%只是為了傳送到android後用split分割字串用
    }
    [WebMethod]
    public string savepicturetest(string Email, string imgurl)
    {
        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        s_com.CommandText = "UPDATE userinfo SET qrcode ='" + imgurl +"' WHERE(id='" + Email + "')";
        s_com.Connection = conn;
        string ans = s_com.ExecuteNonQuery().ToString();

        conn.Close(); //關閉連線

        return (ans); //加上%只是為了傳送到android後用split分割字串用

    }
   [WebMethod]
    public string see(string Email)
    {
        string photo = "";  //測試App所需的變數
        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        s_com.CommandText = "SELECT * FROM userinfo where id='" + Email + "'";
        s_com.Connection = conn;
        SqlDataReader s_read = s_com.ExecuteReader();

        while (s_read.Read())
        {
            if (s_read["id"].ToString() != "")
            {
                photo = s_read["photo"].ToString();
                fg = true;
            }
        }
        conn.Close(); //關閉連線
        if (fg == true)
            return (photo); //加上%只是為了傳送到android後用split分割字串用
        else
            return ("error");
    }
    [WebMethod]
    public List<string> homerecyclrview(string Username)
    {
        string friendname = "", hasfriend = "";
        List<string> friendlist = new List<string>();
        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        s_com.CommandText = "Select AccountID1 from FriendRequest where AccountID1='"+Username+"' or AccountID2='"+Username+"'";
        s_com.Connection = conn;
        SqlDataReader s_read = s_com.ExecuteReader();

        while (s_read.Read())
        {
            if (s_read.HasRows)
            {
                hasfriend = "true";
            }
            else { hasfriend = "false"; }
            fg = true;
        }
        conn.Close();
        SqlConnection conn2 = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        conn2.Open();
        SqlCommand s_com2 = new SqlCommand();
        if(hasfriend=="true")
        {
            s_com2.CommandText = "SELECT AccountID1 as friend FROM FriendRequest WHERE (AccountID2 = '" + Username + "' AND status = 1) UNION SELECT AccountID2 as friend FROM FriendRequest WHERE (AccountID1 = '" + Username + "' AND status = 1)";
            s_com2.Connection = conn2;
            SqlDataReader s_read2 = s_com2.ExecuteReader();
            while (s_read2.Read())
            {
                if (s_read2["friend"].ToString() != "")
                {
                    friendname = s_read2["friend"].ToString();
                    friendlist.Add(friendname);
                    fg = true;
                }
            }
            conn2.Close(); //關閉連線
        }
        if (fg == true)
            return (friendlist); //加上%只是為了傳送到android後用split分割字串用
        else
            return (friendlist);
    }
    [WebMethod]
    public string homerecyclrview2(string Username)
    {
        string devicenamelist = "";  //測試App所需的變數
        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        s_com.CommandText = "SELECT distinct STUFF((select '%' + [裝置編號] FROM device as b where a.裝置編號 = b.裝置編號 FOR XML PATH('')), 1,1, '') AS devicelist, a.管理帳號 as 管理帳號 FROM device as a where 管理帳號 = '" + Username + "'";
        s_com.Connection = conn;
        SqlDataReader s_read = s_com.ExecuteReader();

        while (s_read.Read())
        {
            if (s_read["管理帳號"].ToString() != "")
            {
                devicenamelist = s_read["devicelist"].ToString();
                fg = true;
            }
        }
        conn.Close(); //關閉連線
        if (fg == true)
            return (devicenamelist); //加上%只是為了傳送到android後用split分割字串用
        else
            return ("error");
    }
    [WebMethod]
    public string deviceinfoselect(string deviceid)
    {
        string uuid = "", distance = "", birth = "", name = "", gender = "", intro = "", photo = "", manager="";  //測試App所需的變數
        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        s_com.CommandText = "SELECT * FROM device where 裝置編號='" + deviceid + "'";
        s_com.Connection = conn;
        SqlDataReader s_read = s_com.ExecuteReader();

        while (s_read.Read())
        {
            if (s_read["裝置編號"].ToString() != "")
            {
                name = s_read["裝置名稱"].ToString();
                manager = s_read["管理帳號"].ToString();
                uuid = s_read["裝置編號"].ToString();
                distance = s_read["安全範圍"].ToString();
                birth = s_read["裝置生日"].ToString();
                photo = s_read["裝置圖像"].ToString();
                gender = s_read["裝置性別"].ToString();
                if (s_read["裝置介紹"].ToString() != "")
                {
                    intro = s_read["裝置介紹"].ToString();
                }
                else
                {
                    intro = "無";
                }
                fg = true;
            }
        }
        conn.Close(); //關閉連線
        if (fg == true)
            return (name + "%" + manager + "%" + uuid + "%" + birth + "%" + gender + "%" + distance + "%" + intro + "%" + photo); //加上%只是為了傳送到android後用split分割字串用
        else
            return ("error");
    }
    [WebMethod]
    public string deviceinfoupdate(string DeviceName, string UUID, string Birth, string Gender, string Distance, string Word, string imgurl)
    {
        Boolean fg = false; //避免沒抓到資料出現錯誤
        int distance = int.Parse(Distance);
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        s_com.CommandText = "UPDATE device SET 裝置名稱 ='" + DeviceName + "', 裝置生日='" + Birth + "', 裝置性別='" + Gender + "', 安全範圍=" + distance+ ", 裝置介紹='" + Word + "', 裝置圖像='" + imgurl + "' WHERE(裝置編號='" + UUID + "')";
        s_com.Connection = conn;
        string ans = s_com.ExecuteNonQuery().ToString();

        conn.Close(); //關閉連線
        return (ans); //加上%只是為了傳送到android後用split分割字串用
    }
    [WebMethod]
    public string signupornot(string Email)
    {
        string exist="";  //測試App所需的變數
        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        s_com.CommandText = "Select id from userinfo where id='"+Email+"'";
        s_com.Connection = conn;
        SqlDataReader s_read = s_com.ExecuteReader();

        while (s_read.Read())
        {
            if (s_read.HasRows)
            {
                exist = "ture";
            }
            else { exist = "false"; }
            fg = true;
        }
        conn.Close(); //關閉連線
        if (fg == true)
            return (exist); //加上%只是為了傳送到android後用split分割字串用
        else
            return ("error");
    }
    [WebMethod]
    public string personinfoinsert(string UserName, string Email, string Phone, string Birth, string Gender, string Word, string imgurl)
    {
        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        s_com.CommandText = "INSERT INTO userinfo(username, id, phone, birth, gender, [account introduction], photo) VALUES('" + UserName + "','" + Email + "','" + Phone + "','" + Birth + "','" + Gender + "','" + Word + "','" + imgurl + "');";
        s_com.Connection = conn;
        string ans = s_com.ExecuteNonQuery().ToString();

        conn.Close(); //關閉連線

        return (ans); //加上%只是為了傳送到android後用split分割字串用

    }
    [WebMethod]
    public string deviceinfoinsert(string DeviceName, string Email, string UUID, string Birth, string Gender, string Distance, string privategps, string Word, string imgurl)
    {
        Boolean fg = false; //避免沒抓到資料出現錯誤
        int distance = int.Parse(Distance);
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        s_com.CommandText = "INSERT INTO device(裝置名稱,管理帳號,裝置編號,裝置性別,裝置生日,安全範圍,是否公開位置,裝置介紹,裝置圖像,近距離狀態) VALUES('" + DeviceName + "','" + Email + "','" + UUID + "','" + Gender + "','" + Birth + "'," + distance + ",'" + privategps + "','" + Word + "','" + imgurl + "',0);";
        s_com.Connection = conn;
        string ans = s_com.ExecuteNonQuery().ToString();

        conn.Close(); //關閉連線

        return (ans); //加上%只是為了傳送到android後用split分割字串用
    }
    [WebMethod]
    public List<string> selectfriend(string username)
    {
        string photo = "", email="";  //測試App所需的變數 
        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        s_com.CommandText = "SELECT id,photo FROM userinfo where username='" + username + "'";
        s_com.Connection = conn;
        SqlDataReader s_read = s_com.ExecuteReader();
        List<string> wordsList = new List<string>();
        while (s_read.Read())
        {
            if (s_read["photo"].ToString() != "")
            {
                email = s_read["id"].ToString();
                photo = s_read["photo"].ToString();
                wordsList.Add(email+"%"+photo);
                fg = true;
            }
        }
        conn.Close(); //關閉連線
        if (fg == true)
            return (wordsList); //加上%只是為了傳送到android後用split分割字串用
        else
            return (wordsList);
    }
    [WebMethod]
    public string insert_CDs(double CDs)
    {
        string value ="" ;
        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        s_com.CommandText = "INSERT INTO CDs(數值) VALUES(" + CDs + ")";
        s_com.Connection = conn;
        string ans = s_com.ExecuteNonQuery().ToString();
        conn.Close(); //關閉連線
        return (ans); //加上%只是為了傳送到android後用split分割字串用
    }
    [WebMethod]
    public string insert_CDs2(int CDs)
    {
        string value = "";
        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        s_com.CommandText = "INSERT INTO CDs2(數值) VALUES(" + CDs + ")";
        s_com.Connection = conn;
        string ans = s_com.ExecuteNonQuery().ToString();
        conn.Close(); //關閉連線
        return (ans); //加上%只是為了傳送到android後用split分割字串用
    }
    [WebMethod]
    public string addfriend(string user1, string user2, int status, string actionuser)
    {
        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        s_com.CommandText = "INSERT INTO FriendRequest(AccountID1, AccountID2, status, action_user_id) VALUES('" + user1 + "','" + user2 + "',0, '" + actionuser + "')";
        s_com.Connection = conn;
        string ans = s_com.ExecuteNonQuery().ToString();
        conn.Close(); //關閉連線
        return (ans); //加上%只是為了傳送到android後用split分割字串用
    }
    [WebMethod]
    public List<string> noticefriendrequest(string useremail)
    {
        string photo = "", email = "", requestname = "";  //測試App所需的變數 
        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        
        s_com.CommandText = "SELECT AccountID1 as request FROM FriendRequest WHERE((AccountID1 != '"+ useremail +"' AND AccountID2 = '"+ useremail +"') AND status = 0 AND action_user_id != '"+ useremail +"') UNION SELECT AccountID2 FROM FriendRequest WHERE((AccountID1 = '"+ useremail +"'AND AccountID2 != '"+ useremail +"') AND status = 0 AND action_user_id != '"+ useremail +"')";
        s_com.Connection = conn;
        SqlDataReader s_read = s_com.ExecuteReader();
        List<string> wordsList = new List<string>();
        List<string> wordsList2 = new List<string>();
        while (s_read.Read())
        {
            if (s_read["request"].ToString() != "")
            {
                email = s_read["request"].ToString();
                wordsList.Add(email);
                fg = true;
            }
        }
        conn.Close();
        for (int i=0; i<wordsList.Count; i++)
        {
            SqlConnection conn2 = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
            conn2.Open();
            SqlCommand s_com2 = new SqlCommand();
            s_com2.CommandText = "SELECT username,id,photo FROM userinfo where id='" + wordsList[i] + "'";
            s_com2.Connection = conn2;
            SqlDataReader s_read2 = s_com2.ExecuteReader();
            while (s_read2.Read())
            {
                if (s_read2["photo"].ToString() != "")
                {
                    requestname = s_read2["username"].ToString();
                    email = s_read2["id"].ToString();
                    photo = s_read2["photo"].ToString();
                    wordsList2.Add(requestname + "%" + email + "%" + photo);
                    fg = true;
                }
            }
            conn2.Close(); //關閉連線
        }
        if (fg == true)
            return (wordsList2); //加上%只是為了傳送到android後用split分割字串用
        else
            return (wordsList2);
    }
    [WebMethod]
    public string acceptrequest(string user1, string user2, int status, string actionuser)
    {
        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        s_com.CommandText = "UPDATE FriendRequest SET action_user_id='"+ actionuser +"', status=1 WHERE (AccountID1='"+ user1 +"' AND AccountID2='"+ user2 +"') OR (AccountID1='"+ user2 +"' AND AccountID2='"+ user1 +"');";
        s_com.Connection = conn;
        string ans = s_com.ExecuteNonQuery().ToString();
        conn.Close(); //關閉連線
        return (ans); //加上%只是為了傳送到android後用split分割字串用
    }
    [WebMethod]
    public string beaconopen(string deviceuuid, int open)
    {
        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        s_com.CommandText = "UPDATE device SET 近距離狀態 = " + open + " WHERE 裝置編號='" + deviceuuid + "';";
        s_com.Connection = conn;
        string ans = s_com.ExecuteNonQuery().ToString();
        conn.Close(); //關閉連線
        return (ans); //加上%只是為了傳送到android後用split分割字串用
    }
    [WebMethod]
    public int beaconcheck(string deviceuuid)
    {
        int result = 0;  //測試App所需的變數
        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        s_com.CommandText = "SELECT 近距離狀態 FROM device where 裝置編號='" + deviceuuid + "'";
        s_com.Connection = conn;
        SqlDataReader s_read = s_com.ExecuteReader();

        while (s_read.Read())
        {
            if (s_read["近距離狀態"].ToString() != "")
            {
                result = int.Parse(s_read["近距離狀態"].ToString());
                fg = true;
            }
        }
        conn.Close(); //關閉連線
        if (fg == true)
            return (result); //加上%只是為了傳送到android後用split分割字串用
        else
            return (5);
    }
    [WebMethod]
    public DataTable FriendList(string name)
    {
        DataTable dtTable = new DataTable("friendlist");
        DataRow row;

        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        conn.Open();
        SqlCommand s_com = new SqlCommand();
        s_com.CommandText = "SELECT username,fri_name,text FROM friend WHERE username='" + name + "' ";
        s_com.Connection = conn;
        SqlDataReader s_read = s_com.ExecuteReader();

        DataColumn column = new DataColumn();
        column.AllowDBNull = false;
        // column.ColumnName = "User_name";
        //dtTable.Columns.Add("Id", typeof(System.String));
        dtTable.Columns.Add("用戶", typeof(System.String));
        dtTable.Columns.Add("好友", typeof(System.String));
        dtTable.Columns.Add("訊息", typeof(System.String));
        //dtTable.Columns.Add("頭貼", typeof(System.String));

        while (s_read.Read())
        {
            row = dtTable.NewRow();
            //row["Id"] = s_read["FriendId"].ToString();
            row["用戶"] = s_read["username"].ToString();
            row["好友"] = s_read["fri_name"].ToString();
            row["訊息"] = s_read["text"].ToString();
            //row["頭貼"] = s_read[""].ToString();

            dtTable.Rows.Add(row);
        }
        conn.Close(); //關閉連線
        return dtTable;
    }
    [WebMethod]
    public string beaconupdate(string distance, int rssi)
    {
        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        s_com.CommandText = "UPDATE Beacon SET RSSI =" + rssi + " WHERE(距離='" + distance + "M')";
        s_com.Connection = conn;
        string ans = s_com.ExecuteNonQuery().ToString();

        conn.Close(); //關閉連線

        return (ans); //加上%只是為了傳送到android後用split分割字串用

    }
    [WebMethod]
    public List<string> friendlist(string name)
    {
        string username = "", fri_name = "", text = "", photo = "";

        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        s_com.CommandText = "SELECT * FROM friend WHERE username='" + name + "'";
        s_com.Connection = conn;
        SqlDataReader s_read = s_com.ExecuteReader();
        // string ans = s_com.ExecuteNonQuery().ToString();
        List<string> friendlist = new List<string>();

        while (s_read.Read())
        {
            if (s_read["username"].ToString() != "")
            {

                fri_name = s_read["fri_name"].ToString();
                text = s_read["text"].ToString();
                photo = s_read["photo"].ToString();

                //  Console.WriteLine("username:" + username + "friend:" + fri_name + "message:" + text);
                friendlist.Add(fri_name + "%" + text + "%" + photo);
                fg = true;
            }
        }
        conn.Close(); //關閉連線
        if (fg == true)
            return (friendlist); //加上%只是為了傳送到android後用split分割字串用
        else
            return (friendlist);
    }
    [WebMethod]
    public string messageinsert(string sender, string receiver, string msg, string photo)
    {
        string sendtime="",status="";

        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
           
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        s_com.CommandText = "INSERT INTO message (sender,receiver,message,photo) values('"+sender+"','"+receiver+"','"+msg+"','" + photo + "')";
        s_com.Connection = conn;
            
        string ans = s_com.ExecuteNonQuery().ToString();
          
        conn.Close(); //關閉連線
        if (fg == true)
            return (ans); //加上%只是為了傳送到android後用split分割字串用
        else
            return (ans);
    }
    [WebMethod]
    public List<string> message(string sender)
    {
        string receiver = "", msg = "", hour = "", photo = "", minute = "";

        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();

        s_com.CommandText = "SELECT sender,receiver,message, DATEPART(hh,sendtime) AS Orderhour,DATEPART(mi,sendtime)AS Orderminute,photo FROM message";
        s_com.Connection = conn;
        SqlDataReader s_read = s_com.ExecuteReader();

        List<string> message = new List<string>();

        while (s_read.Read())
        {
            if (s_read["sender"].ToString() != "")
            {

                sender = s_read["sender"].ToString();
                receiver = s_read["receiver"].ToString();
                msg = s_read["message"].ToString();
                hour = s_read["Orderhour"].ToString();
                minute = s_read["Orderminute"].ToString();
                photo = s_read["photo"].ToString();

                //  Console.WriteLine("username:" + username + "friend:" + fri_name + "message:" + text);
                message.Add(sender + "%" + receiver + "%" + msg + "%" + hour + "%" + minute + "%" + photo);
                fg = true;
            }
        }
        conn.Close(); //關閉連線
        if (fg == true)
            return (message); //加上%只是為了傳送到android後用split分割字串用
        else
            return (message);
    }
    [WebMethod]
    public int beaconrssi(string usersetbeacondistance)
    {
        int result = 0;  //測試App所需的變數
        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        s_com.CommandText = "select * from Beacon where 距離 = '" + usersetbeacondistance + "'";
        s_com.Connection = conn;
        SqlDataReader s_read = s_com.ExecuteReader();

        while (s_read.Read())
        {
            if (s_read["RSSI"].ToString() != "")
            {
                result = int.Parse(s_read["RSSI"].ToString());
                fg = true;
            }
        }
        conn.Close(); //關閉連線
        if (fg == true)
            return (result); //加上%只是為了傳送到android後用split分割字串用
        else
            return (5);
    }
    /* [WebMethod]
     public string personinfoupdate(string UserName, string Email, string Phone, string Birth, string Gender, string Word, string imgurl)
     {
         Boolean fg = false; //避免沒抓到資料出現錯誤
         SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
         //連線
         conn.Open();

         SqlCommand s_com = new SqlCommand();
         s_com.CommandText = "UPDATE userinfo SET username ='" + UserName + "', phone='" + Phone + "', birth='" + Birth + "', gender='" + Gender + "', [account introduction]='" + Word + "', photo='" + imgurl + "' WHERE(id='" + Email + "')";
         s_com.Connection = conn;
         string ans = s_com.ExecuteNonQuery().ToString();

         conn.Close(); //關閉連線

         return (ans); //加上%只是為了傳送到android後用split分割字串用

     }*/
    [WebMethod]
    public string history(string userid)
    {
        string devicename = "", uuid = "";

        Boolean fg = false; //避免沒抓到資料出現錯誤
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        //連線
        conn.Open();

        SqlCommand s_com = new SqlCommand();

        s_com.CommandText = "SELECT 裝置名稱,裝置編號 FROM device WHERE 管理帳號='" + userid + "'";
        s_com.Connection = conn;
        SqlDataReader s_read = s_com.ExecuteReader();

        while (s_read.Read())
        {
            if (s_read["管理帳號"].ToString() != "")
            {
                devicename = s_read["裝置名稱"].ToString();
                uuid = s_read["裝置編號"].ToString();

                fg = true;
            }
        }
        conn.Close(); //關閉連線
        if (fg == true)
            return (uuid + "%" + devicename); //加上%只是為了傳送到android後用split分割字串用
        else
            return ("erro");
    }
    [WebMethod]
    public string deletefriendship(string usergmail, string friendgmail)
    {
        Boolean fg = false; 
        SqlConnection conn = new SqlConnection("server=127.0.0.1;uid=test;pwd=1234554321;database=TrackDear");
        conn.Open();

        SqlCommand s_com = new SqlCommand();
        s_com.CommandText = "delete from FriendRequest where (AccountID1 = '" + usergmail + "' and AccountID2 = '" + friendgmail + "') or (AccountID1 = '" + friendgmail + "' and AccountID2 = '" + usergmail + "')";
        s_com.Connection = conn;
        string ans = s_com.ExecuteNonQuery().ToString();

        conn.Close(); 
        return (ans);
    }
}





