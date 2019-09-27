using System;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Alencar_Teste_2
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 3)
            {
                Uri orgUrl = new Uri(args[0]);
                String personalAccessToken = args[1];
                int workItemId = int.Parse(args[2]);

                VssConnection connection = new VssConnection(orgUrl, new VssBasicCredential(string.Empty, personalAccessToken));

                List<WorkitemDto> allWorkiDB = DBSelect();

                ShowWorkItemDetails(connection, workItemId, allWorkiDB).Wait();

            }
            else
            {
                Console.WriteLine("Usando: Console App {orgUrl} {personalAccessToken} {workItemId}");
            }

        }

        static public async Task ShowWorkItemDetails(VssConnection connection, int workItemId, List<WorkitemDto> all)
        {

            WorkItemTrackingHttpClient witClient = connection.GetClient<WorkItemTrackingHttpClient>();


            try
            {

                WorkItem workitem = await witClient.GetWorkItemAsync(workItemId);

               
                object a = new object();
                object b = new object();
                object c = new object();
                var getall = 0;

                foreach (var field in workitem.Fields)
                {           
  
                    if (field.Key == "System.WorkItemType")
                    {
                        a = field.Value;
                        getall++;
                    }

                    if (field.Key == "System.Title")
                    {
                        b = field.Value;
                        getall++;
                    }

                    if (field.Key == "System.CreatedDate")
                    {
                        c = field.Value;
                        getall++;
                    }

                    if (getall == 3)
                    {
                        int? total = 0;
                        if (all.Count == 0)
                            total = 1;
                        else
                            total = all[0].ID+1;

                        all.Add(new WorkitemDto()
                        {
                            ID = total ?? 1 ,
                            Tipo = a.ToString(),
                            Titulo = b.ToString(),
                            DataCriacao = Convert.ToDateTime(c.ToString())
                        });


                        DBInsert(all[all.Count - 1]);
                        getall++;
                    }

                    Console.WriteLine("  {0}: {1}", field.Key, field.Value);
                }



            }
            catch (AggregateException aex)
            {
                VssServiceException vssex = aex.InnerException as VssServiceException;
                if (vssex != null)
                {
                    Console.WriteLine(vssex.Message);
                }
            }
        }


        private static List<WorkitemDto> DBSelect()
        {
            string connectionString = "Data Source=PC_VVP;Initial Catalog=Workitem;User ID=sa;Password=123456";

            List<WorkitemDto> listaWorkitem = new List<WorkitemDto>();

            SqlConnection sqlConn = new SqlConnection(connectionString);

            sqlConn.Open();

            SqlCommand cmd = new SqlCommand("SELECT ID, Tipo, Titulo, DataCriacao FROM [Workitem].[dbo].[TB.Workitem] ORDER BY ID DESC", sqlConn);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                listaWorkitem.Add(new WorkitemDto()
                {
                    ID = (int)dr["ID"],
                    Tipo = dr["Tipo"].ToString(),
                    Titulo = dr["Titulo"].ToString(),
                    DataCriacao = Convert.ToDateTime(dr["DataCriacao"])
                });
            }

            sqlConn.Close();


            return listaWorkitem;
        }

        private static List<string> DBInsert(WorkitemDto wi)
        {
            string connectionString = "Data Source=PC_VVP;Initial Catalog=Workitem;User ID=sa;Password=123456";

            List<string> listaWorkitem = new List<string>();

            SqlConnection sqlConn = new SqlConnection(connectionString);

            sqlConn.Open();           

            using (SqlCommand command = sqlConn.CreateCommand())
            {
                command.CommandText = "INSERT INTO [Workitem].[dbo].[TB.Workitem] VALUES(@param1,@param2,@param3,@param4)";

                command.Parameters.AddWithValue("@param1", wi.ID);
                command.Parameters.AddWithValue("@param2", wi.Tipo);
                command.Parameters.AddWithValue("@param3", wi.Titulo);
                command.Parameters.AddWithValue("@param4", wi.DataCriacao);

                command.ExecuteNonQuery();
            }

            sqlConn.Close();


            return listaWorkitem;
        }

    }

    public class WorkitemDto
    {
        public int? ID { get; set; }

        public string Tipo { get; set; }

        public string Titulo { get; set; }

        public DateTime DataCriacao { get; set; }

    }
}
