using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NextClass;
using NextClass.Extensions;
using NextClass.Model;

namespace TestApp
{
    public partial class Form1 : Form
    {
        private const string _db = "JUNIOR_SQLTraceability_v2";
        private readonly string _sqlIp = @"192.168.0.187\DevIT";
        private readonly string _sqlUser = "sa";
        private readonly string _sqlPass = "b52mkmn5";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dupa = new ConnectionStringModel(_sqlIp, _db, _sqlUser, _sqlPass);
            var ble = dupa.ConnectionString;
            var fly = new MsSql(dupa);
            var emo = fly.IsConnection();
            fly.ExecSql("select getdate()");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var fly = new MsSql(new ConnectionStringModel(_sqlIp, _db, _sqlUser, _sqlPass));
            var emo = fly.IsConnection();
            fly.ExecSql("select getdate()");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var dupa = "1234567890";
            string[] foo = new string[]{"jeden", "dwa","trzy","cztery"};


            rTB.Text = dupa.ToQuote();
            rTB.AppendText(Environment.NewLine+dupa.ToQuote(char.Parse("'")));
            rTB.AppendText(Environment.NewLine + dupa.ToQuote('\u0022'));
            rTB.AppendText(Environment.NewLine + dupa.ToQuote('"'));
            rTB.AppendText(Environment.NewLine + dupa.ToQuote(char.Parse("[")));
            rTB.AppendText(Environment.NewLine + dupa.ToQuote(char.Parse("]")));
            rTB.AppendText(Environment.NewLine + dupa.ToQuote(char.Parse("(")));
            rTB.AppendText(Environment.NewLine + dupa.ToQuote(char.Parse(")")));
            rTB.AppendText(Environment.NewLine + dupa.ToQuote(char.Parse("{")));
            rTB.AppendText(Environment.NewLine + dupa.ToQuote(char.Parse("}")));
            rTB.AppendText(Environment.NewLine + dupa.ToQuote(char.Parse("*")));
            rTB.AppendText(Environment.NewLine + dupa.ToQuote(char.Parse("+")));
            rTB.AppendText(Environment.NewLine + "---------------");

            rTB.AppendText(Environment.NewLine + foo.ToQuoteAndCommaSplitStrings());

        }

        private void button4_Click(object sender, EventArgs e)
        {

            var fly = new MsSql(new ConnectionStringModel(_sqlIp, _db, _sqlUser, _sqlPass));

            var dataTable = fly.ExecStoredProcedure("CofA_Prod", "Test3", "dupa", "dupa");

            rTB.AppendText(dataTable.Rows[0][0].ToString());

        }

        private void button5_Click(object sender, EventArgs e)
        {        //EXEC	[dbo].[ASK_SN]	@LINE = N'JUNIOR',	@OP = 750, @PN = N'26111047',	@SN = N'26111047062173033700'

            var dupa = new Traceability(new ConnectionStringModel(_sqlIp, _db, _sqlUser, _sqlPass));
            if (!dupa.IsConnection()) return;
            try
            {
                
                DataTable tb = new DataTable();
                tb.Load((dupa.Ask_SN("Junior", "750", "26111047", "26111047062173033700"));

                var foo = tb.Rows[0][0].ToString();
                MessageBox.Show(foo);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
                throw;
            }

        }

        //public void dupa()
        //{
        //    ///////////////////
        //    String connectionString =
        //        "Data Source=(local);Initial Catalog=MySchool;Integrated Security=True;Asynchronous Processing=true;";

        //    CountCourses(connectionString, 2012);


        //    Console.WriteLine("Following result is the departments that started from 2007:");
        //    GetDepartments(connectionString, 2007);
        //    Console.WriteLine();

        //    Console.WriteLine("Add the credits when the credits of course is lower than 4.");
        //    AddCredits(connectionString, 4);
        //    Console.WriteLine();

        //    Console.WriteLine("Please press any key to exit...");
        //    Console.ReadKey();
        //}

        //static void CountCourses(String connectionString, Int32 year)
        //{
        //    var commandText = "Select Count([CourseID]) FROM [MySchool].[dbo].[Course] Where Year=@Year";
        //    var parameterYear = new SqlParameter("@Year", SqlDbType.Int);
        //    parameterYear.Value = year;

        //    Object oValue = SqlHelper.ExecuteScalar(connectionString, commandText, CommandType.Text, parameterYear);

        //    Int32 count;

        //    if (Int32.TryParse(oValue.ToString(), out count))
        //    {
        //        Console.WriteLine(@"here {0} {1} course{2} in {3}.",
        //            count > 1 ? "are" : "is",
        //            count,
        //            count > 1 ? "s" : null,
        //            year);
        //    }
        //}

        //// Display the Departments that start from the specified year.
        //static void GetDepartments(String connectionString, Int32 year)
        //{
        //    String commandText = "dbo.GetDepartmentsOfSpecifiedYear";

        //    // Specify the year of StartDate
        //    SqlParameter parameterYear = new SqlParameter("@Year", SqlDbType.Int);
        //    parameterYear.Value
        //        =
        //        year;

        //    // When the direction of parameter is set as Output, you can get the value after 
        //    // executing the command.
        //    SqlParameter parameterBudget = new SqlParameter("@BudgetSum", SqlDbType.Money);
        //    parameterBudget.Direction = ParameterDirection.Output;

        //    using (SqlDataReader reader = SqlHelper.ExecuteReader(connectionString, commandText,
        //        CommandType.StoredProcedure, parameterYear, parameterBudget)
        //    )
        //    {
        //        Console.WriteLine(@"There {0} {1} course{2} in {3}.", "Name", "Budget", "StartDate",
        //            "Administrator");
        //        while (reader.Read())
        //        {
        //            Console.WriteLine(@"There {0} {1} course{2} in {3}.", reader["Name"],
        //                reader["Budget"], reader["StartDate"], reader["Administrator"]);
        //        }
        //    }
        //    Console.WriteLine("{0,-20}{1,-20:C}", "Sum:", parameterBudget.Value);
        //}

        //// If credits of course is lower than the certain value, the method will add the credits.
        //static void AddCredits(String connectionString, Int32 creditsLow)
        //{
        //    var commandText = "Update [MySchool].[dbo].[Course] Set Credits=Credits+1 Where Credits<@Credits";

        //    var parameterCredits = new SqlParameter("@Credits", creditsLow);

        //    Int32 rows = SqlHelper.ExecuteNonQuery(connectionString, commandText, CommandType.Text, parameterCredits);

        //    Console.WriteLine("{0} row{1} {2} updated.", rows, rows > 1 ? "s" : null, rows > 1 ? "are" : "is");
        //}
    }
}
