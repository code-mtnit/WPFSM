using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Sbn.Libs.AssemblyTools;
using Sbn.Core;
using Sbn.Core;
using MSXML2;
namespace Sbn.Systems.ELS.ELSObject
{
[Description("")]
[DisplayName ("")]
[ItemsType ("Sbn.Systems.ELS.ELSObject.Documents")]
[SystemName("ELS")]

[Serializable]
public class Documents : SbnListObject<Document> 
{
#region Constructors
public Documents()
: base()
{
}
#endregion Constructors
public override object  Clone(string sNodeName)
{
Documents Col = new  Documents ();
foreach (Document objMember in this)
{
Col.Add((Document)objMember.Clone(sNodeName));
}
return Col;
}
}
}
