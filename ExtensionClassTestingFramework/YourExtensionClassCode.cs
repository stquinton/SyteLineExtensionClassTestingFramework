using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
//using System.Transactions;

using Mongoose.Core.Common;
using Mongoose.Core.DataAccess;
using Mongoose.IDO;
using Mongoose.IDO.Protocol;

namespace ExtensionClassTestingFramework
{
    [IDOExtensionClass( "YourExtensionClassCode" )]
    public partial class YourExtensionClassCode : IDOExtensionClass
    {
        [IDOMethod(MethodFlags.CustomLoad, "Infobar")]
        public DataTable MyMethod(string Param1, string Param2)
        {
            return Process_MyMethod(this.Context.Commands, Param1, Param2);
        }
        public static DataTable Process_MyMethod(IIDOCommands context, string Param1, string Param2)
        {
            DataTable dtResults = new DataTable();
            // Add columns and match the expected data type
            dtResults.Columns.Add("Item", typeof(string));       
            dtResults.Columns.Add("Description", typeof(string));

            LoadCollectionRequestData req = new LoadCollectionRequestData();
            req.IDOName = "SLItems";
            req.Filter = "PMTCode = 'P'";
            req.RecordCap = 1000;
            req.PropertyList = new PropertyList("Item, Description");

            LoadCollectionResponseData rsp = context.LoadCollection(req);
            rsp.Fill(dtResults);

            return dtResults;
        }
    }
}
