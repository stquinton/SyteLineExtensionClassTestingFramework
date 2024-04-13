using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;

using Mongoose.Core.Common;
using Mongoose.Core.DataAccess;
using Mongoose.IDO;
using Mongoose.IDO.Protocol;

namespace ExtensionClassTestingFramework
{
    // [IDOExtensionClass( "YourExtensionClassCode" )]
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
            LoadCollectionRequestData req = new LoadCollectionRequestData();
            LoadCollectionResponseData rsp = new LoadCollectionResponseData();

            req.IDOName = "SLItems";
            req.Filter = "PMTCode = 'P'";
            req.RecordCap = 1000;

            rsp = context.LoadCollection(req);
            rsp.Fill(dtResults);

            return dtResults;
        }
    }
}
