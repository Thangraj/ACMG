using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace ACMGAdmin.Compliance
{
    public partial class CallRecordingsSales : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (txtStartDate.Text == "") { txtStartDate.Text = DateTime.Now.ToString("MM/dd/yyyy"); }
			if (txtEndDate.Text == "") { txtEndDate.Text = DateTime.Now.ToString("MM/dd/yyyy"); }
            
		}

		protected void RunReport_Click(object sender, EventArgs e)
		{

		   // this.CallRecordingList.DataBind();
		   // GridView1.DataBind();
           
			
		}

		

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }


        protected string getRecordingFiles(Object theFilestring)
        {
            string theLiteralText = "";
            string theFilename = "";
            string theFileString2 = theFilestring.ToString();
            ArrayList theFiles = new ArrayList(); //the  filenames in the directory matching the partial string
            try
            {


                //use first 21 characters
                //get filenames with no path
                //use virtual directory path on server
                string strPath = Server.MapPath("/AgentRecordings");

                //string strPath = "Z:/ACMGCallRecordings/"; //Server.MapPath("/AgentRecordings");// "E:/ACMGCallRecordings/";//theCSVDirectory;
                //get the recording time that should be on the file
                string theFileHour = theFileString2.Substring(16, 2);
                string theFileMin = theFileString2.Substring(18, 2);
                string theFileSec = theFileString2.Substring(20, 2);
                string theFileTime = theFileHour + ":" + theFileMin + ":" + theFileSec;

                //get the minute for 1 minute before and 1 minute after
                DateTime theFileDTime = DateTime.Parse(theFileTime);
                DateTime theStartTime = theFileDTime.AddSeconds(-30);
                DateTime theEndTime = theFileDTime.AddSeconds(30);

                string theBeginFile = theFileString2.Substring(0, 16) + theStartTime.ToString("HHmm") + "*";
                string theMiddleFile = theFileString2.Substring(0, 20) + "*";
                string theEndFile = theFileString2.Substring(0, 16) + theEndTime.ToString("HHmm") + "*";

                string strFilesToFind = theBeginFile;
                string strFilesToFind1 = theMiddleFile;
                string strFilesToFind2 = theEndFile;
                DirectoryInfo di = new DirectoryInfo(strPath);
                FileInfo[] fi = di.GetFiles(strFilesToFind);
                FileInfo[] fi1 = di.GetFiles(strFilesToFind1);
                FileInfo[] fi2 = di.GetFiles(strFilesToFind2);



                theLiteralText += "</hr>";
                for (int i = 0; i < fi.Length; i++)
                {
                    theLiteralText += "<embed src='/AgentRecordings/" + fi[i].ToString() + "' type='application/x-mplayer2' height='40' autostart='false' loop='FALSE' style='width: 250px'/> </br>";
                }
                theLiteralText += "</hr>";
                if (theBeginFile != theMiddleFile)
                {
                    for (int i = 0; i < fi1.Length; i++)
                    {
                        theLiteralText += "<embed src='/AgentRecordings/" + fi1[i].ToString() + "' type='application/x-mplayer2' height='40' autostart='false' loop='FALSE' style='width: 250px'/> </br>";
                    }
                    theLiteralText += "</hr>";
                }
                if (theBeginFile != theEndFile && theMiddleFile != theEndFile)
                {
                    for (int i = 0; i < fi2.Length; i++)
                    {
                        theLiteralText += "<embed src='/AgentRecordings/" + fi2[i].ToString() + "' type='application/x-mplayer2' height='40' autostart='false' loop='FALSE' style='width: 250px'/> </br>";
                    }
                }
                theLiteralText += "</hr>";
                return theLiteralText;


            }
            catch (System.Exception excpt)
            {
                Console.WriteLine("Error: " + excpt.Message);
                return theLiteralText;
            }


        }




        protected string getRecordingFiles2(Object theFilestring)
        {
            string theLiteralText = "";
            string theFilename = "";
            string theFileString2 = theFilestring.ToString();
            ArrayList theFiles = new ArrayList(); //the  filenames in the directory matching the partial string
            try
            {


                //use first 21 characters
                //get filenames with no path
                //use virtual directory path on server
                string strPath = Server.MapPath("/AgentRecordings2");

                //string strPath = "Z:/ACMGCallRecordings/"; //Server.MapPath("/AgentRecordings");// "E:/ACMGCallRecordings/";//theCSVDirectory;
                //get the recording time that should be on the file
                string theFileHour = theFileString2.Substring(16, 2);
                string theFileMin = theFileString2.Substring(18, 2);
                string theFileSec = theFileString2.Substring(20, 2);
                string theFileTime = theFileHour + ":" + theFileMin + ":" + theFileSec;

                //get the minute for 1 minute before and 1 minute after
                DateTime theFileDTime = DateTime.Parse(theFileTime);
                DateTime theStartTime = theFileDTime.AddSeconds(-30);
                DateTime theEndTime = theFileDTime.AddSeconds(30);

                string theBeginFile = theFileString2.Substring(0, 16) + theStartTime.ToString("HHmm") + "*";
                string theMiddleFile = theFileString2.Substring(0, 20) + "*";
                string theEndFile = theFileString2.Substring(0, 16) + theEndTime.ToString("HHmm") + "*";

                string strFilesToFind = theBeginFile;
                string strFilesToFind1 = theMiddleFile;
                string strFilesToFind2 = theEndFile;
                DirectoryInfo di = new DirectoryInfo(strPath);
                FileInfo[] fi = di.GetFiles(strFilesToFind);
                FileInfo[] fi1 = di.GetFiles(strFilesToFind1);
                FileInfo[] fi2 = di.GetFiles(strFilesToFind2);



                theLiteralText += "</hr>";
                for (int i = 0; i < fi.Length; i++)
                {
                    theLiteralText += "<embed src='/AgentRecordings2/" + fi[i].ToString() + "' type='application/x-mplayer2' height='40' autostart='false' loop='FALSE' style='width: 250px'/> </br>";
                }
                theLiteralText += "</hr>";
                if (theBeginFile != theMiddleFile)
                {
                    for (int i = 0; i < fi1.Length; i++)
                    {
                        theLiteralText += "<embed src='/AgentRecordings2/" + fi1[i].ToString() + "' type='application/x-mplayer2' height='40' autostart='false' loop='FALSE' style='width: 250px'/> </br>";
                    }
                    theLiteralText += "</hr>";
                }
                if (theBeginFile != theEndFile && theMiddleFile != theEndFile)
                {
                    for (int i = 0; i < fi2.Length; i++)
                    {
                        theLiteralText += "<embed src='/AgentRecordings2/" + fi2[i].ToString() + "' type='application/x-mplayer2' height='40' autostart='false' loop='FALSE' style='width: 250px'/> </br>";
                    }
                }
                theLiteralText += "</hr>";
                return theLiteralText;


            }
            catch (System.Exception excpt)
            {
                Console.WriteLine("Error: " + excpt.Message);
                return theLiteralText;
            }


        }



		
	}
}