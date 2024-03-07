using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Groupe_11.classes
{
    internal class DataManager
    {
        private List<Oenologue> oenologues;

        public DataManager()
        {
            this.oenologues = new List<Oenologue>();
        }
        public bool IsOenologueConnected()
        {
            return oenologues.Count > 0;
        }

        public Oenologue GetOenologueByEmail(string email)
        {
            return oenologues.Find(oenologue => oenologue.Email == email);
        }
        public void AddOenologue(Oenologue oenologue)
        {
            oenologues.Add(oenologue);
        }

        public List<Oenologue> GetAllOenologues()
        {
            return oenologues;
        }
        public Oenologue GetOenologueByUsernameAndPassword(string username, string password)
        {
            return oenologues.FirstOrDefault(oenologue => oenologue.Nom == username && oenologue.Password == password);
        }

    }
}
