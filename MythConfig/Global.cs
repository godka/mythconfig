using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace MythConfig
{
    public static class Global
    {
        public static string serverip = "120.204.70.218";
        public static int serverport = 5830;
        public static int uid = -1;
        public static string loginrequest = "select userid from users where username='{0}' and userpwd='{1}'";
        public static string levelrequest = "SELECT topGroupID,topGroupName,topgroupparent FROM topGroups where USERID = {0} order by topGroupID asc";
        public static string tripplesteprequest = "SELECT a.CameraPTZ,a.UserCameraID, b.cameraid , b.CAMERANUM,b.name,b.subname, b.port,b.ptzcontrol,b.SerialPort,b.audio, c.httpport,c.ip,c.vstypeid,c.name As ServerName,c.videoserverid,d.name AS groupname,d.groupid,D.type as grouptype ,b.ptzcontrol,b.audio,b.description,c.username,c.password,CASE when MultiCastIP is NULL or MultiCastIP = ''  then '' else (select externalip from server where serverid = multicastIP) end as MultiCastIP FROM UserCamera a, Camera b, videoserver c, groups d WHERE d.userid = {0}  AND a.groupid = d.groupid AND a.cameraid = b.cameraid AND b.videoserverid = c.videoserverid order by d.Description asc,d.groupid asc,a.usercameraid asc";


        public static string selectCamera = "select a.cameraID as ID,a.Name,b.ip,a.SubName as port,c.VsType as VideoType from Camera as a,VideoServer as b,VSType as c where a.VideoServerID = b.VideoServerID and c.VsTypeID = b.vstypeid;";
        public static string selectVsType = "select * from VsType";
        public static class StaticForms
        {
            public static frmTreeEdit TreeEdit = null;
        }
    }
}
