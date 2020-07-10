using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using DBSystem.DAL;
using DBSystem.ENTITIES;

namespace DBSystem.BLL
{
    public class PlayerController
    {
        
        public List<Player> FindByID(int id)
        {
            using (var context = new Context())
            {
                IEnumerable<Player> results =
                    context.Database.SqlQuery<Player>("Player_GetByTeam @ID"
                        , new SqlParameter("ID", id));
                return results.ToList();
            }
        }

        public List<Player> List()
        {
            using (var context = new Context())
            {
                return context.Players.ToList(); //Ex 8
            }
        }

        public int Add(Player item)
        {
            using (var context = new Context())
            {
                context.Players.Add(item);
                context.SaveChanges();
                return item.PlayerID;

            }
        }
    }
}
