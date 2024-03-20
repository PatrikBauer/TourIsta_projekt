using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2
{
    public class Model
    {
        private Persistence mydb;
        public Model(Persistence persistence)
        {
            mydb = persistence;
            if (mydb.destinations.Count() == 0)
            {
                ImportCities();
            }
            if (mydb.comforts.Count() == 0)
            {
                ImportComfort();
            }
        }
        public List<Comfort> LoadComforts()
        {
            return mydb.comforts.ToList();
        }
        public List<Destination> LoadCities()
        {
            return mydb.destinations.ToList();
        }
        public void ImportComfort()
        {
            using (System.IO.StreamReader read = new StreamReader("komfort.txt"))
            {
                while (!read.EndOfStream)
                {
                    string[] temp = read.ReadLine().Split(";");
                    mydb.comforts.Add(new Comfort { Level = temp[0], Multiplier = Convert.ToDouble(temp[1]) });
                }
            }
            mydb.SaveChanges();
        }
        public void ImportCities()
        {
            using (System.IO.StreamReader read = new StreamReader("varosok.txt"))
            {
                while (!read.EndOfStream)
                {
                    string[] temp = read.ReadLine().Split(";");
                    mydb.destinations.Add(new Destination { City = temp[0], Price = Convert.ToInt32(temp[1]) });
                }
            }
            mydb.SaveChanges();
        }
        public bool Registration(string name, string pass)
        {
            bool success = false;
            if (mydb.user.Where(x => x.Name == name).Any())
            {
                return success;
            }
            else
            {
                using var trx = mydb.Database.BeginTransaction();
                mydb.user.Add(new User { Name = name, Password = pass });
                trx.Commit();
                mydb.SaveChanges();
                return true;
            }
        }
        public bool SignIn(string name, string pass)
        {
            return mydb.user.Where(x => x.Name == name && x.Password == pass).Any();
        }
    }
}
