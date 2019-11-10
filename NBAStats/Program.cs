using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace NBAStats
{
    class Program
    {


        static void Main(string[] args)
        {
            string query = "select PTS, PTS_1 " +
            "from games";

            SqlConnection sqlcon = new SqlConnection(@"Data Source=LAPTOP-BSO196BK\SQLEXPRESS;Initial Catalog=NBAStats;Integrated Security=True");

            SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);
            sqlcon.Close();
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);

            int i = 0;
            double HS = 0;
            double VS = 0;

            foreach (DataRow row in dtbl.Rows)
            {
                VS += int.Parse(row["PTS"].ToString());
                HS += int.Parse(row["PTS_1"].ToString());

                i += 1;
            }

            double j = HS / i;
            double k = VS / i;

            Console.WriteLine("Home Team scores " + j.ToString() + " Road Team scores " + k.ToString());


            string query2 = "select c.gameID, c.PTS_1, c.PTS, " +
                "b.Wins as VisitorWins, a.Wins as HomeWins " +
                "from games c, games d, standings a, standings b " +
                "where a.team = c.home_neutral " +
                "and b.team = d.Visitor_Neutral " +
                "and a.year = c.year " +
                "and b.year = d.year " +
                "and c.gameID = d.gameID;";

            SqlConnection sqlcon2 = new SqlConnection(@"Data Source=LAPTOP-BSO196BK\SQLEXPRESS;Initial Catalog=NBAStats;Integrated Security=True");

            SqlDataAdapter sda2 = new SqlDataAdapter(query2, sqlcon2);
            sqlcon2.Close();
            DataTable dtbl2 = new DataTable();
            sda2.Fill(dtbl2);

            //double[] p = new double[83];

            double[,] winsGrid = new double[83, 3];

            foreach (DataRow row in dtbl2.Rows)
            {


                if (int.Parse(row["VisitorWins"].ToString()) > int.Parse(row["HomeWins"].ToString()))
                {
                    int winDifference = int.Parse(row["VisitorWins"].ToString()) - int.Parse(row["HomeWins"].ToString());

                    for (int x = 0; x < winsGrid.GetLength(0); x++)
                    {
                        if (winDifference == x)
                        {
                            winsGrid[x, 0] = x;
                            winsGrid[x, 1] += int.Parse(row["PTS"].ToString()) - int.Parse(row["PTS_1"].ToString());
                            winsGrid[x, 2] += 1;
                        }
                    }
                }
                else if (int.Parse(row["VisitorWins"].ToString()) == int.Parse(row["HomeWins"].ToString()))
                {
                    winsGrid[0, 2] += 1;
                }
                else
                {
                    int winDifference = int.Parse(row["HomeWins"].ToString()) - int.Parse(row["VisitorWins"].ToString());

                    for (int x = 0; x < winsGrid.GetLength(0); x++)
                    {
                        if (winDifference == x)
                        {
                            winsGrid[x, 0] = x;
                            winsGrid[x, 1] += int.Parse(row["PTS_1"].ToString()) - int.Parse(row["PTS"].ToString());
                            winsGrid[x, 2] += 1;

                            //p[winDifference] += 1;
                        }
                    }
                }
            }
            double[] z = new double[83];

            for (int x = 0; x < 83; x++)
            {
                Console.Write(x.ToString() + " ");
                Console.Write(winsGrid[x, 1].ToString() + " ");
                Console.Write(winsGrid[x, 2].ToString() + " ");               
                z[x] = winsGrid[x, 1] / winsGrid[x, 2];

                Console.Write("Teams with " + x + " More Wins won by average of " +
                   z[x]);


                Console.WriteLine();
            }
        }
    }
}
