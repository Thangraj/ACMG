<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecordingTEST1.aspx.cs" Inherits="ACMGAdmin.Compliance.CallRecordingsByAgent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <SCRIPT type="text/javascript">
        if (-1 != navigator.userAgent.indexOf("MSIE")) {
            document.write('<OBJECT id="Player"');
            document.write(' classid="clsid:6BF52A52-394A-11d3-B153-00C04F79FAA6"');
            document.write(' width=300 height=200>');
        }
        else if (-1 != navigator.userAgent.indexOf("Firefox")) {
            document.write('<OBJECT id="Player"');
            document.write(' type="application/x-ms-wmp"');
            document.write(' width=300 height=200>');
        }
        else if (-1 != navigator.userAgent.indexOf("Chrome")) {
            document.write('<OBJECT id="Player"');
            document.write(' type="application/x-ms-wmp"');
            document.write(' width=300 height=200>');
        }

        alert(navigator.userAgent);
        alert(navigator.userAgent.indexOf("Chrome"));     
    </SCRIPT>

    

   <PARAM NAME="URL" VALUE="D:/ACMGLeads/Recordings/01100120110201080201-FN.wav">

   <PARAM NAME="enabled" VALUE="True">

   <PARAM NAME="AutoStart" VALUE="False">

   <PARAM name="PlayCount" value="3">

   <PARAM name="Volume" value="50">

   <PARAM NAME="balance" VALUE="0">

   <PARAM NAME="Rate" VALUE="1.0">

   <PARAM NAME="Mute" VALUE="False">

   <PARAM NAME="fullScreen" VALUE="False">

   <PARAM name="uiMode" value="full">

</OBJECT>

    </div>
    </form>
</body>
</html>
