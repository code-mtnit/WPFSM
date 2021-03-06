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
    public class ProcessesBL : BaseBL
    {       
        public ProcessesBL()
        {
            m_ArrCounters.Add(new ObjectMetaData(true, "Name", "Name"));
            m_ArrCounters.Add(new ObjectMetaData(false, "User Name", "User Name"));
            m_ArrCounters.Add(new ObjectMetaData(false, "Peak Memory Kb", "Peak Memory"));
            m_ArrCounters.Add(new ObjectMetaData(false, "Start Time", "Start Time"));
            m_ArrCounters.Add(new ObjectMetaData(false, "Handles Count", "Handles Count"));
            m_ArrCounters.Add(new ObjectMetaData(false, "Threads Count", "Threads Count"));
            m_ArrCounters.Add(new ObjectMetaData(false, "Main File Name", "Main File Name"));
            m_ArrCounters.Add(new ObjectMetaData(false, "Memory Usage Kb", "Memory Usage"));
            m_ArrCounters.Add(new ObjectMetaData(false, "CPU Time", "CPU Time"));
            m_ArrCounters.Add(new ObjectMetaData(false, "CPU Usage %", "CPU Usage"));
            m_ArrCounters.Add(new ObjectMetaData(false, "Paged Memory Kb", "Paged Memory"));
            m_ArrCounters.Add(new ObjectMetaData(false, "Page Faults/sec", "Page Faults"));
            m_ArrCounters.Add(new ObjectMetaData(false, "I/O Reads b/sec", "I/O Reads"));
            m_ArrCounters.Add(new ObjectMetaData(false, "Priority", "Priority"));
        }

        public override ReplyData ExecuteQuery(string i_IP, int i_Port , ClientInfo clientInfo)
        {            
            QueryData ProcessQueryData = new QueryData();
            ProcessQueryData.Type = Consts.SectionType.Processes;

            foreach (ObjectMetaData currCounter in m_ArrCounters)
            {
                if (currCounter.Checked)
                {
                    ProcessQueryData.ArrCounter.Add(currCounter);
                }
            }
            ProcessQueryData.CurrClient = clientInfo;// new ClientInfo("10.0.142.22", "Madadi", Consts.ClientStatus.Connected);
            string QueryString = ProcessQueryData.Serialize();
            String ReplyString = Comm.SendQuery(i_IP, i_Port, QueryString);
            ReplyData ReplyDataObj = ReplyData.Deserialize(ReplyString);

            return ReplyDataObj;
        }

        public override ReplyData ExecuteQuery(string i_IP, int i_Port)
        {
            throw new NotImplementedException();
        }

        public override ReplyData ExecuteQuery(string i_IP, int i_Port, ClientInfo clientInfo, object CurObj)
        {
            throw new NotImplementedException();
        }
    }
}
