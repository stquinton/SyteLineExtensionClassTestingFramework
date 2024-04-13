using Mongoose.IDO;
using Mongoose.IDO.Protocol;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ExtensionClassTestingFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            string slserver = "https://YourSyteLineServer/IDORequestService/RequestService.aspx";

            Client oclient = new Client(slserver, IDOProtocol.Http);
            DataTable dtResults = new DataTable();

            try
            {
                OpenSessionResponseData res = oclient.OpenSession("username@domain.com", "ThisIsAPassword", "ConfigurationName");

                if (res.Result.ToString() != "Success") { throw new Exception(res.Result.ToString()); }

                dtResults = YourExtensionClassCode.Process_MyMethod(context: oclient, Param1: "Value", Param2: "Value");

                Console.WriteLine(dtResults.Rows.Count);

                string curr = DateTime.Now.ToString("yyyyMMdd_HHmmss");

                output_results(dtResults, "C:\\output_" + curr + ".csv");
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            finally
            {
                oclient.CloseSession();
                Console.Read();
            }

        }


        static void output_results(DataTable data, string path)
        {
            StringBuilder sb = new StringBuilder();

            foreach (DataColumn col in data.Columns)
            {
                sb.Append(col.ColumnName + ',');
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append(Environment.NewLine);

            foreach (DataRow row in data.Rows)
            {
                foreach (DataColumn col in data.Columns)
                {
                    sb.Append(row[col].ToString().Replace(",", "|") + ",");
                }

                sb.Remove(sb.Length - 1, 1);
                sb.Append(Environment.NewLine);
            }

            StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8);

            sw.WriteLine(sb);
            sw.Close();
        }
    }
}
