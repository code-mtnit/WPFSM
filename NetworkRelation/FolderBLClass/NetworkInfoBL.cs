using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using BaseClass;

namespace MonitorInfoViewer
{
    public class NetworkInfoBL : BaseBL
    {
        public NetworkInfoBL()
        {
           
        }

        public NetworkInfoBL(string FromIP, string ToIP)
        {
            m_ArrCounters.Add(new ObjectMetaData(true,FromIP, "SearchFromIP"));
            m_ArrCounters.Add(new ObjectMetaData(true, ToIP, "SearchToIP"));
        }

        public NetworkInfoBL(string ViewIPStatus)
        {
            m_ArrCounters.Add(new ObjectMetaData(true, ViewIPStatus, "ViewIPStatus"));
        }


        public override ReplyData ExecuteQuery(string i_IP, int i_Port , ClientInfo clientInfo)
        {


            QueryData SysInfoQueryData = new QueryData();
            SysInfoQueryData.Type = Consts.SectionType.NetWorkInfo;

            //SysInfoQueryData.ArrCounter.Add(new ObjectMetaData("ProcessName", m_ProcessName));
            SysInfoQueryData.CurrClient = clientInfo;
            string QueryString = SysInfoQueryData.Serialize();
            String ReplyString = Comm.SendQuery(i_IP, i_Port, QueryString);
            ReplyData ReplyDataObj = ReplyData.Deserialize(ReplyString);
            
            return ReplyDataObj;



          //  QueryData ProcessQueryData = new QueryData();
          //  ProcessQueryData.Type = Consts.SectionType.NetWorkInfo;

          //  foreach (ObjectMetaData currCounter in m_ArrCounters)
          //  {
          //      if (currCounter.Checked)
          //      {
          //          ProcessQueryData.ArrCounter.Add(currCounter);
          //      }
          //  }
          ////  ProcessQueryData.CurrClient = clientInfo;// new ClientInfo("10.0.142.22", "Madadi", Consts.ClientStatus.Connected);
          //  string QueryString = ProcessQueryData.Serialize();
          //  String ReplyString = Comm.SendQuery(i_IP, i_Port, QueryString);
          //  ReplyData ReplyDataObj = new ReplyData();

          //  return ReplyDataObj;
        }

        public override ReplyData ExecuteQuery(string i_IP, int i_Port)
        {
            throw new NotImplementedException();
        }

        //public static string ExecuteQuery(string i_IP, int i_Port , string ViewStatusIP)
        //{
        //    QueryData ProcessQueryData = new QueryData();
        //    ProcessQueryData.Type = Consts.SectionType.NetWorkInfo;
        //    ProcessQueryData.ArrCounter.Add(new ObjectMetaData(true, ViewStatusIP, "ViewIPStatus"));


        //    string QueryString = ProcessQueryData.Serialize();
        //    String ReplyString = Comm.SendQuery(i_IP, i_Port, QueryString);


        //    return ReplyString;
        //}

        public override ReplyData ExecuteQuery(string i_IP, int i_Port, ClientInfo clientInfo, object CurObj)
        {
            throw new NotImplementedException();
        }
    }
}
