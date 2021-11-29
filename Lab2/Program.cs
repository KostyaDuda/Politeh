using System;
using System.Data;

namespace Lab2
{
    class Program
    {

        static void Main(string[] args)
        {
            DataSet Ap = new DataSet("Appartaments");
            DataTable Buildings = Ap.Tables.Add("Buildings");
            DataTable Residents = Ap.Tables.Add("Residents");
            DataTable Appartaments = Ap.Tables.Add("Appartaments");
            // TEEMS
            Appartaments.Columns.Add(new DataColumn("Id", typeof(int))
            {
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1
            });

            Appartaments.Columns.Add(new DataColumn("Id_build", typeof(int))
            {
                AllowDBNull = false
            });
            Appartaments.Columns.Add(new DataColumn("Id_resident", typeof(int))
            {
                AllowDBNull = false
            });

            Appartaments.PrimaryKey = new DataColumn[] { Appartaments.Columns["Id"] };

            Appartaments.Columns.Add(new DataColumn("type", typeof(string))
            {
                AllowDBNull = false           
            });
            Appartaments.Columns.Add(new DataColumn("number", typeof(int))
            {
                AllowDBNull = false

            });

            //PROJECTS
            Buildings.Columns.Add(new DataColumn("Id", typeof(int)));
            Buildings.PrimaryKey = new DataColumn[] { Buildings.Columns["Id"] };
            Buildings.Columns.Add(new DataColumn("Street", typeof(string))
            {
                AllowDBNull = false
            });
            Buildings.Columns.Add(new DataColumn("number", typeof(string)));
            ;
            //CLIENTS
            Residents.Columns.Add(new DataColumn("Id", typeof(int))
            {
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1
            });
            Residents.Columns.Add(new DataColumn("Name", typeof(string))
            {
                AllowDBNull = false
            });
            Residents.Columns.Add(new DataColumn("Date", typeof(string))
            {
                AllowDBNull = false
            });

            Residents.Rows.Add(1,"Kostya     ","14.11.1999");
            Residents.Rows.Add(2,"Katya     ", "21.12.2000");
            Residents.Rows.Add(3,"Andrey   ", "24.10.1979");
            Residents.Rows.Add(4,"Oleksandr  ", "03.07.1995");
            Residents.Rows.Add(5,"Viktor", "10.10.1999");

            Buildings.Rows.Add(1,"Bandera st    ","12/1");
            Buildings.Rows.Add(2, "Zaliznychna st    ", "10");
            Buildings.Rows.Add(3,"Franka st  ","23");
            Buildings.Rows.Add(4,"Vaysera st","13a");
            Buildings.Rows.Add(5,"Proskyrivska  ","10");

            Appartaments.Rows.Add(1,1,1,"1 room",12);
            Appartaments.Rows.Add(2,2, 2, "1 room", 34);
            Appartaments.Rows.Add(3,3, 3, "2 room", 134);
            Appartaments.Rows.Add(4,4, 4, "32 room", 124);
            Appartaments.Rows.Add(5,5, 5, "4 room", 12);

            //cout
            Console.WriteLine("Buildings");
            foreach (DataRow builds in Buildings.Rows)
            {
                foreach (DataColumn build in Buildings.Columns)
                {
                    Console.Write(builds[build] + "\t\t");
                }
                Console.Write("\n");
            }

            Console.WriteLine("Residents");
            foreach (DataRow residents in Residents.Rows)
            {
                foreach (DataColumn resident in Residents.Columns)
                {
                    Console.Write(residents[resident] + "\t\t");
                }
                Console.Write("\n");
            }

            Console.WriteLine("Appartaments");
            foreach (DataRow appartaments in Appartaments.Rows)
            {
                foreach (DataColumn appartament in Appartaments.Columns)
                {
                    Console.Write(appartaments[appartament] + "\t\t");
                }
                Console.Write("\n");
            }
            //Sort
            Console.WriteLine("\nSort\n"); 

            DataView view = new DataView(Residents);
            view.Sort = "Name ASC";
           // view.RowFilter = "Name IN ('A%', 'K%')";
        
            Console.WriteLine("Residents");
            foreach (DataRowView residents in view)
            {
                foreach (DataColumn resident in Residents.Columns)
                {
                    Console.Write(residents.Row[resident] + "\t\t");
                }
                Console.Write("\n");
            }

            //Sort + Filter
            Console.WriteLine("\nSort + Filter\n");

            DataView view_sort = new DataView(Buildings);
            //view_sort.Sort = "Street ASC";
            view_sort.RowFilter = "Street LIKE 'Ba*'";

            foreach (DataRowView builds in view_sort)
            {
                foreach (DataColumn build in Buildings.Columns)
                {
                    Console.Write(builds.Row[build] + "\t\t");
                }
                Console.Write("\n");
            }

            //Changes
             Ap.AcceptChanges();
            Residents.Rows[3].Delete();
            Residents.Rows[4]["Name"] = "Vasyl";
            Residents.Rows.Add(6, "Petro","12.11.1996");



            Appartaments.Rows[3].Delete();
            Appartaments.Rows[4]["type"] = "1 room";
            Appartaments.Rows.Add(6, 6, 6, "3 room", 125);


            //
            DataSet NewDataSet = new DataSet("New");
              Console.Write("\nChanges:\n");
              foreach (DataTable table in Ap.Tables)
              {
                  DataTable newTable = table.Copy();
                  newTable.Rows.Clear();
                  NewDataSet.Tables.Add(newTable);
                  foreach (DataRow row in table.Rows)
                  {

                      if (row.RowState == DataRowState.Deleted)
                      {
                          var id = (int)row["Id", DataRowVersion.Original];
                          Console.Write("In table {0} str with ID ({2}) is {1}\n ", table.TableName, row.RowState,id);                                           
                      }
                      if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                      {
                      //    var id = (int)row["Id", DataRowVersion.Original];
                          Console.Write("In table {0} str ( ", table.TableName);
                          foreach (DataColumn col in table.Columns)
                          {
                              Console.Write(row[col] + " ");
                          }
                          Console.Write(") is {0}\n", row.RowState);
                      }
                  }
              }


            Appartaments.AcceptChanges();
            Buildings.AcceptChanges();
            Residents.AcceptChanges();
              Console.WriteLine("\nCorrected tables:\n");
  
            Console.WriteLine("Buildings");
            foreach (DataRow builds in Buildings.Rows)
            {
                foreach (DataColumn build in Buildings.Columns)
                {
                    Console.Write(builds[build] + "\t\t");
                }
                Console.Write("\n");
            }

            Console.WriteLine("Residents");
            foreach (DataRow residents in Residents.Rows)
            {
                foreach (DataColumn resident in Residents.Columns)
                {
                    Console.Write(residents[resident] + "\t\t");
                }
                Console.Write("\n");
            }

            Console.WriteLine("Appartaments");
            foreach (DataRow appartaments in Appartaments.Rows)
            {
                foreach (DataColumn appartament in Appartaments.Columns)
                {
                    Console.Write(appartaments[appartament] + "\t\t");
                }
                Console.Write("\n");
            }
            Console.ReadKey();
        }
    }
}
